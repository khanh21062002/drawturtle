using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.CalculateWorkingHour;
using EPS.Service.Dtos.DayOff;
using EPS.Service.Dtos.Holidays;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.WorkingHours;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class CalculateWorkingHourService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;


        public CalculateWorkingHourService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        public async Task<int> ExecuteCalculate(CalculateDto dto)
        {
            int? compId = _userIdentity.CompId;
            //Thuc hien tinh toan cham cong tu cac dieu kien trong dto
            if (!dto.DateFrom.HasValue || !dto.DateTo.HasValue)
            {
                throw new EPSException("CalculateWorkingHourService.Message.ExecuteCalculate");
            }
            DateTime dateFrom = dto.DateFrom.Value;
            DateTime dateTo = dto.DateTo.Value;
            if (dateFrom > dateTo)
            {
                throw new EPSException("CalculateWorkingHourService.Message.ChooseTime");
            }
            if (!compId.HasValue)
            {
                throw new EPSException("CalculateWorkingHourService.Message.UserLogin");
            }
            double diffrenceDays = (dateTo - dateFrom).TotalDays;
            if (diffrenceDays > 45)
            {
                throw new EPSException("CalculateWorkingHourService.Message.Time");
            }


            int rowAffected = 0;
            if (dto.Type == 0)
            {
                //Tính công cả công ti
                string sqlQuery = " EXEC [dbo].[CalculateOvertimeHours] "
                    + " @DayFrom = '" + dateFrom + "', "
                    + " @DayTo = '" + dateTo + "', "
                    + " @CompId = '" + compId + "' ";

                rowAffected = _baseService.ExecuteNonQuery(sqlQuery);
                return rowAffected;
            }
            if (dto.Type == 1)
            {
                //Tính công cả phòng ban
                if (!dto.DeptId.HasValue)
                {
                    throw new EPSException("CalculateWorkingHourService.Message.DepartmentCalculate");
                }

                string sqlQuery = " EXEC [dbo].[CalculateOvertimeHoursFORDEPARTMENT] "
                    + " @DayFrom = '" + dateFrom + "', "
                    + " @DayTo = '" + dateTo + "', "
                    + " @CompId = '" + compId + "', "
                    + " @DeptId = '" + dto.DeptId + "' ";

                rowAffected = _baseService.ExecuteNonQuery(sqlQuery);
                return rowAffected;
            }

            if (dto.Type == 2)
            {
                if (!dto.PersonId.HasValue)
                {
                    throw new EPSException("CalculateWorkingHourService.Message.EmployeeCalculate");
                }
                //Tính công user
                string sqlQuery = " EXEC [dbo].[CalculateOvertimeHoursFORPERSON] "
                    + " @DayFrom = '" + dateFrom + "', "
                    + " @DayTo = '" + dateTo + "', "
                    + " @CompId = '" + compId + "', "
                    + " @PersonId = '" + dto.PersonId + "' ";

                rowAffected = _baseService.ExecuteNonQuery(sqlQuery);
                return rowAffected;
            }

            return rowAffected;
        }

        public async Task<List<WorkingHoursGridDto>> ReportDetails(CalculateDto dto)
        {
            int? compId = _userIdentity.CompId;
            if (!compId.HasValue)
            {
                throw new EPSException("CalculateWorkingHourService.Message.UserLogin");
            }

            if (!dto.DeptId.HasValue || !dto.PersonId.HasValue || !dto.DateFrom.HasValue || !dto.DateTo.HasValue)
            {
                throw new EPSException("CalculateWorkingHourService.Message.FullInformation");
            }
            DateTime dateFrom = dto.DateFrom.Value;
            DateTime dateTo = dto.DateTo.Value;
            if (dateFrom > dateTo)
            {
                throw new EPSException("CalculateWorkingHourService.Message.ChooseTime");
            }
            double diffrenceDays = (dateTo - dateFrom).TotalDays;
            if (diffrenceDays > 45)
            {
                throw new EPSException("CalculateWorkingHourService.Message.Time");
            }
            DateTime dateToPlus1 = dateTo.AddDays(1);
            List<Expression<Func<WorkingHoursGridDto, bool>>> predicates = new List<Expression<Func<WorkingHoursGridDto, bool>>>();
            predicates.Add(x => x.CompId == compId);
            predicates.Add(x => x.PersonId == dto.PersonId);
            predicates.Add(x => x.Day >= dateFrom && x.Day < dateToPlus1);

            return await _baseService.Filter<WorkingHours, WorkingHoursGridDto>(predicates.ToArray()).ToListAsync();
        }

    }
}
