using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.DayOff;
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
    [Route("api/timesheet/DayOff")]
    [Authorize]
    public class DayOffController : BaseController
    {
        private DayOffService _dayOffService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public DayOffController(DayOffService dayOffService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _dayOffService = dayOffService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        [CustomAuthorize(PrivilegeList.ViewDayOff, PrivilegeList.ManageDayOff)]
        [HttpGet]
        public async Task<IActionResult> GetListDayOff([FromQuery] DayOffGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _dayOffService.GetAllDayOffs(pagingModel));
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewDayOff, PrivilegeList.ManageDayOff)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDayOffById(int id)
        {
            await _checkDataService.CheckDayOff(id, (int)_userIdentity.CompId);
            return Ok(await _dayOffService.GetDayOffsById(id));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageDayOff)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckDayOff(id, (int)_userIdentity.CompId);
            return Ok(await _dayOffService.UpdateDelete(id));
        }
        //create
        [CustomAuthorize(PrivilegeList.ManageDayOff)]
        [HttpPost]
        public async Task<IActionResult> Create(DayOffCreateDto DayOffCreateDto)
        {
            if (DayOffCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(DayOffCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }

            return Ok(await _dayOffService.CreateDayOffs(DayOffCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageDayOff)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DayOffUpdateDto shiftTimeUpdateDto)
        {
            await _checkDataService.CheckDayOff(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckDayOff(id, (int)shiftTimeUpdateDto.CompId);
            return Ok(await _dayOffService.UpdateDayOffs(id, shiftTimeUpdateDto));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageDayOff)]
        [HttpDelete]
        public async Task<IActionResult> DeleteMultiple(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var shiftTimeIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _dayOffService.UpdateDeletes(shiftTimeIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
