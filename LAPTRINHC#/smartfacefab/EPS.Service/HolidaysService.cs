using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Holidays;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class HolidaysService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public HolidaysService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }
        public async Task<PagingResult<HolidaysGridDto>> GetAllHolidays(HolidaysGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Holidays, HolidaysGridDto>(pagingModel);
        }
        public async Task<int> CreateHolidays(HolidaysCreateDto createDto, bool isExploiting = false)
        {
            createDto.IsDelete = false;
            await _baseService.CreateAsync<Holidays, HolidaysCreateDto>(createDto);
            return createDto.ID;
        }

        public async Task<HolidaysDetailDto> GetHolidaysById(int id)
        {
            return await _baseService.FindAsync<Holidays, HolidaysDetailDto>(id);
        }

        public async Task<int> UpdateHolidays(int id, HolidaysUpdateDto editDto)
        {
            return await _baseService.UpdateAsync<Holidays, HolidaysUpdateDto>(id, editDto);
        }

        public async Task<int> UpdateDelete(int id)
        {
            var holidays = await _baseService.FindAsync<Holidays, HolidaysUpdateDto>(x => x.ID == id);
            holidays.IsDelete = true;
            return await _baseService.UpdateAsync<Holidays, HolidaysUpdateDto>(id, holidays);
        }
        public async Task<int> UpdateDeletes(int[] ids)
        {
            foreach (var item in ids)
            {

                var holidays = await _baseService.FindAsync<Holidays, HolidaysUpdateDto>(x => x.ID == item);
                if (holidays != null)
                {
                    holidays.IsDelete = true;
                    await _baseService.UpdateAsync<Holidays, HolidaysUpdateDto>(holidays.ID, holidays);
                }
            }
            return 0;
        }
    }
}
