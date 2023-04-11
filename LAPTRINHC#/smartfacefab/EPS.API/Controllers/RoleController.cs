using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/roles")]
    [Authorize]
    public class RoleController : BaseController
    {
        private AuthorizationService _authorizationService;

        public RoleController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewRole, PrivilegeList.ManageRole)]
        [HttpGet]
        public async Task<IActionResult> GetListRoles([FromQuery]RoleGridPagingDto pagingModel)
        {
            return Ok(await _authorizationService.GetRoles(pagingModel));
        }

        //get privileges by roleId  
        [CustomAuthorize(PrivilegeList.ViewRole, PrivilegeList.ManageRole)]
        [HttpGet("{id}/privileges")]
        public async Task<IActionResult> GetRolePrivieleges(int id)
        {
            return Ok(await _authorizationService.GetRolePrivileges(id));
        }

        [CustomAuthorize(PrivilegeList.ManageRole)]
        [HttpPut("{id}/privileges")]
        public async Task<IActionResult> SaveRolePrivileges(int id, string privileges)
        {
            string[] arrprivileges = { };
            try
            {
                if (!string.IsNullOrEmpty(privileges))
                    arrprivileges = privileges.Split(',').ToArray();

                await _authorizationService.SaveRolePrivileges(id, arrprivileges);
                return Ok();
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewRole, PrivilegeList.ManageRole)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            return Ok(await _authorizationService.GetRoleById(id));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageRole)]
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateDto roleCreateDto)
        {
            return Ok(await _authorizationService.CreateRole(roleCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageRole)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleUpdateDto roleUpdateDto)
        {
            return Ok(await _authorizationService.UpdateRole(id, roleUpdateDto));
        }

        [CustomAuthorize(PrivilegeList.ManageRole)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _authorizationService.UpdateDelete(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageRole)]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoles(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var roleIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _authorizationService.UpdateDeletes(roleIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
