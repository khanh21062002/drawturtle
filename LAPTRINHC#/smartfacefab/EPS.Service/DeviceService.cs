using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.WorkingShiftTimes;
using EPS.Service.Dtos.Device;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using EPS.Service.Dtos.DeviceFeatures;
using Microsoft.EntityFrameworkCore;
using EPS.Service.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using EPS.Service.Dtos.CamViewConfig;

namespace EPS.Service
{
    public class DeviceService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;
        private readonly AppSettings _appSettings;
        private UpdateNotificationService _updateNotificationService;

        public DeviceService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity, IOptions<AppSettings> appSettings, UpdateNotificationService updateNotificationService)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
            _appSettings = appSettings.Value;
            _updateNotificationService = updateNotificationService;
        }

        public async Task<PagingResult<DeviceGridDto>> GetListDevice(DeviceGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<ViewDeviceFeatures, DeviceGridDto>(pagingModel);
        }

        public async Task<int> AddDevice(DeviceCreateDto createDto)
        {
            var dup = await _baseService.CountAsync<Device, DeviceDetailDto>(x => x.DeviceCode == createDto.DeviceCode && x.CompId == _userIdentity.CompId && x.IsDelete == false);
            if (dup > 0)
            {
                var error = "GroupService.Message.DeviceCode";
                throw new EPSException(error);
            }
            else
            {
                createDto.IsDelete = false;
                createDto.Status = 1;
                await _baseService.CreateAsync<Device, DeviceCreateDto>(createDto);

                foreach (var item in createDto.FeaturesId)
                {
                    DeviceFeaturesCreateDto DeviceFeatures = new DeviceFeaturesCreateDto();
                    var listDeviceFeatures = _baseService.Filter<Device, DeviceDetailDto>(x => x.DeviceCode == createDto.DeviceCode && x.CompId == _userIdentity.CompId && x.IsDelete == false).OrderByDescending(x => x.Id).FirstOrDefault();

                    DeviceFeatures.DeviceId = listDeviceFeatures.Id;
                    DeviceFeatures.FeatureId = item;
                    DeviceFeatures.IsDelete = false;

                    await _baseService.CreateAsync<DeviceFeature, DeviceFeaturesCreateDto>(DeviceFeatures);
                }

                //var area = _repository.Filter<Areas>(x => x.Id == createDto.AreaId).FirstOrDefault();
                //string method = "POST";
                //var data = new { data = new string[] { createDto.Id.ToString(), createDto.RstpLink, _userIdentity.CompId.ToString(), area.Code, createDto.DeviceCode } };
                //await _updateNotificationService.SyncData(method, data);

                return createDto.Id;
            }
        }

        public async Task<int> UpdateDevice(int Id, DeviceUpdateDto updateDto)
        {
            var dup = await _baseService.CountAsync<Device, DeviceDetailDto>(x => x.DeviceCode == updateDto.DeviceCode && x.Id != Id && x.CompId == _userIdentity.CompId && x.IsDelete == false);
            if (dup > 0)
            {
                var error = "GroupService.Message.DeviceCode";
                throw new EPSException(error);
            }
            else
            {
                await _baseService.UpdateAsync<Device, DeviceUpdateDto>(Id, updateDto);

                var listDeviceFeatures = _repository.Filter<DeviceFeature>(x => x.DeviceId == Id && x.IsDelete == false).Select(x => x.Id).ToList();
                foreach (var item in listDeviceFeatures)
                {
                    await _repository.DeleteAsync<DeviceFeature, int>(item);
                }

                foreach (var item in updateDto.FeaturesId)
                {
                    DeviceFeaturesCreateDto DeviceFeatures = new DeviceFeaturesCreateDto();
                    DeviceFeatures.DeviceId = updateDto.Id;
                    DeviceFeatures.FeatureId = item;
                    DeviceFeatures.IsDelete = false;
                    await _baseService.CreateAsync<DeviceFeature, DeviceFeaturesCreateDto>(DeviceFeatures);
                }

                //var area = _repository.Filter<Areas>(x => x.Id == updateDto.AreaId).FirstOrDefault();
                //string method = "PUT";
                //var data = new { data = new string[] { updateDto.Id.ToString(), updateDto.RstpLink, _userIdentity.CompId.ToString(), area.Code, updateDto.DeviceCode } };
                //await _updateNotificationService.SyncData(method, data);
            }
            return 0;
        }

        public async Task<int> UpdateDelete(int id)
        {
            var ft = _repository.FindAsync<DeviceFeature>(x => x.DeviceId == id && x.IsDelete == false);
            ft.Result.IsDelete = true;
            await _repository.UpdateAsync<DeviceFeature>(ft.Result);

            var dto = await _baseService.FindAsync<Device, DeviceUpdateDto>(x => x.Id == id);
            dto.IsDelete = true;
            await _baseService.UpdateAsync<Device, DeviceUpdateDto>(id, dto);

            string method = "DELETE";
            var data = new { data = new string[] { dto.Id.ToString(), "Delete camera" } };
            await _updateNotificationService.SyncData(method, data);

            return 0;
        }

        public async Task<DeviceDetailDto> GetById(int Id)
        {
            DeviceDetailDto DeviceDetailDto = new DeviceDetailDto();
            var listFeaturesId = _repository.Filter<DeviceFeature>(x => x.DeviceId == Id && x.IsDelete == false).OrderByDescending(x => x.Id).Select(x => x.FeatureId).ToList();
            var a = await _baseService.FindAsync<ViewDeviceFeatures, DeviceDetailDto>(Id);
            a.MjpegPort = _appSettings.MjpegServer + a.MjpegPort;
            a.FeaturesId = listFeaturesId;

            return a;
        }

        public async Task<int?> CreatePolygon(PolygonCreateDto createDto, bool isExploiting = false)
        {
            if (createDto.Type == 1)
            {
                var card = _repository.Find<Polygon>(i => i.CamId == createDto.CamId && i.Type == createDto.Type);
                var device = _repository.Find<Device>(i => i.Id == createDto.CamId);
                var deviceFeature = _repository.Find<DeviceFeature>(i => i.DeviceId == createDto.CamId && i.IsDelete == false);
                var deviceFeatureName = _repository.Find<Features>(i => i.Id == deviceFeature.FeatureId && i.IsDelete == false);

                if (card != null)
                {
                    await _repository.DeleteAsync<Polygon>(card);
                }
                DrawPolygon a = new DrawPolygon();
                var A = createDto.CanvasPoitArray.Replace('[', ' ').Replace(']', ' ');
                string[] u = A.Trim().Split("\",");
                List<active_area> lstResult = new List<active_area>();

                foreach (var item in u)
                {
                    string u2 = item.Trim().Replace('"', ' ');
                    string[] u1 = item.Trim().Split(",");
                    var t = u1[0].Replace('"', ' ');
                    var t2 = u1[1].Replace('"', ' ');
                    Double x = Int32.Parse(t.Trim());
                    Double y = Int32.Parse(t2.Trim());
                    active_area uploadFaceDto = new active_area();
                    uploadFaceDto.x = x / createDto.CanvasPoitArrayWieght;
                    uploadFaceDto.y = y / createDto.CanvasPoitArrayHight;
                    lstResult.Add(uploadFaceDto);
                }
                active_area uploadFaceDto1 = new active_area();

                var p = lstResult.ToList();
                var h = p[0];
                var h2 = p[1];
                var h1 = new active_area() { x = h.x, y = h2.y };
                var h3 = new active_area() { x = h2.x, y = h.y };

                a.active_area = new List<active_area>() { h, h1, h2, h3 };
                a.id = createDto.CamId;

                using var client = new HttpClient();
                var myContent = JsonConvert.SerializeObject(a);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync(_appSettings.ApiUrlPolygon, byteContent).Result;

                await _baseService.CreateAsync<Polygon, PolygonCreateDto>(createDto);
                return createDto.Id;
            }
            else
            {
                var card = _repository.Find<Polygon>(i => i.CamId == createDto.CamId && i.Type == createDto.Type);
                var device = _repository.Find<Device>(i => i.Id == createDto.CamId);
                var deviceFeature = _repository.Find<DeviceFeature>(i => i.DeviceId == createDto.CamId && i.IsDelete == false);
                var deviceFeatureName = _repository.Find<Features>(i => i.Id == deviceFeature.FeatureId && i.IsDelete == false);

                if (card != null)
                {
                    await _repository.DeleteAsync<Polygon>(card);
                }

                DrawPolygon a = new DrawPolygon();
                var A = createDto.CanvasPoitArray.Replace('[', ' ').Replace(']', ' ');
                string[] u = A.Trim().Split("\",");
                List<active_area> lstResult = new List<active_area>();

                foreach (var item in u)
                {
                    string u2 = item.Trim().Replace('"', ' ');
                    string[] u1 = item.Trim().Split(",");
                    var t = u1[0].Replace('"', ' ');
                    var t2 = u1[1].Replace('"', ' ');
                    Double x = Int32.Parse(t.Trim());
                    Double y = Int32.Parse(t2.Trim());
                    active_area uploadFaceDto = new active_area();
                    uploadFaceDto.x = x / createDto.CanvasPoitArrayWieght;
                    uploadFaceDto.y = y / createDto.CanvasPoitArrayHight;
                    lstResult.Add(uploadFaceDto);
                }

                active_area uploadFaceDto1 = new active_area();

                var p = lstResult.ToList<active_area>();

                a.active_area = new List<active_area>();
                a.active_area = p;
                a.id = createDto.CamId;

                using var client = new HttpClient();
                var myContent = JsonConvert.SerializeObject(a);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync(_appSettings.ApiUrlPolygonCancel, byteContent).Result;

                await _baseService.CreateAsync<Polygon, PolygonCreateDto>(createDto);
                return createDto.Id;
            }
        }

        public async Task<PolygonCreateDto> GetPolygonByCamId(int camPolygonId, int type)
        {
            var listPolygon = _repository.Filter<Polygon>(x => x.CamId == camPolygonId && x.Type == type).OrderByDescending(x => x.Id).FirstOrDefault();
            if (listPolygon == null)
            {
                PolygonCreateDto polygonCreate = new PolygonCreateDto();
                polygonCreate.CamId = 0;
                polygonCreate.CanvasPoitArray = "[[]]";
                return polygonCreate;
            }
            var result = await _baseService.FindAsync<Polygon, PolygonCreateDto>(listPolygon.Id);
            return result;
        }

        public async Task<int> UpdateDeletePolygon(int id)
        {
            var card = _repository.Find<Polygon>(i => i.CamId == id);
            if (card != null)
            {
                DrawPolygon content = new DrawPolygon();
                content.active_area = new List<active_area>();
                content.id = id;
                using var client = new HttpClient();

                var myContent = JsonConvert.SerializeObject(content);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync(_appSettings.ApiUrlPolygonCancel, byteContent).Result;
                await _repository.DeleteAsync<Polygon>(card);
            }
            return 0;
        }

        //Lấy danh sách thiết bị online
        public List<DeviceDetailDto> GetDeviceOnline(int comId)
        {
            var lstDevice = _baseService.Filter<Device, DeviceDetailDto>(i => i.CompId == comId && i.IsDelete == false && i.Status != 0).ToList();
            return lstDevice;
        }

        //Lấy danh sách thiết bị theo view config
        public List<CamViewConfigDto> GetDeviceByCamViewConfig()
        {
            var userId = _userIdentity.UserId;
            var lstConfig = _baseService.Filter<CamViewConfig, CamViewConfigDto>(i => i.UserId == userId).ToList();
            return lstConfig;
        }

        //Lấy id view config
        public int GetIdCamViewConfig(int position)
        {
            var userId = _userIdentity.UserId;
            var dtoConfig = _baseService.Filter<CamViewConfig, CamViewConfigDto>(i => i.UserId == userId && i.Position == position).FirstOrDefault();
            if (dtoConfig == null)
            {
                return 0;
            }
            return dtoConfig.Id;
        }

        //Update view config
        public async Task<int> CreateCamViewConfig(CamViewConfigUpdateDto createDto)
        {
            await _baseService.CreateAsync<CamViewConfig, CamViewConfigUpdateDto>(createDto);
            return 0;
        }

        //Update view config
        public async Task<int> UpdateCamViewConfig(int id, CamViewConfigUpdateDto updateDto)
        {
            await _baseService.UpdateAsync<CamViewConfig, CamViewConfigUpdateDto>(id, updateDto);
            return 0;
        }
    }
}
