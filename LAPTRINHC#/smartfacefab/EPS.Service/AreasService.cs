using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Areas;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class AreasService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IUserIdentity<int> _userIdentity;
        private IMapper _mapper;

        public AreasService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _userIdentity = userIdentity;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<AreasGridDto>> GetAllAreas(AreasGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Areas, AreasGridDto>(pagingModel);
        }

        //get by id

        public async Task<AreasDetailDto> GetById(int id, bool isExploiting = false)
        {
            try
            {
                return await _baseService.FindAsync<Areas, AreasDetailDto>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //create
        public async Task<int?> CreateAreas(AreasCreateDto areasCreateDto, bool isExploiting = false)
        {
            var count = await _repository.CountAsync<Areas>(x => x.Code == areasCreateDto.Code.Trim() && x.IsDelete == false && x.CompId == areasCreateDto.CompId);
            if (count > 0)
            {
                //var errors1 = "Mã khu vực đã tồn tại";
                var errors1 = "GroupService.Message.AreaCode";

                throw new EPSException(errors1);
            }
            else
            {
                areasCreateDto.IsDelete = false;
                await _baseService.CreateAsync<Areas, AreasCreateDto>(areasCreateDto);
                return areasCreateDto.Id;
            }
        }

        //update
        public async Task<int?> UpdateAreas(int id, AreasUpdateDto areasUpdateDto, bool isExploiting = false)
        {
            var count = await _repository.CountAsync<Areas>(x => x.Code == areasUpdateDto.Code.Trim() && x.IsDelete == false && x.Id != id && x.CompId == areasUpdateDto.CompId);
            if (count > 0)
            {
                var errors1 = "GroupService.Message.AreaCode";
                throw new EPSException(errors1);
            }
            else
            {
                areasUpdateDto.IsDelete = false;
                await _baseService.UpdateAsync<Areas, AreasUpdateDto>(id, areasUpdateDto);
                return areasUpdateDto.Id;
            }
        }

        //sort delete
        public async Task<int?> DeleteAreas(int? id, bool isExploiting = false)
        {
            var vehic = await _repository.CountAsync<AreaMachine>(x => x.AreaId == id && x.IsDelete == false);
            var countDevice = await _repository.CountAsync<Device>(x => x.AreaId == id && x.IsDelete == false);

            if (vehic > 0 || countDevice > 0)
            {
                throw new Exception("Constant.Error1");
            }
            else
            {
                var vehicleRemove = await _baseService.FindAsync<Areas, AreasUpdateDto>(x => x.Id == id);
                vehicleRemove.IsDelete = true;
                await _baseService.UpdateAsync<Areas, AreasUpdateDto>(id, vehicleRemove);
                return vehicleRemove.Id;
            }
        }
    }
}
