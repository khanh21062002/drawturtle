using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.DayOff;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class DayOffService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public DayOffService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<DayOffGridDto>> GetAllDayOffs(DayOffGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<DayOff, DayOffGridDto>(pagingModel);
        }
        public async Task<int> CreateDayOffs(DayOffCreateDto createDto, bool isExploiting = false)
        {
           
                TimeSpan Time = createDto.DateTo - createDto.DateFrom;
                int TongSoNgay = Time.Days;

            
            if (createDto.IsHalfDay ==true && TongSoNgay > 1)
            {
                var errors1 = "CompanyService.Message.Dayoff";
                throw new EPSException(errors1);
            }
            createDto.IsDelete = false;
            await _baseService.CreateAsync<DayOff, DayOffCreateDto>(createDto);
            return createDto.ID;
        }

        public async Task<DayOffDetailDto> GetDayOffsById(int id)
        {
            return await _baseService.FindAsync<DayOff, DayOffDetailDto>(id);
        }

        public async Task<int> UpdateDayOffs(int id, DayOffUpdateDto editDto)
        {
            TimeSpan Time = editDto.DateTo - editDto.DateFrom;
            int TongSoNgay = Time.Days;


            if (editDto.IsHalfDay == true && TongSoNgay > 1)
            {
                var errors1 = "CompanyService.Message.Dayoff";
                throw new EPSException(errors1);
            }
            return await _baseService.UpdateAsync<DayOff, DayOffUpdateDto>(id, editDto);
        }

        public async Task<int> UpdateDelete(int id)
        {
            var dayoffs = await _baseService.FindAsync<DayOff, DayOffUpdateDto>(x => x.ID == id);
            dayoffs.IsDelete = true;
            return await _baseService.UpdateAsync<DayOff, DayOffUpdateDto>(id, dayoffs);
        }
        public async Task<int> UpdateDeletes(int[] ids)
        {
            foreach (var item in ids)
            {

                var dayOff = await _baseService.FindAsync<DayOff, DayOffUpdateDto>(x => x.ID == item);
                if (dayOff != null)
                {
                    dayOff.IsDelete = true;
                    await _baseService.UpdateAsync<DayOff, DayOffUpdateDto>(dayOff.ID, dayOff);
                }
            }
            return 0;
        }
    }
}
