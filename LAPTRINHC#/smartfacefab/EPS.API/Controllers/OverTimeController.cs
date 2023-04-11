using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.OverTime;
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
    [Route("api/timesheet/overtime")]
    [Authorize]
    public class OverTimeController : BaseController
    {
        private OverTimeService _overTimeSevice;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public OverTimeController(OverTimeService overTimeSevice, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _overTimeSevice = overTimeSevice;
            _checkDataService = checkDataService;
            _userIdentity = userIdentity;
        }

        //list
        [CustomAuthorize(PrivilegeList.ViewOverTime, PrivilegeList.ManageOverTime)]
        [HttpGet]
        public async Task<IActionResult> GetListOverTimes([FromQuery] OverTimeGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            pagingModel.SortDesc = false;
            pagingModel.SortBy = " PERSON_ID,  DATE_OT ";


            return Ok(await _overTimeSevice.GetOverTimes(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageOverTime)]
        [HttpPost]
        public async Task<IActionResult> Create(OverTimeCreateDto overTimeCreateDto)
        {
            if (overTimeCreateDto.COMP_ID.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(overTimeCreateDto.COMP_ID.Value, (int)_userIdentity.CompId);
            }
            return Ok(await _overTimeSevice.CreateOverTime(overTimeCreateDto));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewOverTime, PrivilegeList.ManageOverTime)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOverTimeById(int id)
        {
            await _checkDataService.CheckOverTime(id, (int)_userIdentity.CompId);
            return Ok(await _overTimeSevice.GetOverTimeById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageOverTime)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OverTimeUpdateDto overTimeUpdateDto)
        {
            await _checkDataService.CheckOverTime(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckOverTime(id, (int)overTimeUpdateDto.COMP_ID);
            return Ok(await _overTimeSevice.UpdateOverTime(id, overTimeUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageOverTime)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckOverTime(id, (int)_userIdentity.CompId);
            return Ok(await _overTimeSevice.DeleteOverTime(id));
        }
        //list
        [CustomAuthorize(PrivilegeList.ViewOverTime, PrivilegeList.ManageOverTime)]
        [HttpGet("check-holidays/{day}")]
        public async Task<IActionResult> CheckHoliday(string day)
        {
            int? compId = _userIdentity.CompId;

            return Ok(await _overTimeSevice.CheckHolidays(day, compId));
        }
    }
}
