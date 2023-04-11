using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Holidays;
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
    [Route("api/timesheet/holidays")]
    [Authorize]
    public class HolidaysController : BaseController
    {
        private HolidaysService _holidaysService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public HolidaysController(HolidaysService holidaysService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _holidaysService = holidaysService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        [CustomAuthorize(PrivilegeList.ViewHolidays, PrivilegeList.ManageHolidays)]
        [HttpGet]
        public async Task<IActionResult> GetListHolidays([FromQuery] HolidaysGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _holidaysService.GetAllHolidays(pagingModel));
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewHolidays, PrivilegeList.ManageHolidays)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHolidaysById(int id)
        {
            await _checkDataService.CheckHolidays(id, (int)_userIdentity.CompId);
            return Ok(await _holidaysService.GetHolidaysById(id));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageHolidays)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckHolidays(id, (int)_userIdentity.CompId);
            return Ok(await _holidaysService.UpdateDelete(id));
        }
        //create
        [CustomAuthorize(PrivilegeList.ManageHolidays)]
        [HttpPost]
        public async Task<IActionResult> Create(HolidaysCreateDto holidaysCreateDto)
        {
            if (holidaysCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(holidaysCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            return Ok(await _holidaysService.CreateHolidays(holidaysCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageHolidays)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HolidaysUpdateDto shiftTimeUpdateDto)
        {
            await _checkDataService.CheckHolidays(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckHolidays(id, (int)shiftTimeUpdateDto.CompId);
            return Ok(await _holidaysService.UpdateHolidays(id, shiftTimeUpdateDto));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageHolidays)]
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
                return Ok(await _holidaysService.UpdateDeletes(shiftTimeIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
