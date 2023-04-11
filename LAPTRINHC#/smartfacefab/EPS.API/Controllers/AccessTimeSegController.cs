using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.AccessTimeSeg;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/accesstimeseg")]
    [Authorize]

    public class AccessTimeSegController : BaseController
    {
        private AccessTimeSegservice _AccessTimeSegservice;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;


        public AccessTimeSegController(AccessTimeSegservice AccessTimeSegservice, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _AccessTimeSegservice = AccessTimeSegservice;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewAccessTimeSeg, PrivilegeList.ManageAccessTimeSeg)]
        [HttpGet]
        public async Task<IActionResult> GetListAccessTimeSegs([FromQuery] AccessTimeSegGridPagingDto pagingModel)
        {

            pagingModel.compId = _userIdentity.CompId;

            return Ok(await _AccessTimeSegservice.GetAccessTimeSegs(pagingModel));

        }

        //create
        [CustomAuthorize(PrivilegeList.ManageAccessTimeSeg)]
        [HttpPost]
        public async Task<IActionResult> Create(AccessTimeSegCreateDto AccessTimeSegCreateDto)
        {
            if (AccessTimeSegCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(AccessTimeSegCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }

            return Ok(await _AccessTimeSegservice.CreateAccessTimeSeg(AccessTimeSegCreateDto));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewAccessTimeSeg, PrivilegeList.ManageAccessTimeSeg)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessTimeSegById(int id)
        {
            await _checkDataService.CheckAcessTimeSeg(id, (int)_userIdentity.CompId);
            return Ok(await _AccessTimeSegservice.GetAccessTimeSegById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageAccessTimeSeg)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccessTimeSeg(int id, AccessTimeSegUpdateDto AccessTimeSegUpdateDto)
        {
            await _checkDataService.CheckAcessTimeSeg(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckAcessTimeSeg(id, (int)AccessTimeSegUpdateDto.CompId);
            return Ok(await _AccessTimeSegservice.UpdateAccessTimeSeg(id, AccessTimeSegUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageAccessTimeSeg)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckAcessTimeSeg(id, (int)_userIdentity.CompId);
            return Ok(await _AccessTimeSegservice.UpdateDelete(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageAccessTimeSeg)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccessTimeSegs(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var AccessTimeSegs = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _AccessTimeSegservice.UpdateDeletes(AccessTimeSegs));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
