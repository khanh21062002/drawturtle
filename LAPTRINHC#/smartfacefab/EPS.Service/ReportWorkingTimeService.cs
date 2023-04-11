using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.ReportWorkingTime;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Service;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EPS.Service
{
    public class ReportWorkingTimeService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IOptions<AppSettings> _settings;
        private ShiftTimeService _shiftTimeService;

        public ReportWorkingTimeService(EPSRepository repository, IMapper mapper, IOptions<AppSettings> settings, ShiftTimeService shiftTimeService)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _settings = settings;
            _shiftTimeService = shiftTimeService;
        }
        public async Task<PagingResult<ReportWorkingTimeGridDto>> GetListReportWorkingTime(ReportWorkingTimeGridPagingDto pagingModel)
        {

            //Giá trị mặc định là 480p(8:00 sáng) và 1050p(17:30 chiều)
            const int DEFAULT_START_TIME = 480;
            const int DEFAULT_END_TIME = 480;
            //Giá trị mặc định gio nghi trua
            const int DEFAULT_START_FREE_TIME = 720; //12h 
            const int DEFAULT_END_FREE_TIME = 810; ; //13h30

            int startWorkingTime = 0;
            int endWorkingTime = 0;
            int startLunchBreak = 0;
            int endLunchBreak = 0;

            int? compId = pagingModel.FilterCompId;
            if (compId.HasValue)
            {
                var shiftTime = await _shiftTimeService.GetShiftTimeByCompId(compId.Value);
                if (shiftTime != null)
                {
                    startWorkingTime = convertStringTimeToMinute(shiftTime.BeginShiftTime);
                    endWorkingTime = convertStringTimeToMinute(shiftTime.EndShiftTime);
                    startLunchBreak = convertStringTimeToMinute(shiftTime.BeginFreeShiftTime);
                    endLunchBreak = convertStringTimeToMinute(shiftTime.EndFreeShiftTime);
                }
            }
            if (startWorkingTime == 0) startWorkingTime = DEFAULT_START_TIME;
            if (endWorkingTime == 0) endWorkingTime = DEFAULT_END_TIME;
            if (startLunchBreak == 0) startLunchBreak = DEFAULT_START_FREE_TIME;
            if (endLunchBreak == 0) endLunchBreak = DEFAULT_END_FREE_TIME;

            // cac filter
            string filterText = pagingModel.FilterText;
            int filterCompId = pagingModel.FilterCompId.HasValue ? pagingModel.FilterCompId.Value : 0;
            int filterDeptId = pagingModel.FilterDeptId.HasValue ? pagingModel.FilterDeptId.Value : 0;

            string sqlQuery = "EXEC [dbo].[GenerateWorkingDay] "
                + startWorkingTime + " , "
                + endWorkingTime + " , "
                + startLunchBreak + " , "
                + endLunchBreak + " , ";
            //add các điều kiện tìm kiếm ( nếu có)
            if (!string.IsNullOrEmpty(filterText))
            {
                filterText = filterText.Trim();
                sqlQuery = sqlQuery + "@FilterText = N'" + filterText + "' , ";
            }

            if (pagingModel.FilterDateFrom.HasValue)
            {
                sqlQuery = sqlQuery + "@FilterDateFrom = '" + pagingModel.FilterDateFrom.Value + "' , ";
                sqlQuery = sqlQuery + "@StartDate = '" + pagingModel.FilterDateFrom.Value + "' , ";
            } else
            {
                var previousDay = DateTime.Today.AddDays(-1);
                sqlQuery = sqlQuery + "@StartDate = '" + previousDay + "' , ";
            }
            if (pagingModel.FilterDateTo.HasValue)
            {
                sqlQuery = sqlQuery + "@FilterDateTo = '" + pagingModel.FilterDateTo.Value + "' , ";
                sqlQuery = sqlQuery + "@EndDate = '" + pagingModel.FilterDateTo.Value + "' , ";
            } else
            {
                var toDAY = DateTime.Today;
                sqlQuery = sqlQuery + "@EndDate = '" + toDAY + "' , ";
            }
            sqlQuery = sqlQuery + "@FilterCompId = " + filterCompId + " , ";
            sqlQuery = sqlQuery + "@FilterDeptId = " + filterDeptId + " ";
            IQueryable<ReportWorkingTimeGridDto> query = _baseService.FromSqlRaw<v_ReportWorkingTime, ReportWorkingTimeGridDto>(sqlQuery);
            var exData = query.AsEnumerable();
         

            var result = new PagingResult<ReportWorkingTimeGridDto>() { PageSize = pagingModel.ItemsPerPage, CurrentPage = pagingModel.Page };

            result.TotalRows = exData.Count();

            if (pagingModel.StartingIndex > 0)
            {
                exData = exData.Skip(pagingModel.StartingIndex);
            }

            if (pagingModel.ItemsPerPage != -1 && pagingModel.ItemsPerPage <= 0)
            {
                pagingModel.ItemsPerPage = 100;
            }

            if (pagingModel.ItemsPerPage > 0)
            {
                exData = exData.Take(pagingModel.ItemsPerPage);
            }


            result.Data = exData.ToList();

            return result;
        }

        private int convertStringTimeToMinute(string time)
        {
            if (!String.IsNullOrEmpty(time))
            {
                string[] timeArr = time.Split(":");
                if (timeArr.Length == 2)
                {
                    int hourToMinute = 0;
                    string hour = timeArr[0].Trim();
                    if (Int32.TryParse(hour, out hourToMinute))
                    {
                        // you know that the parsing attempt
                        // was successful
                        hourToMinute = hourToMinute * 60;
                    }

                    int minute = 0;
                    string minuteStr = timeArr[1].Trim();
                    if (Int32.TryParse(minuteStr, out minute))
                    {
                        return hourToMinute + minute;
                    } 
                }
            }

            return 0;
        }

    }
}
