using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.DayOff;
using EPS.Service.Dtos.Grades;
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
    [Route("api/grades")]
    [Authorize]
    public class GradesController : BaseController
    {
        private GradesService _gradesService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public GradesController(GradesService gradesService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _gradesService = gradesService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        [CustomAuthorize(PrivilegeList.ViewGrades, PrivilegeList.ManageGrades)]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GradesGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _gradesService.GetAll(pagingModel));
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewGrades, PrivilegeList.ManageGrades)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDayOffById(int id)
        {
            await _checkDataService.CheckGrades(id, (int)_userIdentity.CompId);
            return Ok(await _gradesService.GetById(id));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageGrades)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckGrades(id, (int)_userIdentity.CompId);
            return Ok(await _gradesService.UpdateDelete(id));
        }
        //create
        [CustomAuthorize(PrivilegeList.ManageGrades)]
        [HttpPost]
        public async Task<IActionResult> Create(GradesCreateDto gradeCreateDto)
        {
            if (gradeCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(gradeCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }

            return Ok(await _gradesService.Create(gradeCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageGrades)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GradesUpdateDto shiftTimeUpdateDto)
        {
            await _checkDataService.CheckGrades(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckGrades(id, (int)shiftTimeUpdateDto.CompId);
            return Ok(await _gradesService.Update(id, shiftTimeUpdateDto));
        }



    }
}
