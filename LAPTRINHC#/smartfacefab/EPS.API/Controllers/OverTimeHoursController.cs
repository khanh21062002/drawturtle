using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.CalculateWorkingHour;
using EPS.Service.Dtos.OverTimeHours;
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
    [Route("api/timesheet/overtimehours")]
    [Authorize]
    public class OverTimeHoursController : BaseController
    {
        private OverTimeHoursService _overTimeHoursService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;
        private TimeKeepingService _timeKeepingSevice;

        public OverTimeHoursController(OverTimeHoursService overTimeHoursService, CheckDataService checkDataService, IUserIdentity<int> userIdentity, TimeKeepingService timeKeepingService)
        {
            _overTimeHoursService = overTimeHoursService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
            _timeKeepingSevice = timeKeepingService;
        }

        [CustomAuthorize(PrivilegeList.ViewOverTimeHours, PrivilegeList.ManageOverTimeHours)]
        [HttpGet]
        public async Task<IActionResult> GetListWorkingShiftTimes([FromQuery] OverTimeHoursGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            pagingModel.SortDesc = false;
            pagingModel.SortBy = " PersonId,  Day ";

            int? groupId = pagingModel.GroupId;
            //TODO: mai lam
            if (groupId.HasValue)
            {
                //get all list person id in this personid
                List<Guid> listPersonId = new List<Guid>();
                 listPersonId = await _timeKeepingSevice.GetListPersonIdByGroupId(groupId.Value);
                pagingModel.ListPersonId = listPersonId;

            }

            var rs = await _overTimeHoursService.GetAll(pagingModel);
            foreach (var item in rs.Data)
            {
                if (item.PersonId.HasValue)
                {
                    var lstGroup = await _timeKeepingSevice.GetListGroupByPersonId(item.PersonId.Value);
                    item.GroupName = "";
                    int index = 0;
                    foreach (var group in lstGroup)
                    {

                        if (!string.IsNullOrEmpty(group.GroupName))
                        {
                            if (index == 0)
                            {
                                item.GroupName = group.GroupName;
                            }
                            else
                            {
                                item.GroupName = item.GroupName + "; " + group.GroupName;
                            }

                            index++;
                        }
                    }
                }

            }


            return Ok(rs);
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewOverTimeHours, PrivilegeList.ManageOverTimeHours)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            await _checkDataService.CheckOverTimeHours(id, (int)_userIdentity.CompId);
            return Ok(await _overTimeHoursService.GetDetails(id));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageOverTimeHours)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckOverTimeHours(id, (int)_userIdentity.CompId);
            return Ok(await _overTimeHoursService.Delete(id));
        }
        //create
        [CustomAuthorize(PrivilegeList.ManageOverTimeHours)]
        [HttpPost]
        public async Task<IActionResult> Create(OverTimeHoursCreateDto shiftTimeCreateDto)
        {
            if (shiftTimeCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(shiftTimeCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            var id = await _overTimeHoursService.Create(shiftTimeCreateDto);

            return Ok(1);
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageOverTimeHours)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OverTimeHoursUpdateDto shiftTimeUpdateDto)
        {
            await _checkDataService.CheckOverTimeHours(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckOverTimeHours(id, (int)shiftTimeUpdateDto.CompId);
            var rs = await _overTimeHoursService.UpdateById(id, shiftTimeUpdateDto);
            return Ok(rs);
        }
        [CustomAuthorize(PrivilegeList.ManageTimeKeeping)]
        [HttpPost("calculate-ot")]
        public async Task<IActionResult> CalculateOT(CalculateDto calculateDto)
        {
            return Ok(await _overTimeHoursService.ExecuteCalculate(calculateDto));
        }

    }
}
