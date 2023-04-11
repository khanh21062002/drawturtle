using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.AppParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/face")]
    [Authorize]

    public class AppParamController : BaseController
    {
        private AppParamService _faceService;

        public AppParamController(AppParamService faceService)
        {
            _faceService = faceService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewAppParam, PrivilegeList.ManageAppParam)]
        [HttpGet]
        public async Task<IActionResult> GetListAppParams([FromQuery] AppParamGridPagingDto pagingModel)
        {
            return Ok(await _faceService.GetAppParams(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageAppParam)]
        [HttpPost]
        public async Task<IActionResult> Create(AppParamCreateDto faceCreateDto)
        {
            return Ok(await _faceService.CreateAppParam(faceCreateDto));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewAppParam, PrivilegeList.ManageAppParam)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppParamById(int id)
        {
            return Ok(await _faceService.GetAppParamById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageAppParam)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppParam(int id, AppParamUpdateDto faceUpdateDto)
        {
            return Ok(await _faceService.UpdateAppParam(id, faceUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageAppParam)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _faceService.DeleteAppParam(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageAppParam)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAppParams(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var AppParams = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _faceService.DeleteAppParam(AppParams));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
