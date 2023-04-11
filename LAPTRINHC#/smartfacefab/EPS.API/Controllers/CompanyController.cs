using EPS.API.Helpers;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Company;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        //list all
        [CustomAuthorize(PrivilegeList.ViewCompany, PrivilegeList.ManageCompany)]
        [HttpGet]
        public async Task<IActionResult> GetListCompanys([FromQuery] CompanyGridPagingDto pagingModel)
        {
            if(_userIdentity.CompId != 1)
            {
                throw new EPSException("Bạn không có quyền truy cập API này!");
            }

            return Ok(await _companyService.GetCompanys(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageCompany)]
        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateDto companyCreateDto)
        {
            return Ok(await _companyService.CreateCompany(companyCreateDto));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewCompany, PrivilegeList.ManageCompany)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
         
            return Ok(await _companyService.GetCompanyById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageCompany)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyUpdateDto companyUpdateDto)
        {
            return Ok(await _companyService.UpdateCompany(id, companyUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageCompany)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _companyService.UpdateIsDelete(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageCompany)]
        [HttpDelete]
        public async Task<IActionResult> DeleteCompanys(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var Companys = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _companyService.UpdateIsDeleteMuilti(Companys));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [CustomAuthorize(PrivilegeList.ManageCompany)]
        [HttpGet("{checkcode}/{id}/{code}")]
        public async Task<IActionResult> CheckExistCode(int id, string code)
        {
            return Ok(await _companyService.CheckExistCode(id, code));
        }

        [CustomAuthorize(PrivilegeList.ManageCompany)]
        [HttpGet("{id}/{taxCode}")]
        public async Task<IActionResult> CheckExistTaxCode(int id, string taxCode)
        {
            return Ok(await _companyService.CheckExistTaxCode(id, taxCode));
        }
    }
}
