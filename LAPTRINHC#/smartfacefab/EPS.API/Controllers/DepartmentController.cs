using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Department;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/department")]
    [Authorize]

    public class DepartmentController : BaseController
    {
        private DepartmentService _departmentService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public DepartmentController(DepartmentService departmentService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _departmentService = departmentService;
            _checkDataService = checkDataService;
            _userIdentity = userIdentity;
        }

        //Regenerate tree map in db (parent hidden field and tree level)
        [HttpGet("regenerate-tree")]
        public async Task<IActionResult> GenerateTreeView()
        {
            if (_userIdentity.CompId != 1)
            {
                throw new Exception("CompanyService.Message.UserPrivileges");
            }
            return Ok(await _departmentService.RegenerateTree());
        }

        [HttpGet("department-tree-view")]
        public async Task<IActionResult> GetDepartmentTreeView()
        {
            return Ok(await _departmentService.GetDepartmentTreeView());
        }


        //list all phòng ban nhà máy xí nghiệp
        [CustomAuthorize(PrivilegeList.ViewDepartment, PrivilegeList.ManageDepartment)]
        [HttpGet]
        public async Task<IActionResult> GetListDepartments([FromQuery] DepartmentGridPagingDto pagingModel)
        {
            //await _departmentService.RegenerateTree();
            pagingModel.compId = _userIdentity.CompId;
            pagingModel.Type = AppConstants.DepartmentType.FACTORY;
            return Ok(await _departmentService.GetDepartments(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageDepartment)]
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateDto departmentCreateDto)
        {
            if (departmentCreateDto.ComId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(departmentCreateDto.ComId.Value, (int)_userIdentity.CompId);
            }

            return Ok(await _departmentService.CreateDepartment(departmentCreateDto));
        }


        //detail
        [CustomAuthorize(PrivilegeList.ViewDepartment, PrivilegeList.ManageDepartment)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            await _checkDataService.CheckDepartment(id, (int)_userIdentity.CompId);
            return Ok(await _departmentService.GetDepartmentById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageDepartment)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentUpdateDto departmentUpdateDto)
        {
            await _checkDataService.CheckDepartment(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckDepartment(id, (int)departmentUpdateDto.ComId);
            return Ok(await _departmentService.UpdateDepartment(id, departmentUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageDepartment)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckDepartment(id, (int)_userIdentity.CompId);
            return Ok(await _departmentService.UpdateIsDelete(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageDepartment)]
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartments(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var Departments = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _departmentService.UpdateIsDeleteMuilti(Departments));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [CustomAuthorize(PrivilegeList.ManageDepartment)]
        [HttpGet("{checkcode}/{id}/{comId}/{code}")]
        public async Task<IActionResult> CheckExistCode(int id, int comId, string code)
        {
            if (comId != _userIdentity.CompId)
            {
                throw new EPSException("CompanyService.Message.UserPrivileges");
            }

            return Ok(await _departmentService.CheckExistCode(id, comId, code));
        }




        //---FOR SCHOOL --

        //list all lớp
        [CustomAuthorize(PrivilegeList.ViewClass, PrivilegeList.ManageClass)]
        [HttpGet("class")]
        public async Task<IActionResult> GetListClasses([FromQuery] DepartmentGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            pagingModel.Type = AppConstants.DepartmentType.SCHOOL;
            return Ok(await _departmentService.GetDepartments(pagingModel));
        }

        //create lớp
        [CustomAuthorize(PrivilegeList.ManageClass)]
        [HttpPost("class")]
        public async Task<IActionResult> CreateClass(DepartmentCreateDto departmentCreateDto)
        {
            if (departmentCreateDto.ComId.HasValue)
            {

                await _checkDataService.CheckCompIdIsAuthorize(departmentCreateDto.ComId.Value, (int)_userIdentity.CompId);
            }
            departmentCreateDto.Type = AppConstants.DepartmentType.SCHOOL;
            return Ok(await _departmentService.CreateDepartment(departmentCreateDto));
        }
        //update
        [CustomAuthorize(PrivilegeList.ManageDepartment)]
        [HttpPut("class/{id}")]
        public async Task<IActionResult> UpdateClass(int id, DepartmentUpdateDto departmentUpdateDto)
        {
            await _checkDataService.CheckDepartment(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckDepartment(id, (int)departmentUpdateDto.ComId);
            departmentUpdateDto.Type = AppConstants.DepartmentType.SCHOOL;
            return Ok(await _departmentService.UpdateDepartment(id, departmentUpdateDto));
        }
    }
}
