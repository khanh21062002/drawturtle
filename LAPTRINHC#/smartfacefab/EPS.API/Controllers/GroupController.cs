using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Group;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/group")]
    [Authorize]

    public class GroupController : BaseController
    {
        private GroupService _groupService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;


        public GroupController(GroupService groupService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _groupService = groupService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewGroup, PrivilegeList.ManageGroup)]
        [HttpGet]
        public async Task<IActionResult> GetListGroups([FromQuery] GroupGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _groupService.GetGroups(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageGroup)]
        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateDto GroupCreateDto)
        {
            if (GroupCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(GroupCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            return Ok(await _groupService.CreateGroup(GroupCreateDto));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewGroup, PrivilegeList.ManageGroup)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            await _checkDataService.CheckGroup(id, (int)_userIdentity.CompId);
            return Ok(await _groupService.GetGroupById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageGroup)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupUpdateDto GroupUpdateDto)
        {
            await _checkDataService.CheckGroup(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckGroup(id, (int)GroupUpdateDto.CompId);
            return Ok(await _groupService.UpdateGroup(id, GroupUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageGroup)]
        [HttpDelete("{groupid}")]
        public async Task<IActionResult> Delete(int groupid)
        {
            await _checkDataService.CheckGroup(groupid, (int)_userIdentity.CompId);
            return Ok(await _groupService.UpdateIsDelete(groupid));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageGroup)]
        [HttpDelete]
        public async Task<IActionResult> DeleteGroups(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var Groups = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _groupService.UpdateDeletes1(Groups));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [CustomAuthorize(PrivilegeList.ManageGroup)]
        [HttpGet("{checkcode}/{id}/{compId}/{deptId}/{groupcode}")]
        public async Task<IActionResult> CheckExistCode(int id, int compId, int deptId, string groupcode)
        {
            return Ok(await _groupService.CheckExistCode(id, compId, deptId, groupcode));
        }
    }
}
