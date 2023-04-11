using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.ShiftTime;
using EPS.Service.Dtos.WorkingShiftTimes;
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
    [Route("api/timesheet/working-shift-times")]
    [Authorize]
    public class WorkingShiftTimeController : BaseController
    {
        private WorkingShiftTimeService _workingShiftTimeSevice;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public WorkingShiftTimeController(WorkingShiftTimeService workingShiftTimeSevice, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _workingShiftTimeSevice = workingShiftTimeSevice;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        [CustomAuthorize(PrivilegeList.ViewShiftTime, PrivilegeList.ManageShiftTime)]
        [HttpGet]
        public async Task<IActionResult> GetListWorkingShiftTimes([FromQuery] WorkingShiftTimesGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            return Ok(await _workingShiftTimeSevice.GetListWorkingShiftTimes(pagingModel));
        }
        //list all no paging
        [CustomAuthorize(PrivilegeList.ViewShiftTime, PrivilegeList.ManageShiftTime, PrivilegeList.ViewRegisterWorkingShift, PrivilegeList.ManageRegisterWorkingShift)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNoPaging()
        {
            return Ok(await _workingShiftTimeSevice.GetAllNoPaging());
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewShiftTime, PrivilegeList.ManageShiftTime)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftTimeById(int id)
        {
            await _checkDataService.CheckWorkingShiftTimes(id, (int)_userIdentity.CompId);
            return Ok(await _workingShiftTimeSevice.GetWorkingShiftTimesById(id));
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewShiftTime, PrivilegeList.ManageShiftTime)]
        [HttpGet("loadShiftTimeByCompId/{compId}")]
        public async Task<IActionResult> LoadShiftTimeByCompId(int compId)
        {
            await _checkDataService.CheckCompIdIsAuthorize(compId, (int)_userIdentity.CompId);

            return Ok(await _workingShiftTimeSevice.GetWorkingShiftTimesByCompId(compId));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageShiftTime)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckWorkingShiftTimes(id, (int)_userIdentity.CompId);
            return Ok(await _workingShiftTimeSevice.UpdateDelete(id));
        }
        //create
        [CustomAuthorize(PrivilegeList.ManageShiftTime)]
        [HttpPost]
        public async Task<IActionResult> Create(WorkingShiftTimesCreateDto shiftTimeCreateDto)
        {
            if (shiftTimeCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(shiftTimeCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            return Ok(await _workingShiftTimeSevice.CreateWorkingShiftTimes(shiftTimeCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageShiftTime)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkingShiftTimesUpdateDto shiftTimeUpdateDto)
        {
            await _checkDataService.CheckWorkingShiftTimes(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckWorkingShiftTimes(id, (int)shiftTimeUpdateDto.CompId);
            return Ok(await _workingShiftTimeSevice.UpdateWorkingShiftTimes(id, shiftTimeUpdateDto));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageShiftTime)]
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
                    await _checkDataService.CheckWorkingShiftTimes(item, (int)_userIdentity.CompId);
                }
                return Ok(await _workingShiftTimeSevice.UpdateDeletes(shiftTimeIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
