using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.CalculateWorkingHour;
using EPS.Service.Dtos.Holidays;
using EPS.Service.Dtos.OverTimeHours;
using EPS.Service.Dtos.WorkCalendar;
using EPS.Service.Dtos.WorkingShiftTimes;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class OverTimeHoursService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public OverTimeHoursService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;

        }
        public async Task<PagingResult<OverTimeHoursGridDto>> GetAll(OverTimeHoursGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<OverTimeHours, OverTimeHoursGridDto>(pagingModel);
        }
        public async Task<int> Create(OverTimeHoursCreateDto createDto, bool isExploiting = false)
        {
           
            //lich lam viec
            double workcalendar = 0;
            var workCal = await _baseService.FindAsync<WorkCalendar, WorkCalendarDetailDto>(x => x.CompId == _userIdentity.CompId && x.DateFrom <= createDto.Day && x.DateTo >= createDto.Day && x.IsDelete == false);
            if (workCal != null)
            {
                int dayOfWeek = (int)(createDto.Day.Value.DayOfWeek);
                var workCalDetails = await _baseService.FindAsync<WorkCalendarDetail, WCalendarDetailDetailDto>(x => x.WorkCalendarId == workCal.ID && x.DayOfWeek == dayOfWeek && x.IsDelete == false);
                if (workCalDetails != null)
                {
                    workcalendar = workCalDetails.Number.Value;
                }
            }

            double shiftNumber = 0;
            var workingShift = await _baseService.FindAsync<WorkingShiftTimes, WorkingShiftTimesDetailDto>(x => x.ID == createDto.WorkingShiftId);
            if (workingShift != null && workingShift.OT != null)
            {
                shiftNumber = workingShift.OT.Value;
            }
            double number = shiftNumber + workcalendar;
            //then the shif
            createDto.Number = number;
            string numberPartion = number.ToString();
            if (shiftNumber > 0 && workcalendar > 0)
            {
                numberPartion = numberPartion + "(" + shiftNumber + " + " + workcalendar  + ")";
            }


            //Get all number of this day
            //first is holidays
            double holidayNumber = 0;
            var holidays = await _baseService.FindAsync<Holidays, HolidaysDetailDto>(x => x.Date == createDto.Day && x.CompId == _userIdentity.CompId );
            if (holidays != null)
            {
                holidayNumber = holidays.CoEf.Value;
                //createDto.NumberPartion = holidayNumber.ToString();
                //then the shif
                double number1 = shiftNumber + holidayNumber;
                createDto.Number = number1;
                string numberPartion1 = number1.ToString();
                if (shiftNumber > 0 && holidayNumber > 0)
                {
                    numberPartion1 = numberPartion1 + "(" + shiftNumber + " + " + holidayNumber + ")";
                }
                createDto.NumberPartion = numberPartion1;
                createDto.Total = number1 * createDto.TotalReal;
                // 5 is manaully adding
                createDto.Type = 5;
                createDto.NoteDev = "Manually Adding:  Holidays: ";
                if (!string.IsNullOrEmpty(holidays.Description))
                {
                    createDto.NoteDev = createDto.NoteDev + holidays.Description;
                }
                await _baseService.CreateAsync<OverTimeHours, OverTimeHoursCreateDto>(createDto);
                return createDto.ID;
            }

            createDto.NumberPartion = numberPartion;
            createDto.Total = number * createDto.TotalReal;
            createDto.NoteDev = "Manually Adding: " + DateTime.Now;
            // 5 is manaully adding
            createDto.Type = 5;
            createDto.IsDelete = false;
            await _baseService.CreateAsync<OverTimeHours, OverTimeHoursCreateDto>(createDto);
            return createDto.ID;
        }
        private static string getDayOfWeekInVietnamese(DateTime? time)
        {
            if (time.HasValue)
            {
                int dayOfWeek = (int)(time.Value.DayOfWeek);
                switch (dayOfWeek)
                {
                    case 0:
                        return "Common.DayOfWeek.Sun";
                        break;
                    case 1:
                        return "Common.DayOfWeek.Mon";
                        break;
                    case 2:
                        return "Common.DayOfWeek.Tue";
                        break;
                    case 3:
                        return "Common.DayOfWeek.Wed";
                        break;
                    case 4:
                        return "Common.DayOfWeek.Thu";
                        break;
                    case 5:
                        return "Common.DayOfWeek.Fri";
                        break;
                    case 6:
                        return "Common.DayOfWeek.Sat";
                        break;
                    default:
                        return "";
                        break;
                }
            }
            return "";
        }

        public async Task<OverTimeHoursDetailDto> GetDetails(int id)
        {
            return await _baseService.FindAsync<OverTimeHours, OverTimeHoursDetailDto>(id);
        }

        public async Task<int> UpdateById(int id, OverTimeHoursUpdateDto editDto)
        {
            return await _baseService.UpdateAsync<OverTimeHours, OverTimeHoursUpdateDto>(id, editDto);
        }
        //delete
        public async Task<int> Delete(int id)
        {
            return await _baseService.DeleteAsync<OverTimeHours, int>(id);
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
            if (diffrenceDays > 31)
            {
                throw new EPSException("CalculateWorkingHourService.Message.TimeMonth");
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

    }
}
