using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.GroupAccess;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/groupaccess")]
    [Authorize]

    public class GroupAccessController : BaseController
    {
        private GroupAccessService _groupaccessService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public GroupAccessController(GroupAccessService groupaccessService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _groupaccessService = groupaccessService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewGroupAccess, PrivilegeList.ManageGroupAccess)]
        [HttpGet]
        public async Task<IActionResult> GetListGroupAccesss([FromQuery] GroupAccessGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _groupaccessService.GetGroupAccesss(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageGroupAccess)]
        [HttpPost]
        public async Task<IActionResult> Create(GroupAccessCreateDto groupaccessCreateDto)
        {
            if (groupaccessCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(groupaccessCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            return Ok(await _groupaccessService.CreateGroupAccess(groupaccessCreateDto));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewGroupAccess, PrivilegeList.ManageGroupAccess)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupAccessById(string id)
        {
            //await _checkDataService.CheckGroupAccess(Int32.Parse(id), (int)_userIdentity.CompId);

            return Ok(await _groupaccessService.GetGroupAccessById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageGroupAccess)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupAccess(string id, GroupAccessUpdateDto groupaccessUpdateDto)
        {
            //await _checkDataService.CheckGroupAccess(Int32.Parse(id), (int)_userIdentity.CompId);
            return Ok(await _groupaccessService.UpdateGroupAccess(id, groupaccessUpdateDto));
        }   

        //delete
        [CustomAuthorize(PrivilegeList.ManageGroupAccess)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            //await _checkDataService.CheckGroupAccess(Int32.Parse(id), (int)_userIdentity.CompId);
            return Ok(await _groupaccessService.UpdateDelete1(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageGroupAccess)]
        [HttpDelete]
        public async Task<IActionResult> DeleteGroupAccesss(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var GroupAccesss = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _groupaccessService.UpdateDeletes1(GroupAccesss));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
