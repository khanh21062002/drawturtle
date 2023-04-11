using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Areas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/areas")]
    public class AreasController : BaseController
    {
        private AreasService _areasService;
        public AreasController(AreasService areasService)
        {
            _areasService = areasService;
        }
        [CustomAuthorize(PrivilegeList.ViewAreas, PrivilegeList.ManageAreas)]
        [HttpGet]
        public async Task<IActionResult> GetAllAreas([FromQuery] AreasGridPagingDto pagingModel)
        {
            return Ok(await _areasService.GetAllAreas(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.ViewAreas, PrivilegeList.ManageAreas)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _areasService.GetById(id));
        }

        [CustomAuthorize( PrivilegeList.ManageAreas)]
        [HttpPost]
        public async Task<IActionResult> Create(AreasCreateDto areasCreateDto)
        {
            return Ok(await _areasService.CreateAreas(areasCreateDto));
        }

        [CustomAuthorize( PrivilegeList.ManageAreas)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AreasUpdateDto areasUpdateDto)
        {
            return Ok(await _areasService.UpdateAreas(id, areasUpdateDto));
        }

        [CustomAuthorize(PrivilegeList.ManageAreas)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _areasService.DeleteAreas(id));
        }
    }
}
