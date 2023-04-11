using AutoMapper;
using DevExpress.Xpo;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Machine;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class MachineService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IOptions<AppSettings> _appsettings;
        public MachineService(EPSRepository repository, IMapper mapper, IOptions<AppSettings> settings)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _appsettings = settings;
        }

        public async Task<PagingResult<MachineGridDto>> GetMachines(MachineGridPagingDto pagingModel)
        {
            var machine = await _baseService.FilterPagedAsync<MachineSynchronized, MachineGridDto>(pagingModel);
            foreach (var item in machine.Data)
            {
                var ma = await _baseService.FindAsync<MachineSynchronized, MachineGridDto>(x => x.Imei == item.Imei);
                MachineInformationDto product = new MachineInformationDto();
                var json = JsonConvert.SerializeObject(product);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using var client = new HttpClient();
                var URL = _appsettings.Value.ApiUrl + "get-performance/{imei}";
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.GetAsync(item.Imei);
                var contents = response.Content.ReadAsStringAsync().Result;
                
                if (response.IsSuccessStatusCode)
                {
                    var maSearch = Newtonsoft.Json.JsonConvert.DeserializeObject<MachineSearchDto>(contents);
                    if (maSearch.data != null)
                    {
                        item.DevicePerformance = "Ram" + ": " + maSearch.data.ram + " % " + "\n" + "   Cpu" + ":" + maSearch.data.cpu + " % " + "\n " + "Temp" + ": " + maSearch.data.temperature + "℃";
                    }
                }
                response.EnsureSuccessStatusCode();

            }
            return machine ;
        }
        public async Task<bool> CreateMachine(MachineCreateDto categoryCreate, bool isExploiting = false)
        {
            categoryCreate.IsDelete = false;
            var count = await _repository.CountAsync<Machine>(x => x.DeviceName == categoryCreate.DeviceName.Trim() && x.IsDelete == false && x.CompId == categoryCreate.CompId);
            var countImie = await _repository.CountAsync<Machine>(x => x.Imei == categoryCreate.Imei.Trim() && x.IsDelete == false);
            var countMac = await _repository.CountAsync<Machine>(x => x.Mac == categoryCreate.Mac.Trim() && x.IsDelete == false);

            if (count > 0)
            {
                var errors1 = "GroupService.Message.MachineCode";
                throw new EPSException(errors1);
            }
            else if (countImie > 0)
            {
                var errors1 = "GroupService.Message.ImeiAddress";
                throw new EPSException(errors1);
            }

            else if (countMac > 0)
            {
                var errors1 = "GroupService.Message.MacAddress";
                throw new EPSException(errors1);
            }

            {
                categoryCreate.IsDelete = false;
                categoryCreate.FingerThreshold = 0;
                categoryCreate.UsageStorage = 0;
                categoryCreate.ServerPort = 0;
                categoryCreate.AngleDetect = 0;
                categoryCreate.LockControl = 0;
                categoryCreate.MaxUserCount = 0;
                categoryCreate.MaxFaceCount = 0;
                categoryCreate.MaxFingerCount = 0;
                categoryCreate.MaxCardCount = 0;
                categoryCreate.MaxAttlogCount = 0;
                categoryCreate.UserCount = 0;
                categoryCreate.CardCount = 0;
                categoryCreate.FaceCount = 0;
                categoryCreate.FingerCount = 0;
                categoryCreate.DeviceType = 0;

                await _baseService.CreateAsync<Machine, MachineCreateDto>(categoryCreate);

                var count1 = await _repository.CountAsync<AreaMachine>(x => x.MachineId == categoryCreate.Id);
                if (count1 > 0)
                {
                    var errors1 = "thiết bị đã thuộc vào khu vực";
                    throw new EPSException(errors1);
                }
                else
                {
                    AreaMachineCreateDto createArea = new AreaMachineCreateDto();
                    createArea.MachineId = categoryCreate.Id;
                    createArea.AreaId = categoryCreate.AreaId;
                    createArea.IsDelete = false;
                    await _baseService.CreateAsync<AreaMachine, AreaMachineCreateDto>(createArea);
                }
                
            }
            return true;
        }
        public async Task<int> UpdateDelete1(int id)
        {
            var count1 = await _repository.CountAsync<AreaMachine>(x => x.MachineId == id && x.IsDelete == false);
            if (count1 > 0)
            {
                var area = await _baseService.FindAsync<AreaMachine, AreaMachineGridDto>(x => x.MachineId == id);
                AreaMachineUpdateDto areaUpdate = new AreaMachineUpdateDto();
                areaUpdate.Id = area.Id;
                areaUpdate.MachineId = id;
                areaUpdate.AreaId = area.AreaId;
                areaUpdate.IsDelete = true;
                await _baseService.UpdateAsync<AreaMachine, AreaMachineUpdateDto>(area.Id, areaUpdate);
            }
            var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.MachineId == id && x.IsDelete == false);
            if (countGroupAccess > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                throw new EPSException(errors1);
            }
            else
            {
                var face = await _baseService.FindAsync<Machine, MachineUpdateDto>(x => x.Id == id);
                face.IsDelete = true;

                await _baseService.UpdateAsync<Machine, MachineUpdateDto>(id, face);
            }
            return 0;
        }
        public async Task<int> UpdateDeletes1(int[] ids)
        {
            foreach (var item in ids)
            {


                var company = await _baseService.FindAsync<Machine, MachineUpdateDto>(x => x.Id == item);
                var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.MachineId == company.Id);
                if (countGroupAccess > 0)
                {
                    var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                    throw new EPSException(errors1);
                }
                else
                {
                    if (company != null)
                    {
                        company.IsDelete = true;
                        await _baseService.UpdateAsync<Machine, MachineUpdateDto>(company.Id, company);
                    }
                }
            }
            return 0;
        }
        public async Task<MachineDetailDto> GetMachineById(int id)
        {
            return await _baseService.FindAsync<View_Machine, MachineDetailDto>(id);
        }

        public async Task<int> UpdateMachine(int id, MachineUpdateDto editedMachine)
        {
                
            var count = await _repository.CountAsync<Machine>(x => x.DeviceName == editedMachine.DeviceName.Trim() && x.Id != id && x.IsDelete == false && x.CompId == editedMachine.CompId);
            var countImie = await _repository.CountAsync<Machine>(x => x.Imei == editedMachine.Imei.Trim() && x.Id != id && x.IsDelete == false && x.CompId == editedMachine.CompId);
            var countMac = await _repository.CountAsync<Machine>(x => x.Mac == editedMachine.Mac.Trim() && x.Id != id && x.IsDelete == false && x.CompId == editedMachine.CompId);

            if (count > 0)
            {
                var errors1 = "GroupService.Message.MachineCode";
                throw new EPSException(errors1);
            }
            else if (countImie > 0)
            {
                var errors1 = "GroupService.Message.ImeiAddress";
                throw new EPSException(errors1);
            }

            else if (countMac > 0)
            {
                var errors1 = "GroupService.Message.MacAddress";
                throw new EPSException(errors1);
            }
            else
            {
                await _baseService.UpdateAsync<Machine, MachineUpdateDto>(id, editedMachine);

            }
            
            var count1 = await _repository.CountAsync<AreaMachine>(x => x.MachineId == id);
            if (count1 > 0)
            {
                var aremachine = await _baseService.FindAsync<AreaMachine, AreaMachineUpdateDto>(x => x.MachineId == id);
                AreaMachineUpdateDto areaUpdate = new AreaMachineUpdateDto();
                areaUpdate.Id = aremachine.Id;
                areaUpdate.MachineId = id;
                areaUpdate.AreaId = editedMachine.AreaId;
                areaUpdate.IsDelete = false;
                await _baseService.UpdateAsync<AreaMachine, AreaMachineUpdateDto>(aremachine.Id, areaUpdate);
            }
            else
            {
                AreaMachineCreateDto createArea = new AreaMachineCreateDto();
                createArea.MachineId = id;
                createArea.AreaId = editedMachine.AreaId;
                createArea.IsDelete = false;
                await _baseService.CreateAsync<AreaMachine, AreaMachineCreateDto>(createArea);
            }    
            return 0;

        }

        public async Task<int> DeleteMachine(int id)
        {
            
            var delete = await _baseService.DeleteAsync<Machine, int>(id);
            return delete;
        }

        public async Task<int> DeleteMachine(int[] ids)
        {
            return await _baseService.DeleteAsync<Machine, int>(ids);
        }
        public async Task<bool> CheckExistCode(int id, int compid, string code)
        {
            int count = 0;
            if (id != 0)
                count = await _repository.CountAsync<Machine>(x => x.Code == code.Trim() && x.Id != id && x.IsDelete == false && x.CompId == compid);
            else
                count = await _repository.CountAsync<Machine>(x => x.Code == code.Trim() && x.IsDelete == false && x.CompId == compid);

            if (count > 0)
                return false;
            else
                return true;
        }

        public async Task<int> CreateSynchronizeDataToMachine(int id)
        {
            string sqlQuery = " EXEC [dbo].[CreateSynchronizeDataToMachine] "
                + " @machineIdInput = " + id + " "
              ;

            _repository.ExecuteNonQuery(sqlQuery);
            //IQueryable<p_PieChartData> query = _repository.FromSqlRaw<p_PieChartData>(sqlQuery);

            return 0;
        }
        public async Task<int> RequestRebootToMachine(int id)
        {
            string sqlQuery = " EXEC [dbo].[RequestRebootToMachine] "
                + " @machineIdInput = " + id + " "
              ;

            _repository.ExecuteNonQuery(sqlQuery);
            //IQueryable<p_PieChartData> query = _repository.FromSqlRaw<p_PieChartData>(sqlQuery);

            return 0;
        }
    }
}
