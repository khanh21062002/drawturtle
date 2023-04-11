using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Common;
using EPS.Service.Dtos.User;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class LookupService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public LookupService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }


        public async Task<List<SelectItem>> GetRoles()
        {
            return await _baseService.Filter<Role, SelectItem>(x => x.Status == 1).ToListAsync();
        }


        public async Task<List<SelectItem>> GetPrivileges()
        {
            return await _baseService.Filter<Privilege, SelectItem>().ToListAsync();
        }
        //Danh sách công ty
        public async Task<List<SelectItem>> GetCompanys()
        {
            int curUserId = _userIdentity.UserId;
            //check if user is admin, and get compnay to fill
            var user = await _baseService.Filter<User, UserDetailDto>(user => user.Id == curUserId && user.IsDelete == false).FirstAsync();
            if (user.isAdminByCompany)
            {
                //return await _baseService.Filter<Company, SelectItem>(comp => comp.IsDelete == false).ToListAsync();
            }
            else
            {
                string compId = user.CompanyId.ToString();
                //Neu khong phai thuoc cong ti admin
                //chi return cong ti cua chinh no
                //return await _baseService.Filter<Company, SelectItem>(comp => comp.Id.Equals(compId) && comp.IsDelete == false).ToListAsync();
            }
            return await _baseService.Filter<Company, SelectItem>().ToListAsync();
        }
        public async Task<List<CompanyTreeDto>> GetCompanyTree()
        {
            int companyId = _userIdentity.CompanyId;
            var companys = await _baseService.All<Company, CompanyTreeDto>().ToListAsync();
            var userCom = companys.Find(x => x.Id == companyId);
            var tree = new List<CompanyTreeDto>();
            return BuildTree(companys, tree, userCom.Id, userCom.ParentId);
        }

        private List<CompanyTreeDto> BuildTree(IEnumerable<CompanyTreeDto> allCompanys, List<CompanyTreeDto> tree, int? compId = 0, int? parentId = null, int index = 0)
        {
            List<CompanyTreeDto> lstChild = new List<CompanyTreeDto>();
            if (parentId != null && index == 0)
            {
                var item = allCompanys.Where(x => x.Id == compId).FirstOrDefault();
                lstChild.Add(item);
            }
            else
            {
                lstChild = allCompanys.Where(x => x.ParentId == parentId).ToList();
            }

            foreach (var item in lstChild)
            {
                index++;
                var childNode = new List<CompanyTreeDto>();
                childNode = BuildTree(allCompanys, childNode, 0, item.Id, index);

                if (childNode.Count > 0)
                {
                    item.HasChildren = true;

                    item.children = new List<CompanyTreeDto>();                    

                    foreach (var childItem in childNode)
                    {
                        item.children.Add(childItem);
                    }
                }

                item._children = new List<CompanyTreeDto>();
                foreach (var childItem in childNode)
                {
                    item._children.Add(childItem);
                }

                tree.Add(item);
            }

            return tree;
        }
    }
}
