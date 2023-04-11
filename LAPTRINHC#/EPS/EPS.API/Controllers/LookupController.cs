using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service.Dtos.Common;
using Microsoft.EntityFrameworkCore;
using EPS.Service;

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

        [HttpGet("privileges")]
        public async Task<IActionResult> GetPrivileges()
        {
            return Ok(await _lookupService.GetPrivileges());
        }

        [HttpGet("company-tree")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCompanyTree()
        {
            return Ok(await _lookupService.GetCompanyTree());
        }
        [HttpGet("companys")]
        public async Task<IActionResult> GetCompany()
        {
            return Ok(await _lookupService.GetCompanys());
        }
    }
}
