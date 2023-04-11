using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Company;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class CompanyService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public CompanyService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        public async Task<CompanyDetailDto> GetCompanyById(int id)
        {
            return await _baseService.FindAsync<Company, CompanyDetailDto>(id);
        }

        public async Task<int> UpdateCompany(int id, CompanyUpdateDto editCompany)
        {
            bool has = _repository.Contain<Company>(x => x.Code == editCompany.Code.Trim() && x.Id != id);
            if (has)
            {
                var error = "System.Company.Error.CompanyCodeExists";
                throw new EPSException(error);
            }

            await _baseService.UpdateAsync<Company, CompanyUpdateDto>(id, editCompany);
            return id;
        }

        public async Task<int> CreateCompany(CompanyCreateDto createCompany, bool isExploiting = false)
        {
            bool has = _repository.Contain<Company>(x => x.Code == createCompany.Code.Trim());
            if (has)
            {
                var error = "System.Company.Error.CompanyCodeExists";
                throw new EPSException(error);
            }

            await _baseService.CreateAsync<Company, CompanyCreateDto>(createCompany);
            return createCompany.Id;
        }

        public async Task<int> DeleteCompany(int id)
        {
            try
            {
                var user = await _repository.FindAsync<User>(x => x.CompanyId == id);
                if(user != null)
                {
                    var error = "Messages.CannotDelete";
                    throw new EPSException(error);
                }
                return await _baseService.DeleteAsync<Company, int>(id);
            }
            catch (Exception)
            {
                var error = "Messages.CannotDelete";
                throw new EPSException(error);
            }
            
        }
    }
}
