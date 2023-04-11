using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.RegisterWorkingShift;
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
    [Route("api/timesheet/register-working-shift")]
    [Authorize]
    public class RegisterWorkingShiftController : BaseController
    {
        private RegisterWorkingShiftService _registerWorkingShiftService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public RegisterWorkingShiftController(RegisterWorkingShiftService registerWorkingShiftService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _registerWorkingShiftService = registerWorkingShiftService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        [CustomAuthorize(PrivilegeList.ViewRegisterWorkingShift, PrivilegeList.ManageRegisterWorkingShift)]
        [HttpGet]
        public async Task<IActionResult> GetListWorkingShiftTimes([FromQuery] RegisterWorkingShiftGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            return Ok(await _registerWorkingShiftService.GetAll(pagingModel));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewRegisterWorkingShift, PrivilegeList.ManageRegisterWorkingShift)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            await _checkDataService.CheckRegisterWorkingShift(id, (int)_userIdentity.CompId);
            return Ok(await _registerWorkingShiftService.GetDetails(id));
        }

        //danh sách chi tiết cảnh báo 
        [CustomAuthorize(PrivilegeList.ViewRegisterWorkingShift, PrivilegeList.ManageRegisterWorkingShift)]
        [HttpGet("getListDetails/{registerWorkingShiftId}")]
        public async Task<IActionResult> GetListDetailsByWkID(int registerWorkingShiftId)
        {
            await _checkDataService.CheckRegisterWorkingShift(registerWorkingShiftId, (int)_userIdentity.CompId);
            return Ok(await _registerWorkingShiftService.GetListNotiDetails(registerWorkingShiftId));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageRegisterWorkingShift)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckRegisterWorkingShift(id, (int)_userIdentity.CompId);
            return Ok(await _registerWorkingShiftService.UpdateDelete(id));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageRegisterWorkingShift)]
        [HttpPost]
        public async Task<IActionResult> Create(RegisterWorkingShiftCreateDto shiftTimeCreateDto)
        {
            if (shiftTimeCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(shiftTimeCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            var id = await _registerWorkingShiftService.Create(shiftTimeCreateDto);
            foreach (var item in shiftTimeCreateDto.listDetails)
            {
                item.RegisterWorkingShiftId = id;
                await _registerWorkingShiftService.CreateDetails(item);
            }
            return Ok(1);
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageRegisterWorkingShift)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RegisterWorkingShiftUpdateDto shiftTimeUpdateDto)
        {
            await _checkDataService.CheckRegisterWorkingShift(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckRegisterWorkingShift(id, (int)shiftTimeUpdateDto.CompId);
            var rs = await _registerWorkingShiftService.UpdateById(id, shiftTimeUpdateDto);
            return Ok(rs);
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageRegisterWorkingShift)]
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
                return Ok(await _registerWorkingShiftService.UpdateDeletes(shiftTimeIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
