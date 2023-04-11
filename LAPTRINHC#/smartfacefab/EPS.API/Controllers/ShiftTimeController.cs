using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.ShiftTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/categories/shifttime")]
    [Authorize]
    public class ShiftTimeController : BaseController
    {
        private ShiftTimeService _shiftTimeSevice;

        public ShiftTimeController(ShiftTimeService shiftTimeSevice)
        {
            _shiftTimeSevice = shiftTimeSevice;
        }

        [CustomAuthorize(PrivilegeList.ViewShiftTime, PrivilegeList.ManageShiftTime)]
        [HttpGet]
        public async Task<IActionResult> GetListShiftTimes([FromQuery] ShiftTimeGridPagingDto pagingModel)
        {
            return Ok(await _shiftTimeSevice.GetShiftTimes(pagingModel));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewShiftTime, PrivilegeList.ManageShiftTime)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftTimeById(int id)
        {
            return Ok(await _shiftTimeSevice.GetShiftTimeById(id));
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewShiftTime, PrivilegeList.ManageShiftTime)]
        [HttpGet("loadShiftTimeByCompId/{compId}")]
        public async Task<IActionResult> LoadShiftTimeByCompId(int compId)
        {
            return Ok(await _shiftTimeSevice.GetShiftTimeByCompId(compId));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageShiftTime)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _shiftTimeSevice.UpdateDelete(id));
        }
        //create
        [CustomAuthorize(PrivilegeList.ManageShiftTime)]
        [HttpPost]
        public async Task<IActionResult> Create(ShiftTimeCreateDto shiftTimeCreateDto)
        {
            return Ok(await _shiftTimeSevice.CreateShiftTime(shiftTimeCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageShiftTime)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ShiftTimeUpdateDto shiftTimeUpdateDto)
        {
            return Ok(await _shiftTimeSevice.UpdateShiftTime(id, shiftTimeUpdateDto));
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
                return Ok(await _shiftTimeSevice.UpdateDeletes(shiftTimeIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
