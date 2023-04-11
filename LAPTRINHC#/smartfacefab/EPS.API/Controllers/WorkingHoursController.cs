using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.CalculateWorkingHour;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/timesheet/working-hours")]
    [Authorize]
    public class WorkingHoursController : BaseController
    {
        private WorkingHoursService _workingHoursService;

        private CalculateWorkingHourService _calculateWorkingHourService;

        public WorkingHoursController(WorkingHoursService workingHoursService, CalculateWorkingHourService calculateWorkingHourService)
        {
            _workingHoursService = workingHoursService;
            _calculateWorkingHourService = calculateWorkingHourService;
        }

        [CustomAuthorize(PrivilegeList.ManageTimeKeeping)]
        [HttpPost("calculate-working-hours")]
        public async Task<IActionResult> CreateWorkingHoursByDto(CalculateDto calculateDto)
        {
            return Ok(await _calculateWorkingHourService.ExecuteCalculate(calculateDto));
        }
        [CustomAuthorize(PrivilegeList.ManageTimeKeeping)]
        [HttpPost("report-details")]
        public async Task<IActionResult> ReportDetail(CalculateDto calculateDto)
        {
            return Ok(await _calculateWorkingHourService.ReportDetails(calculateDto));
        }



    }
}
