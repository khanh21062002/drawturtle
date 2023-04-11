using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using EPS.Service.Dtos.CheckData;

namespace EPS.Service
{
    public class CheckDataService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private IUserIdentity<int> _userIdentity;
        private Data.EPSContext _context;

        public CheckDataService(EPSRepository repository, IMapper mapper, UserManager<User> userManager, IUserIdentity<int> userIdentity, Data.EPSContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _userIdentity = userIdentity;
            _baseService = new EPSBaseService(repository, mapper);
            _context = context;
        }

        public async Task CheckDepartment(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Department, ExitsIntDto>(x => x.ID == id && x.ComId == comId);

            mes(isExits);
        }

        public async Task CheckAcessTimeSeg(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<AccessTimeSeg, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckDayOff(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<DayOff, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }
        public async Task CheckEvent(Guid eventId, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Event, ExitsIntDto>(x => x.EventId == eventId && x.ComId == comId);
            mes(isExits);
        }


        public async Task CheckGroupAccess(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<GroupAccess, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckGroup(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Group, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckGuess(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<GuessRegister, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }
        public async Task CheckGuessByPhoneNumber(string phoneNumber, int comId)
        {
            bool isExits = await _baseService.AnyAsync<GuessRegister, ExitsIntDto>(x => x.PhoneNumber == phoneNumber && x.ComId == comId);
            mes(isExits);
        }
        public async Task CheckHolidays(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Holidays, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }
        public async Task CheckMachine(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Machine, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckNotification(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Notification, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

       

        public async Task CheckOverTime(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<OverTime, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckOverTimeHours(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<OverTimeHours, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckPerson(Guid personId, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Person, ExitsIntDto>(x => x.PersonId == personId && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckRegisterWorkingShift(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<RegisterWorkingShift, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckShiftTime(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<ShiftTime, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }
        public async Task CheckTimeKeeping(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<WorkingHours, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckUser(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<User, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckWorkCalendar(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<WorkCalendar, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckWorkingHours(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<WorkingHours, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }

        public async Task CheckWorkingShiftTimes(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<WorkingShiftTimes, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }


        public async Task CheckCompIdIsAuthorize(int compId, int compIdAuthor)
        {
            bool isExits = compId == compIdAuthor;
            mes2(isExits);
        }

        public async Task CheckGrades(int id, int comId)
        {
            bool isExits = await _baseService.AnyAsync<Grades, ExitsIntDto>(x => x.ID == id && x.ComId == comId);
            mes(isExits);
        }


        private void mes(bool isExits)
        {
            if (!isExits)
            {
                throw new EPSException(EPSExceptionCode.NotExistInTheSystem, "Dữ liệu");
            }
        }

        private void mes2(bool isExits)
        {
            if (!isExits)
            {
                throw new EPSException("AuthorizationService.Message.CompanyPrivileges");
            }
        }
    }
}
