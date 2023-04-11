using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.ShiftTime;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class ShiftTimeService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public ShiftTimeService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<ShiftTimeGridDto>> GetShiftTimes(ShiftTimeGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<ShiftTime, ShiftTimeGridDto>(pagingModel);
        }

        public async Task<int> CreateShiftTime(ShiftTimeCreateDto shiftTimeCreate)
        {
            shiftTimeCreate.IsDelete = false;
            await _baseService.CreateAsync<ShiftTime, ShiftTimeCreateDto>(shiftTimeCreate);
            return shiftTimeCreate.Id;
        }

        public async Task<ShiftTimeDetailDto> GetShiftTimeById(int id)
        {
            return await _baseService.FindAsync<ShiftTime, ShiftTimeDetailDto>(id);
        }

        public async Task<ShiftTimeDetailDto> GetShiftTimeByCompId(int compId)
        { 
            return await _baseService.FindAsync<ShiftTime, ShiftTimeDetailDto>(x => x.IsDelete == false && x.CompId == compId) ;
        }

        public async Task<int> UpdateShiftTime(int id, ShiftTimeUpdateDto editedShiftTime)
        {
            return await _baseService.UpdateAsync<ShiftTime, ShiftTimeUpdateDto>(id, editedShiftTime);
        }
        public async Task<int> UpdateDelete(int id)
        {
            var shiftTime = await _baseService.FindAsync<ShiftTime, ShiftTimeUpdateDto>(x => x.Id == id);
            shiftTime.IsDelete = true;
            return await _baseService.UpdateAsync<ShiftTime, ShiftTimeUpdateDto>(id, shiftTime);
        }
        public async Task<int> UpdateDeletes(int[] ids)
        {
            foreach (var item in ids)
            {

                var shiftTime = await _baseService.FindAsync<ShiftTime, ShiftTimeUpdateDto>(x => x.Id == item);
                if (shiftTime != null)
                {
                    shiftTime.IsDelete = true;
                    await _baseService.UpdateAsync<ShiftTime, ShiftTimeUpdateDto>(shiftTime.Id, shiftTime);
                }
            }
            return 0;
        }
    }
}
