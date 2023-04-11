using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.WorkCalendar;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/timesheet/workcalendar")]
    [Authorize]
    public class WorkCalendarController : BaseController
    {
        private WorkCalendarService _workCalendarService;
        private IUserIdentity<int> _userIdentity;
        private CheckDataService _checkDataService;


        public WorkCalendarController(WorkCalendarService workCalendarService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _workCalendarService = workCalendarService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }
        [CustomAuthorize(PrivilegeList.ViewWorkCalendar, PrivilegeList.ManageWorkCalendar)]
        [HttpGet]
        public async Task<IActionResult> GetListWorkingShiftTimes([FromQuery] WorkCalendarGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            return Ok(await _workCalendarService.GetAll(pagingModel));
        }

        //list all no paging
        [CustomAuthorize(PrivilegeList.ViewWorkCalendar, PrivilegeList.ManageWorkCalendar, PrivilegeList.ViewRegisterWorkingShift, PrivilegeList.ManageRegisterWorkingShift)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNoPaging()
        {
            return Ok(await _workCalendarService.GetAllNoPaging());
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewWorkCalendar, PrivilegeList.ManageWorkCalendar)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            await _checkDataService.CheckWorkCalendar(id, (int)_userIdentity.CompId);
            return Ok(await _workCalendarService.GetDetails(id));
        }

        //danh sách chi tiết cảnh báo 
        [HttpGet("getListDetails/{workcalendarId}")]
        public async Task<IActionResult> GetListDetailsByWkID(int workcalendarId)
        {
            await _checkDataService.CheckWorkCalendar(workcalendarId, (int)_userIdentity.CompId);
            return Ok(await _workCalendarService.GetListNotiDetails(workcalendarId));
        }


        //delete
        [CustomAuthorize(PrivilegeList.ManageWorkCalendar)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckWorkCalendar(id, (int)_userIdentity.CompId);
            return Ok(await _workCalendarService.UpdateDelete(id));

        }
        //create
        [CustomAuthorize(PrivilegeList.ManageWorkCalendar)]
        [HttpPost]
        public async Task<IActionResult> Create(WorkCalendarCreateDto shiftTimeCreateDto)
        {
            if (shiftTimeCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(shiftTimeCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            var id = await _workCalendarService.Create(shiftTimeCreateDto);
            foreach (var item in shiftTimeCreateDto.ListDetails)
            {
                item.WorkCalendarId = id;
                await _workCalendarService.CreateDetails(item);
            }
            return Ok(id);
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageWorkCalendar)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkCalendarUpdateDto shiftTimeUpdateDto)
        {
            await _checkDataService.CheckWorkCalendar(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckWorkCalendar(id, (int)shiftTimeUpdateDto.CompId);
            var rs = await _workCalendarService.UpdateById(id, shiftTimeUpdateDto);
            //update details
            foreach (var item in shiftTimeUpdateDto.ListDetails)
            {
                item.WorkCalendarId = id;
                var itemId = item.ID;
                if (itemId > 0)
                {
                    await _workCalendarService.UpdateDetailsById(itemId, item);
                }
            }
            return Ok(rs);
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageWorkCalendar)]
        [HttpDelete]
        public async Task<IActionResult> DeleteMultipleShiftTimes(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var shiftTimeIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                foreach (var item in shiftTimeIds)
                {
                    await _checkDataService.CheckWorkCalendar(item, (int)_userIdentity.CompId);
                }
                return Ok(await _workCalendarService.UpdateDeletes(shiftTimeIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
