using EPS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service.Dtos.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EPS.Utils.Repository;
using EPS.Service.Dtos.Common;
using EPS.Service;
using System.Collections.Generic;
using System.Transactions;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    [Authorize]
    public class UserController : BaseController
    {
        private AuthorizationService _authorizationService;

        public UserController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _authorizationService.ChangePassword(UserIdentity.Username, model));
        }

        [CustomAuthorize(PrivilegeList.ManageUser, PrivilegeList.ViewUser)]
        [HttpGet("list")]
        public async Task<IActionResult> GetUsers([FromQuery] UserGridPagingDto pagingModel)
        {
            return Ok(await _authorizationService.GetUsers(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.ManageUser, PrivilegeList.ViewUser)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _authorizationService.GetUserById(id));
        }

        [HttpGet("get-self-info")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {

            return Ok(await _authorizationService.GetCurrentUserInfo());
        }
        [HttpPut("change-password/{infoId}")]
        public async Task<IActionResult> PutCurrentUserInfo(int infoId, UserUpdateDto editedUser)
        {

            return Ok(await _authorizationService.PutUpdateUser(infoId, editedUser));
        }

        [CustomAuthorize(PrivilegeList.ManageUser)]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto newUser)
        {
            newUser.UnitId = 1;
            var id = await _authorizationService.CreateUser(newUser);
            ////set default role for user ( all role for user in admin company/ otherwise all exept manage role)
            //string[] listDefaultPrivileges = await _authorizationService.GetListDefaultPriviliesByUserId(id);
            //await _authorizationService.SaveUserPrivileges(id, listDefaultPrivileges);
            return Ok(id);
        }
        [CustomAuthorize(PrivilegeList.ManageUser)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto editedUser)
        {
            return Ok(await _authorizationService.UpdateUser(id, editedUser));
        }

        [CustomAuthorize(PrivilegeList.ManageUser)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _authorizationService.UpdateDelete1(id));
        }

        [CustomAuthorize(PrivilegeList.ManageUser)]
        [HttpGet("{id}/privileges")]
        public async Task<IActionResult> GetUserPrivileges(int id)
        {
            return Ok(await _authorizationService.GetUserPrivileges(id));
        }

        [CustomAuthorize(PrivilegeList.ManageUser)]
        [HttpPut("{id}/privileges")]
        public async Task<IActionResult> SaveUserPrivileges(int id, string privileges)
        {
            string[] arrprivileges = { };
            try
            {
                if (!string.IsNullOrEmpty(privileges))
                    arrprivileges = privileges.Split(',').ToArray();

                await _authorizationService.SaveUserPrivileges(id, arrprivileges);
                return Ok();
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageUser)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsers(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var userIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _authorizationService.UpdateDeletes1(userIds));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [CustomAuthorize(PrivilegeList.ManageUser)]
        [HttpGet("{checkcode}/{id}/{compid}/{username}")]
        public async Task<IActionResult> CheckExistCode(int id, int compid, string username)
        {
            return Ok(await _authorizationService.CheckExistCode(id, compid, username));
        }
    }
}
