using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Holidays;
using EPS.Service.Dtos.OverTime;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class OverTimeService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public OverTimeService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        //list
        public async Task<PagingResult<OverTimeGridDto>> GetOverTimes(OverTimeGridPagingDto pagingModel)
        {
            var x = await _baseService.FilterPagedAsync<OverTime, OverTimeGridDto>(pagingModel);
            foreach (var item in x.Data)
            {
                double totalTimeOTVl = 0;
                var compId = item.COMP_ID;

                DateTime? dateSearch = item.DATE_OT;
                if (dateSearch.HasValue)
                {
                    var dayOfWeek = (int)dateSearch.Value.DayOfWeek;
                    var wordDetail = await _repository.Filter<WorkCalendarDetail>(x => x.WorkCalendar.CompId == compId && x.WorkCalendar.IsDelete == false
                                        && (x.WorkCalendar.DateFrom <= dateSearch && x.WorkCalendar.DateTo >= dateSearch && x.DayOfWeek == dayOfWeek)).ToListAsync();

                    if (wordDetail.Count == 0)
                    {
                        var totalCoEf = item.COEFFICIENT;
                        item.COEFFICIENT_VIEW = totalCoEf.ToString();

                        if (item.SUM_OT.HasValue)
                        {
                            totalTimeOTVl = totalCoEf * item.SUM_OT.Value;
                            item.TOTAL_TIME_OT = String.Format("{0:0.00}", totalTimeOTVl);
                        }
                        continue;
                    }
                    var number = wordDetail.First().Number;

                    if (number.HasValue)
                    {
                        item.COEFFICIENT_DATE = number.Value;
                        var totalCoEf = item.COEFFICIENT_DATE + item.COEFFICIENT;
                        item.COEFFICIENT_VIEW = totalCoEf.ToString() + " (" + item.COEFFICIENT + "+" + item.COEFFICIENT_DATE + ")";

                        if (item.SUM_OT.HasValue)
                        {
                            totalTimeOTVl = totalCoEf * item.SUM_OT.Value;
                            item.TOTAL_TIME_OT = String.Format("{0:0.00}", totalTimeOTVl);
                        }
                    }
                }
            }
            return x;
        }

        //create
        public async Task<int> CreateOverTime(OverTimeCreateDto overTimeCreate, bool isExploiting = false)
        {
            //var count = await _repository.CountAsync<OverTime>(x => x.DATE_OT == overTimeCreate.DATE_OT && x.PERSON_ID == overTimeCreate.PERSON_ID );
            //if (count > 0)
            //{
            //    var error = "Ngày OT ngoài giờ đã tồn tại";
            //    throw new EPSException(error);
            //}
            await _baseService.CreateAsync<OverTime, OverTimeCreateDto>(overTimeCreate);
            return 0;
        }

        //detail
        public async Task<OverTimeDetailDto> GetOverTimeById(int id)
        {
            return await _baseService.FindAsync<OverTime, OverTimeDetailDto>(x => x.ID == id);
        }

        //update
        public async Task<int> UpdateOverTime(int id, OverTimeUpdateDto editedOverTime)
        {
            //var count = await _repository.CountAsync<OverTime>(x => x.DATE_OT == editedOverTime.DATE_OT && x.ID != id);
            //if (count > 0)
            //{
            //    var error = "Ngày OT ngoài giờ đã tồn tại";
            //    throw new EPSException(error);
            //}
            return await _baseService.UpdateAsync<OverTime, OverTimeUpdateDto>(id, editedOverTime);
        }

        //delete
        public async Task<int> DeleteOverTime(int id)
        {
            return await _baseService.DeleteAsync<OverTime, int>(id);
        }
        public async Task<bool> CheckHolidays(string day, int? compId)
        {
            bool rs = false;
            string format = "yyyy-MM-dd";
            DateTime dateTime;
            if (DateTime.TryParseExact(day, format, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dateTime))
            {
                DateTime startOfDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
                rs = await _baseService.AnyAsync<Holidays, HolidaysDetailDto>(x => x.Date == startOfDate && x.CompId == compId && x.IsDelete == false);
            }
            else
            {
                return false;
            }

            return rs;

        }
    }
}
