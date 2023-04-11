using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Company;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/company")]
    [Authorize]
    public class CompanyController : BaseController
    {
        private CompanyService _companyService;
        private IUserIdentity<int> _userIdentity;

        public CompanyController(CompanyService companyService, IUserIdentity<int> userIdentity)
        {
            _companyService = companyService;
            _userIdentity = userIdentity;
        }

        //detail
        [HttpGet("{id}")]
        [CustomAuthorize(PrivilegeList.ViewCompany, PrivilegeList.ManageCompany)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            return Ok(await _companyService.GetCompanyById(id));
        }

        //update
        [HttpPut("{id}")]
        [CustomAuthorize(PrivilegeList.ManageCompany)]
        public async Task<IActionResult> UpdateCompany(int id, CompanyUpdateDto companyUpdateDto)
        {
            return Ok(await _companyService.UpdateCompany(id, companyUpdateDto));
        }

        //create
        [HttpPost]
        [CustomAuthorize(PrivilegeList.ManageCompany)]
        public async Task<IActionResult> Create(CompanyCreateDto companyCreateDto)
        {
            return Ok(await _companyService.CreateCompany(companyCreateDto));
        }

        //delete
        [HttpDelete("{id}")]
        [CustomAuthorize(PrivilegeList.ManageCompany)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _companyService.DeleteCompany(id));
        }
    }
}
