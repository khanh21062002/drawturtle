using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service.Dtos.Common;
using Microsoft.EntityFrameworkCore;
using EPS.Service;
using System;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/lookup")]
    [Authorize]
    public class LookupController : BaseController
    {
        private LookupService _lookupService;

        public LookupController(LookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _lookupService.GetRoles());
        }

        [AllowAnonymous]
        [HttpGet("currentDateTime")]
        public IActionResult GetCurrentDateTime()
        {
            return Ok(_lookupService.GetCurrentDateTime());
        }

        [HttpGet("privileges")]
        public async Task<IActionResult> GetPrivileges()
        {
            return Ok(await _lookupService.GetPrivileges());
        }

        [HttpGet("unit-tree")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUnitTree()
        {
            return Ok(await _lookupService.GetUnitTree());
        }

        [HttpGet("company-tree")]
        public async Task<IActionResult> GetCompanyTree()
        {
            return Ok(await _lookupService.GetCompanyTree());
        }


        [HttpGet("company-tree-view")]
        public async Task<IActionResult> GetCompanyTreeView()
        {
            return Ok(await _lookupService.GetCompanyTreeView());
        }

        [HttpGet("department-tree")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDepartmentTree()
        {
            return Ok(await _lookupService.GetDepartmentTree());
        }

        [HttpGet("department-tree-exceptItself/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDepartmentTreeExceptItself(int id)
        {
            return Ok(await _lookupService.GetDepartmentTreeExceptItself(id));
        }

        // Danh sách câu hỏi
        [HttpGet("questions/{locale}")]
        public async Task<Object> GetQuestions(string locale)
        {
            var ret = await _lookupService.GetQuestions(locale);
            return ret;
        }

        //Danh sách thiết bị
        [HttpGet("machines")]
        public async Task<IActionResult> GetMachine()
        {
            return Ok(await _lookupService.GetMachines());
        }

        [HttpGet("machines/{compId}")]
        public async Task<IActionResult> GetMachineByCompId(int compId)
        {
            return Ok(await _lookupService.GetMachinesByCompId(compId));
        }

        //danh sách thiết bị theo khu vực
        [HttpGet("machinesByAreaId/{areaId}")]
        public async Task<IActionResult> GetMachineByAreaId(string areaId)
        {
            return Ok(await _lookupService.GetMachineByAreaId(areaId));
        }

        //Danh sách khu vực
        [HttpGet("area")]
        public async Task<IActionResult> GetAreas()
        {
            return Ok(await _lookupService.GetAreas());
        }

        [HttpGet("mon")]
        public async Task<IActionResult> GetMon()
        {
            return Ok(await _lookupService.GetMon());
        }

        [HttpGet("featuresstatus")]
        public async Task<IActionResult> GetFeaturesStatus()
        {
            return Ok(await _lookupService.GetFeaturesStatus());
        }

        //Danh sách đơn vị
        [HttpGet("companys")]
        public async Task<IActionResult> GetCompany()
        {
            return Ok(await _lookupService.GetCompanys());
        }

        [HttpGet("companyas")]
        public async Task<IActionResult> GetCompanya()
        {
            return Ok(await _lookupService.GetCompanyas());
        }

        [HttpGet("companys-trees")]
        public async Task<IActionResult> GetCompanyTrees()
        {
            return Ok(await _lookupService.GetCompanys());
        }

        //Danh sách nhóm người
        [HttpGet("groups")]
        public async Task<IActionResult> GetGroup()
        {
            return Ok(await _lookupService.GetGroups());
        }

        //Danh sách nhóm người khống phân quyền
        [AllowAnonymous]
        [HttpGet("groupAllowAnonymous")]
        public async Task<IActionResult> GetGroupAllowAnonymous()
        {
            return Ok(await _lookupService.GetGroupAllowAnonymous());
        }

        [HttpGet("groups/{compId}")]
        public async Task<IActionResult> GetGroupByCompId(int compId)
        {
            return Ok(await _lookupService.GetGroupsByCompId(compId));
        }

        [HttpGet("group-by-dept/{deptId}")]
        public async Task<IActionResult> GetGroupByDepartmentId(int deptId)
        {
            return Ok(await _lookupService.GetGroupsByDeptId(deptId));
        }

        //Danh sách liên hệ
        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartment()
        {
            return Ok(await _lookupService.GetDepartments());
        }

        //Danh sách liên hệ
        [HttpGet("all-departments")]
        public async Task<IActionResult> GetAllDepartment()
        {
            return Ok(await _lookupService.GetAllDepartments());
        }

        [HttpGet("departments/{compId}")]
        public async Task<IActionResult> GetDepartmentByCompId(int compId)
        {
            return Ok(await _lookupService.GetDepartmentsByCompId(compId));
        }

        //Danh sách vùng truy nhập
        [HttpGet("groupaccesss")]
        public async Task<IActionResult> GetGroupAccesss()
        {
            return Ok(await _lookupService.GetGroupAccesss());
        }

        //Danh sách vùng truy nhập
        [HttpGet("groupaccesss/{compId}")]
        public async Task<IActionResult> GetGroupAccesssByCompId(int compId)
        {
            return Ok(await _lookupService.GetGroupAccesssByCompId(compId));
        }

        //Danh sách liên hệ
        [HttpGet("accesstimesegs")]
        public async Task<IActionResult> GetAccessTimeSegs()
        {
            return Ok(await _lookupService.GetAccessTimeSegs());
        }

        [HttpGet("accesstimesegs/{compId}")]
        public async Task<IActionResult> GetAccessTimeSegsByCompId(int compId)
        {
            return Ok(await _lookupService.GetAccessTimeSegsByCompId(compId));
        }

        [HttpGet("accesstime-by-group/{groupId}")]
        public async Task<IActionResult> GetAccessTimeSegsByGroupId(int groupId)
        {
            return Ok(await _lookupService.GetAccessTimeSegsByGroupId(groupId));
        }

        //Danh sách liên hệ
        [HttpGet("persons")]
        public async Task<IActionResult> GetPersons()
        {
            return Ok(await _lookupService.GetPersons());
        }

        [HttpGet("persons/{compId}")]
        public async Task<IActionResult> GetPersonsByCompId(int compId)
        {
            return Ok(await _lookupService.GetPersonsByCompId(compId));
        }

        [HttpGet("persons-by-dept/{deptId}")]
        public async Task<IActionResult> GetPersonsByDeptId(int deptId)
        {
            return Ok(await _lookupService.GetPersonsByDeptId(deptId));
        }

        [HttpGet("chart/{compId}/checkIn")]
        public async Task<IActionResult> GetDataCheckInOutPerDays(int compId)
        {
            return Ok(await _lookupService.GetDataCheckInOutPerDays(compId));
        }

        [HttpGet("current-company-name")]
        public async Task<IActionResult> GetCompanyName()
        {
            return Ok(await _lookupService.GetCurrentCompName());
        }

        [HttpGet("working-shift-time")]
        public async Task<IActionResult> GetWorkingShiftTime()
        {
            return Ok(await _lookupService.GetWorkingShiftTime());
        }

        [HttpGet("top-late/{options}")]
        public async Task<IActionResult> GetTopPersonLateInCurCompany(int options)
        {
            return Ok(await _lookupService.GetTopLate(options));
        }

        [HttpGet("get-data-pie-chart")]
        public async Task<IActionResult> GetDataPieChartByDate(string dateSearch)
        {
            DateTime convertDate;
            if (DateTime.TryParse(dateSearch, out convertDate))
            {
                // handle parse success
                return Ok(await _lookupService.GetDataPieChartByDay(convertDate));
            }
            //default is yesterday
            convertDate = DateTime.Today.AddDays(-1);
            return Ok(await _lookupService.GetDataPieChartByDay(convertDate));
        }

        //Danh sách khối
        [HttpGet("grades")]
        public async Task<IActionResult> GetGrades()
        {
            return Ok(await _lookupService.GetGrades());
        }

        //Danh sách lớp
        [HttpGet("classes")]
        public async Task<IActionResult> GetClasses()
        {
            return Ok(await _lookupService.GetClasses());
        }

        //Danh sách lớp theo khối
        [HttpGet("classes/{id}")]
        public async Task<IActionResult> GetClassesId(string id)
        {
            return Ok(await _lookupService.GetClassesById(id));
        }

        //Danh sách
        [HttpGet("school/default-parent-dept")]
        public async Task<IActionResult> GetDefaultParentDept()
        {
            return Ok(await _lookupService.GetDefaultParentDept());
        }

        //Danh sách
        [HttpGet("school/default-parent-group")]
        public async Task<IActionResult> GetDefaultParentGroup()
        {
            return Ok(await _lookupService.GetDefaultParentGroup());
        }

        [HttpGet("device")]
        public async Task<IActionResult> GetAllDevice()
        {
            return Ok(await _lookupService.GetDevices());
        }

        //danh sách thiết bị
        [HttpGet("device1")]
        public async Task<IActionResult> GetNameDevice()
        {
            return Ok(await _lookupService.GetDevice());
        }

        //get all areas
        //[HttpGet("areas-all")]
        //public async Task<IActionResult> GetAllAreas()
        //{
        //    return Ok(await _lookupService.GetAreas());
        //}

        //danh sách chức năng
        [HttpGet("features")]
        public async Task<IActionResult> GetFeatures()
        {
            return Ok(await _lookupService.GetFeatures());
        }

        //danh sách khu vực
        [HttpGet("areas-l")]
        public async Task<IActionResult> GetAllAreaName()
        {
            return Ok(await _lookupService.GetAreaName());
        }

        //get areas when status == 1
        [HttpGet("areas-s")]
        public async Task<IActionResult> GetAllAreasStatus()
        {
            return Ok(await _lookupService.GetAreasStatus());
        }

        [HttpGet("dependentDevice")]
        public async Task<IActionResult> GetAllDependentDevice()
        {
            return Ok(await _lookupService.GetAllDependentDevice());
        }

        [HttpGet("guest")]
        public async Task<IActionResult> GetGuest()
        {
            return Ok(await _lookupService.GetGuest());
        }
    }
}
