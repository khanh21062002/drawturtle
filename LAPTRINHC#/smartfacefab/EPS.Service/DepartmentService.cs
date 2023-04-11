using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.AccessTimeSeg;
using EPS.Service.Dtos.Common;
using EPS.Service.Dtos.Department;
using EPS.Service.Dtos.Group;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class DepartmentService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IUserIdentity<int> _userIdentity;
        private IMapper _mapper;

        public DepartmentService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _userIdentity = userIdentity;
            _baseService = new EPSBaseService(repository, mapper);
        }

        //Get All
        public List<DepartmentGridDto> GetAllDepartment()
        {
            return _baseService.Filter<Department, DepartmentGridDto>(x => x.ComId == _userIdentity.CompId && !x.IsDelete).ToList();
        }

        public async Task<PagingResult<DepartmentGridDto>> GetDepartments(DepartmentGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Department, DepartmentGridDto>(pagingModel);
        }

        //create
        public async Task<int> CreateDepartment(DepartmentCreateDto categoryCreate, bool isExploiting = false)
        {

            if (categoryCreate.Type == 1)
            {
                var count = await _repository.CountAsync<Department>(x => x.Code == categoryCreate.Code.Trim() && x.IsDelete == false && x.ComId == categoryCreate.ComId);
                if (count > 0)
                {
                    throw new EPSException("CompanyService.Message.CodeClass");
                }
                else
                {
                    categoryCreate.IsDelete = false;
                    //all company  start with lv 1 - child of root company
                    if (categoryCreate.ParentId.HasValue)
                    {
                        var parent = await _baseService.Filter<Department, DepartmentDetailDto>(x => x.Id == categoryCreate.ParentId.Value).FirstAsync();
                        if (parent != null)
                        {
                            //tree level
                            int parentTreeLevel = parent.TreeLevelInt.HasValue ? parent.TreeLevelInt.Value : 0;
                            int treeLevel = parentTreeLevel + 1;
                            categoryCreate.TreeLevel = treeLevel.ToString();

                            //generate parent code
                            string parentCode = string.IsNullOrEmpty(parent.HiddenParentField) ? "1" : parent.HiddenParentField;

                            var countChild = await _repository.CountAsync<Department>(x => x.ParentId == categoryCreate.ParentId);
                            string maxChildFiled = "";

                            if (countChild > 0)
                            {
                                maxChildFiled = await _repository.Filter<Department>(x => x.ParentId == categoryCreate.ParentId).OrderByDescending(x => x.HiddenParentField).Select(x => x.HiddenParentField).FirstAsync();
                            }
                            else
                            {
                                maxChildFiled = parentCode + "001";
                            }

                            int maxChildFiledConvert = 0;
                            if (Int32.TryParse(maxChildFiled, out maxChildFiledConvert))
                            {
                                // you know that the parsing attempt
                                // was successful
                                maxChildFiledConvert++;
                                string numberchildFormat = maxChildFiledConvert.ToString();
                                categoryCreate.HiddenParentField = numberchildFormat;
                            }
                        }
                    }
                    else
                    {
                        categoryCreate.TreeLevel = "1";
                        var maxChildFiled = await _repository.Filter<Department>(x => !x.ParentId.HasValue || x.ParentId == 0).OrderByDescending(x => x.HiddenParentField).Select(x => x.HiddenParentField).FirstAsync();
                        int maxChildFiledConvert = 0;
                        if (Int32.TryParse(maxChildFiled, out maxChildFiledConvert))
                        {
                            // you know that the parsing attempt
                            // was successful
                            maxChildFiledConvert++;
                            string numberchildFormat = maxChildFiledConvert.ToString();
                            categoryCreate.HiddenParentField = numberchildFormat;
                        }
                    }

                    await _baseService.CreateAsync<Department, DepartmentCreateDto>(categoryCreate);
                }
            }
            else
            {
                var count = await _repository.CountAsync<Department>(x => x.Code == categoryCreate.Code.Trim() && x.IsDelete == false && x.ComId == categoryCreate.ComId);
                if (count > 0)
                {
                    throw new EPSException("CompanyService.Message.CodeDepartment");
                }
                else
                {
                    categoryCreate.IsDelete = false;
                    //all company  start with lv 1 - child of root company
                    if (categoryCreate.ParentId.HasValue)
                    {
                        var parent = await _baseService.Filter<Department, DepartmentDetailDto>(x => x.Id == categoryCreate.ParentId.Value).FirstAsync();
                        if (parent != null)
                        {
                            //tree level
                            int parentTreeLevel = parent.TreeLevelInt.HasValue ? parent.TreeLevelInt.Value : 0;
                            int treeLevel = parentTreeLevel + 1;
                            categoryCreate.TreeLevel = treeLevel.ToString();

                            //generate parent code
                            string parentCode = string.IsNullOrEmpty(parent.HiddenParentField) ? "1" : parent.HiddenParentField;

                            var countChild = await _repository.CountAsync<Department>(x => x.ParentId == categoryCreate.ParentId);
                            string maxChildFiled = "";

                            if (countChild > 0)
                            {
                                maxChildFiled = await _repository.Filter<Department>(x => x.ParentId == categoryCreate.ParentId).OrderByDescending(x => x.HiddenParentField).Select(x => x.HiddenParentField).FirstAsync();
                            }
                            else
                            {
                                maxChildFiled = parentCode + "001";
                            }

                            int maxChildFiledConvert = 0;
                            if (Int32.TryParse(maxChildFiled, out maxChildFiledConvert))
                            {
                                // you know that the parsing attempt
                                // was successful
                                maxChildFiledConvert++;
                                string numberchildFormat = maxChildFiledConvert.ToString();
                                categoryCreate.HiddenParentField = numberchildFormat;
                            }
                        }
                    }
                    else
                    {
                        categoryCreate.TreeLevel = "1";
                        var maxChildFiled = await _repository.Filter<Department>(x => !x.ParentId.HasValue || x.ParentId == 0).OrderByDescending(x => x.HiddenParentField).Select(x => x.HiddenParentField).FirstAsync();
                        int maxChildFiledConvert = 0;
                        if (Int32.TryParse(maxChildFiled, out maxChildFiledConvert))
                        {
                            // you know that the parsing attempt
                            // was successful
                            maxChildFiledConvert++;
                            string numberchildFormat = maxChildFiledConvert.ToString();
                            categoryCreate.HiddenParentField = numberchildFormat;
                        }
                    }

                    await _baseService.CreateAsync<Department, DepartmentCreateDto>(categoryCreate);
                }
            }
            return 0;
        }

        //Xóa phòng ban upadte Isdelete =1
        public async Task<int> UpdateIsDelete(int id)
        {
            var departmentParent = await _baseService.FindAsync<Department, DepartmentDetailDto>(id);
            var count = await _repository.CountAsync<Department>(x => x.ParentId == id && x.IsDelete == false);

            var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.DeptId == id && x.IsDelete == false);
            var countGroup = await _repository.CountAsync<Group>(x => x.DeptId == id && x.IsDelete == false);
            var countAcessTime = await _repository.CountAsync<AccessTimeSeg>(x => x.DeptId == id && x.IsDelete == false);
            var countPerson = await _repository.CountAsync<Person>(x => x.DeptId == id && x.IsDelete == false);
            if (count > 0 || countGroupAccess > 0 || countGroup > 0 || countAcessTime > 0 || countPerson > 0)
            {
                throw new EPSException(Constant.ErrorMessage.ERROR_DELETE);
            }
            else
            {
                var face = await _baseService.FindAsync<Department, DepartmentUpdateDto>(x => x.Id == id);
                face.IsDelete = true;
                await _baseService.UpdateAsync<Department, DepartmentUpdateDto>(id, face);
            }
            return 0;
        }

        //Xóa nhiều phòng ban upadte Isdelete =1
        public async Task<int> UpdateIsDeleteMuilti(int[] ids)
        {
            foreach (var item in ids)
            {
                var company = await _baseService.FindAsync<Department, DepartmentUpdateDto>(x => x.Id == item);
                var departmentParent = await _baseService.FindAsync<Department, DepartmentDetailDto>(company.Id);

                var count = await _repository.CountAsync<Department>(x => x.ParentId == company.Id && x.IsDelete == false);
                var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.DeptId == company.Id && x.IsDelete == false);
                var countGroup = await _repository.CountAsync<Group>(x => x.DeptId == company.Id && x.IsDelete == false);
                var countAcessTime = await _repository.CountAsync<AccessTimeSeg>(x => x.DeptId == company.Id && x.IsDelete == false);
                var countPerson = await _repository.CountAsync<Person>(x => x.DeptId == company.Id && x.IsDelete == false);

                if ((count > 0 && departmentParent.ParentId != null) || countGroupAccess > 0 || countGroup > 0 || countAcessTime > 0 || countPerson > 0)
                {
                    throw new EPSException(Constant.ErrorMessage.ERROR_DELETE);
                }
                else
                {
                    if (company != null)
                    {
                        company.IsDelete = true;
                        await _baseService.UpdateAsync<Department, DepartmentUpdateDto>(company.Id, company);
                    }
                }
            }
            return 0;
        }

        //detail
        public async Task<DepartmentDetailDto> GetDepartmentById(int id)
        {
            return await _baseService.FindAsync<Department, DepartmentDetailDto>(id);
        }

        //update
        public async Task<int> UpdateDepartment(int id, DepartmentUpdateDto editedDepartment)
        {
            var count = await _repository.CountAsync<Department>(x => x.Code == editedDepartment.Code.Trim() && x.IsDelete == false && x.Id != id && x.ComId == editedDepartment.ComId);
            if (count > 0)
            {
                throw new EPSException("CompanyService.Message.CodeDepartment");
            }
            else
            {
                var thisDepart = await _baseService.FindAsync<Department, DepartmentDetailDto>(id);
                // neu thay doi parent id
                if (!editedDepartment.ParentId.HasValue || editedDepartment.ParentId == 0)
                {
                    editedDepartment.TreeLevel = "1";
                    var maxChildFiled = await _repository.Filter<Department>(x => !x.ParentId.HasValue || x.ParentId == 0).OrderByDescending(x => x.HiddenParentField).Select(x => x.HiddenParentField).FirstAsync();
                    int maxChildFiledConvert = 0;
                    if (Int32.TryParse(maxChildFiled, out maxChildFiledConvert))
                    {
                        // you know that the parsing attempt
                        // was successful
                        maxChildFiledConvert++;
                        string numberchildFormat = maxChildFiledConvert.ToString();
                        editedDepartment.HiddenParentField = numberchildFormat;
                    }
                }
                else if (editedDepartment.ParentId != thisDepart.ParentId)
                {
                    // re generate 
                    var parent = await _baseService.Filter<Department, DepartmentDetailDto>(x => x.Id == editedDepartment.ParentId.Value).FirstAsync();
                    if (parent != null)
                    {
                        //tree level
                        int parentTreeLevel = parent.TreeLevelInt.HasValue ? parent.TreeLevelInt.Value : 0;
                        int treeLevel = parentTreeLevel + 1;
                        editedDepartment.TreeLevel = treeLevel.ToString();

                        //generate parent code
                        string parentCode = string.IsNullOrEmpty(parent.HiddenParentField) ? "1" : parent.HiddenParentField;

                        var countChild = await _repository.CountAsync<Department>(x => x.ParentId == editedDepartment.ParentId);
                        string maxChildFiled = "";

                        if (countChild > 0)
                        {
                            maxChildFiled = await _repository.Filter<Department>(x => x.ParentId == editedDepartment.ParentId).OrderByDescending(x => x.HiddenParentField).Select(x => x.HiddenParentField).FirstAsync();
                        }
                        else
                        {
                            maxChildFiled = parentCode + "001";
                        }

                        int maxChildFiledConvert = 0;
                        if (Int32.TryParse(maxChildFiled, out maxChildFiledConvert))
                        {
                            // you know that the parsing attempt
                            // was successful
                            maxChildFiledConvert++;
                            string numberchildFormat = maxChildFiledConvert.ToString();
                            editedDepartment.HiddenParentField = numberchildFormat;
                        }
                    }
                }
                await _baseService.UpdateAsync<Department, DepartmentUpdateDto>(id, editedDepartment);
            }
            return 0;
        }

        //delete
        public async Task<int> DeleteDepartment(int id)
        {
            return await _baseService.DeleteAsync<Department, int>(id);
        }

        public async Task<bool> CheckExistCode(int id, int comId, string code)
        {
            int count = 0;
            if (id != 0)
                count = await _repository.CountAsync<Department>(x => x.Code == code.Trim() && x.Id != id && x.IsDelete == false && x.ComId == comId);
            else
                count = await _repository.CountAsync<Department>(x => x.Code == code.Trim() && x.IsDelete == false && x.ComId == comId);

            if (count > 0)
                return false;
            else
                return true;
        }

        public async Task<int> DeleteDepartment(int[] ids)
        {
            return await _baseService.DeleteAsync<Department, int>(ids);
        }

        public async Task<int> RegenerateTree()
        {
            var departments = await _baseService.Filter<Department, DepartmentTreeDto>().ToListAsync();
            List<DepartmentTreeDto> treeSources = BuildTree1(departments, null, false);
            //convert to update dto and update this;
            foreach (var item in treeSources)
            {
                await LevelTreeTravesal(item);
            }
            return 0;
        }

        public async Task LevelTreeTravesal(DepartmentTreeDto node)
        {
            if (node != null)
            {
                //travel ( by update this node)
                int deptId = node.Id;
                var curDept = await _baseService.FindAsync<Department, DepartmentUpdateDto>(x => x.Id == deptId);
                curDept.HiddenParentField = node.TreeCode.ToString();
                curDept.TreeLevel = node.TreeLevelInt.ToString();
                await _baseService.UpdateAsync<Department, DepartmentUpdateDto>(deptId, curDept);

                //do next node
                foreach (DepartmentTreeDto child in node.Children)
                {
                    await LevelTreeTravesal(child); //<-- recursive
                }
            }
        }

        private List<DepartmentTreeDto> BuildTree1(IEnumerable<DepartmentTreeDto> allDepartments, int? rootDepartment = null, bool? showOnlyChild = false)
        {
            var tree = new List<DepartmentTreeDto>();
            List<DepartmentTreeDto> roots = new List<DepartmentTreeDto>();
            if (showOnlyChild == false)
            {
                if (rootDepartment == null)
                {
                    roots = allDepartments.Where(x => x.ParentId == null || x.ParentId == 0).ToList();
                }
                else
                {
                    var rootNode = allDepartments.FirstOrDefault(x => x.Id == rootDepartment);
                    if (rootNode != null)
                    {
                        roots.Add(rootNode);
                    }
                }
            }
            else
            {
                if (rootDepartment == null)
                {
                    roots = allDepartments.Where(x => x.ParentId == null || x.ParentId == 0).ToList();
                }
                else
                {
                    roots = allDepartments.Where(x => x.ParentId == rootDepartment).ToList();
                }
            }
            //for root, start from 100 parent code and 1 at level
            int rootCode = 100;
            int rootlevel = 1;
            foreach (var r in roots)
            {
                rootCode++;
                r.TreeCode = rootCode;
                r.TreeLevelInt = rootlevel;
                tree.Add(r);
                DepartmentTreeDto nextNode = r;
                DepartmentTreeDto currentNode;
                DepartmentTreeDto parentNode;
                int index;
                while (nextNode != null)
                {
                    currentNode = nextNode;
                    nextNode = null;
                    int childCode = 0;
                    int childLevel = 0;
                    if (currentNode.TreeCode.HasValue && currentNode.TreeLevelInt.HasValue)
                    {
                        childCode = currentNode.TreeCode.Value * 1000;
                        childLevel = currentNode.TreeLevelInt.Value + 1;
                    }
                    foreach (var child in allDepartments.Where(x => x.ParentId == currentNode.Id))
                    {
                        child.Parent = currentNode;
                        child.TreeLevelInt = childLevel;
                        childCode++;
                        child.TreeCode = childCode;
                        currentNode.Children.Add(child);
                        currentNode._children.Add(child);
                    }
                    if (currentNode.Children.Any())
                    {
                        nextNode = currentNode.Children[0];
                    }
                    else
                    {
                        while (currentNode.Parent != null)
                        {
                            parentNode = currentNode.Parent;
                            index = parentNode.Children.IndexOf(currentNode);
                            if (index < parentNode.Children.Count - 1)
                            {
                                nextNode = parentNode.Children[index + 1];
                                break;
                            }
                            else
                            {
                                currentNode = parentNode;
                            }
                        }
                    }
                }
            }
            return tree;
        }

        public async Task<List<DepartmentTreeDto>> GetDepartmentTreeView()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var deparmtents = await _baseService.Filter<Department, DepartmentTreeDto>(x => x.ComId == curCompId && x.IsDelete == false && x.Type != AppConstants.DepartmentType.SCHOOL && x.Type != AppConstants.DepartmentType.DEFAULT_PARENT_DEPT).ToListAsync();
            return BuildTreeForView(deparmtents);
        }

        private List<DepartmentTreeDto> BuildTreeForView(IEnumerable<DepartmentTreeDto> allDepartments)
        {
            var tree = new List<DepartmentTreeDto>();
            List<DepartmentTreeDto> roots = new List<DepartmentTreeDto>();
            roots = allDepartments.Where(x => x.ParentId == null || x.ParentId == 0).ToList();

            foreach (var r in roots)
            {
                tree.Add(r);
                DepartmentTreeDto nextNode = r;
                DepartmentTreeDto currentNode;
                DepartmentTreeDto parentNode;
                int index;
                while (nextNode != null)
                {
                    currentNode = nextNode;
                    nextNode = null;
                    foreach (var child in allDepartments.Where(x => x.ParentId == currentNode.Id))
                    {
                        child.Parent = currentNode;
                        currentNode.Children.Add(child);
                        currentNode._children.Add(child);
                    }
                    if (currentNode.Children.Any())
                    {
                        nextNode = currentNode.Children[0];
                    }
                    else
                    {
                        while (currentNode.Parent != null)
                        {
                            parentNode = currentNode.Parent;
                            index = parentNode.Children.IndexOf(currentNode);
                            if (index < parentNode.Children.Count - 1)
                            {
                                nextNode = parentNode.Children[index + 1];
                                break;
                            }
                            else
                            {
                                currentNode = parentNode;
                            }
                        }
                    }
                }
            }
            return tree;
        }
    }
}


