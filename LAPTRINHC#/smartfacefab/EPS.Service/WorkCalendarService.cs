using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.WorkCalendar;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class WorkCalendarService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public WorkCalendarService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;

        }
        public async Task<PagingResult<WorkCalendarGridDto>> GetAll(WorkCalendarGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<WorkCalendar, WorkCalendarGridDto>(pagingModel);
        }

        public async Task<List<WorkCalendarDetailDto>> GetAllNoPaging()
        {
            int compId = _userIdentity.CompId.Value;
            return await _baseService.Filter<WorkCalendar, WorkCalendarDetailDto>(x => x.CompId == compId).ToListAsync();
        }

        public async Task<int> Create(WorkCalendarCreateDto createDto, bool isExploiting = false)
        {

            var count1 = await _repository.CountAsync<WorkCalendar>(x => x.CompId == createDto.CompId  && x.IsDelete == false && ((x.DateFrom <= createDto.DateFrom && x.DateTo >= createDto.DateFrom) || (x.DateFrom >= createDto.DateFrom && x.DateFrom <= createDto.DateTo)));
            if (count1 > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_EMPLOYEE1;
                throw new EPSException(errors1);
            }
            createDto.IsDelete = false;
            await _baseService.CreateAsync<WorkCalendar, WorkCalendarCreateDto>(createDto);
            return createDto.ID;
        }

        public async Task<WorkCalendarDetailDto> GetDetails(int id)
        {
            return await _baseService.FindAsync<WorkCalendar, WorkCalendarDetailDto>(id);
        }
        //List danh sách chi tiết 
        public async Task<List<WCalendarDetailGridDto>> GetListNotiDetails(int workcalendarId)
        {
            List<Expression<Func<WCalendarDetailGridDto, bool>>> predicates = new List<Expression<Func<WCalendarDetailGridDto, bool>>>();
            predicates.Add(x => x.WorkCalendarId == workcalendarId && x.IsDelete == false);
            return await _baseService.Filter<WorkCalendarDetail, WCalendarDetailGridDto>(predicates.ToArray()).ToListAsync();
        }
        public async Task<int> UpdateById(int id, WorkCalendarUpdateDto editDto)
        {
            return await _baseService.UpdateAsync<WorkCalendar, WorkCalendarUpdateDto>(id, editDto);
        }

        public async Task<int> UpdateDelete(int id)
        {



            var workingShiftTimes = await _baseService.FindAsync<WorkCalendar, WorkCalendarDetailDto>(id);


            var count1 = await _repository.CountAsync<WorkCalendar>(x => x.ID == id && x.IsDelete == false && workingShiftTimes.DateFrom <= DateTime.Now && workingShiftTimes.DateTo >= DateTime.Now);

            var count = await _repository.CountAsync<WorkCalendar>(x => x.ID == id && x.IsDelete == false && workingShiftTimes.DateFrom <= DateTime.Now && workingShiftTimes.DateTo <= DateTime.Now);

            //var count1 = await _baseService.FindAsync<WorkCalendar, WorkCalendarDetailDto>(id);
            if (count1 > 0)
            {
                var dayoffs = await _baseService.FindAsync<WorkCalendar, WorkCalendarUpdateDto>(x => x.ID == id);
                dayoffs.DateTo = DateTime.Now;
                await _baseService.UpdateAsync<WorkCalendar, WorkCalendarUpdateDto>(id, dayoffs);
            }
            else
            {
                if (count > 0)
                {
                    var errors1 = Constant.ErrorMessage.ERROR_DELETEDATE;
                    throw new EPSException(errors1);
                }
                else
                {
                    var dayoffs = await _baseService.FindAsync<WorkCalendar, WorkCalendarUpdateDto>(x => x.ID == id);
                    dayoffs.IsDelete = true;
                     await _baseService.UpdateAsync<WorkCalendar, WorkCalendarUpdateDto>(id, dayoffs);
                }
            }
           
            return 0;
            
        }
        public async Task<int> UpdateDeletes(int[] ids)
        {
            foreach (var item in ids)
            {

                var dayOff = await _baseService.FindAsync<WorkCalendar, WorkCalendarUpdateDto>(x => x.ID == item);
                if (dayOff != null)
                {
                    dayOff.IsDelete = true;
                    await _baseService.UpdateAsync<WorkCalendar, WorkCalendarUpdateDto>(dayOff.ID, dayOff);
                }
            }
            return 0;
        }


        public async Task<int> CreateDetails(WCalendarDetailCreateDto createDto, bool isExploiting = false)
        {
            createDto.IsDelete = false;
            await _baseService.CreateAsync<WorkCalendarDetail, WCalendarDetailCreateDto>(createDto);
            return createDto.ID;
        }

        public async Task<int> UpdateDetailsById(int id, WCalendarDetailCreateDto editDto)
        {
            return await _baseService.UpdateAsync<WorkCalendarDetail, WCalendarDetailCreateDto>(id, editDto);
        }


    }
}
