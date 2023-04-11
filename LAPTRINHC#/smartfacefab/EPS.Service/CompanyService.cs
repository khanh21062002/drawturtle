using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Company;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Service;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using EPS.Service.Dtos.Department;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using EPS.Utils.Repository.Audit;
using EPS.Service.Dtos.Grades;
using EPS.Service.Dtos.Group;
using EPS.Service.Dtos.AccessTimeSeg;
using EPS.Service.Dtos.Person;

namespace EPS.Service
{
    public class CompanyService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private IUserIdentity<int> _userIdentity;
        private DepartmentService _departmentService;


        public CompanyService(EPSRepository repository, IMapper mapper, IOptions<AppSettings> appSettings, IUserIdentity<int> userIdentity, DepartmentService departmentService)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _appSettings = appSettings.Value;
            _userIdentity = userIdentity;
            _departmentService = departmentService;
        }

        //Get All
        public List<CompanyGridDto> GetAllCompany()
        {
            return _baseService.Filter<Company, CompanyGridDto>(x => !x.IsDelete).ToList();
        }

        public async Task<PagingResult<CompanyGridDto>> GetCompanys(CompanyGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Company, CompanyGridDto>(pagingModel);
        }

        public async Task<int> CreateCompany(CompanyCreateDto categoryCreate, bool isExploiting = false)
        {
            var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(x => x.Id == _userIdentity.CompId);
            string hiddentParentFieldAuthor = "";
            if (curComp != null)
            {
                hiddentParentFieldAuthor = curComp.HiddenParentField;
            }
            else
            {
                var errors1 = "CompanyService.Message.UserPrivileges";
                throw new EPSException(errors1);
            }

            var count = await _repository.CountAsync<Company>(x => x.Code == categoryCreate.Code.Trim() && x.IsDelete == false);
            if (count > 0)
            {
                var errors1 = "CompanyService.Message.CompanyCode";
                throw new EPSException(errors1);
            }
            else
            {
                categoryCreate.IsDelete = false;
                //all company  start with lv 1 - child of root company
                if (categoryCreate.ParentId.HasValue)
                {
                    var parent = await _baseService.Filter<Company, CompanyDetailDto>(x => x.Id == categoryCreate.ParentId.Value).FirstAsync();
                    if (parent != null)
                    {
                        //tree level
                        int parentTreeLevel = parent.TreeLevel.HasValue ? parent.TreeLevel.Value : 0;
                        int treeLevel = parentTreeLevel + 1;
                        categoryCreate.TreeLevel = treeLevel;

                        //generate parent code
                        string parentCode = string.IsNullOrEmpty(parent.HiddenParentField) ? "1" : parent.HiddenParentField;
                        if (!parentCode.StartsWith(hiddentParentFieldAuthor))
                        {
                            var errors1 = "CompanyService.Message.UserPrivileges";
                            throw new EPSException(errors1);

                        }
                        var countChild = await _repository.CountAsync<Company>(x => x.ParentId == categoryCreate.ParentId);
                        string maxChildFiled = "";

                        if (countChild > 0)
                        {
                            maxChildFiled = await _repository.Filter<Company>(x => x.ParentId == categoryCreate.ParentId).OrderByDescending(x => x.HiddenParentField).Select(x => x.HiddenParentField).FirstAsync();
                        }
                        else
                        {
                            maxChildFiled = parentCode + "00";
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
                await _baseService.CreateAsync<Company, CompanyCreateDto>(categoryCreate);
                //if company is chool, create default dept for parent 
                if (categoryCreate.CompanyType == AppConstants.CompanyType.SCHOOL)
                    try
                    {
                        DepartmentCreateDto deptDefault = new DepartmentCreateDto();
                        deptDefault.IsDelete = false;
                        deptDefault.Name = "CompanyService.Message.ParentSchool";
                        deptDefault.ComId = categoryCreate.Id;
                        deptDefault.Code = categoryCreate.Code + AppConstants.DEFAULT_PARENT_CODE.DEFAULT_PARENT_CODE_TAIL;
                        deptDefault.Status = 1;
                        deptDefault.Type = AppConstants.DepartmentType.DEFAULT_PARENT_DEPT;
                        await _departmentService.CreateDepartment(deptDefault);
                        await InitDefaultResources(categoryCreate);
                    }
                    catch (Exception ex)
                    {
                        //if has exception, throw Exception with Mess
                        string message = "Exception when init defaults resources:  " + ex.Message;
                        throw new Exception(message);
                    }

                //Thêm mới person không xác định
                PersonCreateDto createPerson = new PersonCreateDto();
                createPerson.PersonId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                createPerson.CompId = categoryCreate.Id;
                createPerson.DeptId = 0;
                createPerson.PersonCode = "NA";
                createPerson.FullName = "Unknown";
                createPerson.RegisterTime = DateTime.Now;
                createPerson.Status = 1;
                createPerson.IsDelete = false;
                createPerson.PersonType = 2;
                await _baseService.CreateAsync<Person, PersonCreateDto>(createPerson);

                return categoryCreate.Id;
            }
        }

        public async Task InitDefaultResources(CompanyCreateDto company)
        {
            // Phòng ban mặc định
            DepartmentCreateDto deptDefault = new DepartmentCreateDto();
            deptDefault.IsDelete = false;
            deptDefault.Name = "CompanyService.Message.DepartmentSchool";
            deptDefault.ComId = company.Id;
            deptDefault.Code = company.Code + "_default_dept";
            deptDefault.Status = 1;
            deptDefault.Type = AppConstants.DepartmentType.FACTORY;
            await _departmentService.CreateDepartment(deptDefault);

            // Nhóm người mặc định
            GroupCreateDto groupDefault = new GroupCreateDto();
            groupDefault.GroupId = 0;
            groupDefault.DeptId = 0;
            groupDefault.GroupCode = company.Code + "_default_group";
            groupDefault.GroupName = "CompanyService.Message.GroupSchool";
            groupDefault.CompId = company.Id;
            groupDefault.Status = 1;
            groupDefault.IsDelete = false;

            await _baseService.CreateAsync<Group, GroupCreateDto>(groupDefault);

            //Tạo mới khung giờ mặc định
            AccessTimeSegCreateDto accessTimeCreate = new AccessTimeSegCreateDto();
            accessTimeCreate.Id = 0;
            accessTimeCreate.CompId = company.Id;
            accessTimeCreate.DeptId = 0;
            accessTimeCreate.GroupId = 0;
            accessTimeCreate.TimeSegName = "CompanyService.Message.TimeSegSchool";
            accessTimeCreate.Status = 1;
            accessTimeCreate.IsDelete = false;

            accessTimeCreate.SundayStart1 = "00:00:00";
            accessTimeCreate.MondayStart1 = "00:00:00";
            accessTimeCreate.TuesdayStart1 = "00:00:00";
            accessTimeCreate.WednesdayStart1 = "00:00:00";
            accessTimeCreate.ThursdayStart1 = "00:00:00";
            accessTimeCreate.FridayStart1 = "00:00:00";
            accessTimeCreate.SaturdayStart1 = "00:00:00";

            accessTimeCreate.SundayStart2 = "00:00:00";
            accessTimeCreate.MondayStart2 = "00:00:00";
            accessTimeCreate.TuesdayStart2 = "00:00:00";
            accessTimeCreate.WednesdayStart2 = "00:00:00";
            accessTimeCreate.ThursdayStart2 = "00:00:00";
            accessTimeCreate.FridayStart2 = "00:00:00";
            accessTimeCreate.SaturdayStart2 = "00:00:00";

            accessTimeCreate.SundayStart3 = "00:00:00";
            accessTimeCreate.MondayStart3 = "00:00:00";
            accessTimeCreate.TuesdayStart3 = "00:00:00";
            accessTimeCreate.WednesdayStart3 = "00:00:00";
            accessTimeCreate.ThursdayStart3 = "00:00:00";
            accessTimeCreate.FridayStart3 = "00:00:00";
            accessTimeCreate.SaturdayStart3 = "00:00:00";

            accessTimeCreate.SundayStart4 = "00:00:00";
            accessTimeCreate.MondayStart4 = "00:00:00";
            accessTimeCreate.TuesdayStart4 = "00:00:00";
            accessTimeCreate.WednesdayStart4 = "00:00:00";
            accessTimeCreate.ThursdayStart4 = "00:00:00";
            accessTimeCreate.FridayStart4 = "00:00:00";
            accessTimeCreate.SaturdayStart4 = "00:00:00";

            accessTimeCreate.SundayEnd1 = "23:59:00";
            accessTimeCreate.MondayEnd1 = "23:59:00";
            accessTimeCreate.TuesdayEnd1 = "23:59:00";
            accessTimeCreate.WednesdayEnd1 = "23:59:00";
            accessTimeCreate.ThursdayEnd1 = "23:59:00";
            accessTimeCreate.FridayEnd1 = "23:59:00";
            accessTimeCreate.SaturdayEnd1 = "23:59:00";

            accessTimeCreate.SundayEnd2 = "00:00:00";
            accessTimeCreate.MondayEnd2 = "00:00:00";
            accessTimeCreate.TuesdayEnd2 = "00:00:00";
            accessTimeCreate.WednesdayEnd2 = "00:00:00";
            accessTimeCreate.ThursdayEnd2 = "00:00:00";
            accessTimeCreate.FridayEnd2 = "00:00:00";
            accessTimeCreate.SaturdayEnd2 = "00:00:00";

            accessTimeCreate.SundayEnd3 = "00:00:00";
            accessTimeCreate.MondayEnd3 = "00:00:00";
            accessTimeCreate.TuesdayEnd3 = "00:00:00";
            accessTimeCreate.WednesdayEnd3 = "00:00:00";
            accessTimeCreate.ThursdayEnd3 = "00:00:00";
            accessTimeCreate.FridayEnd3 = "00:00:00";
            accessTimeCreate.SaturdayEnd3 = "00:00:00";

            accessTimeCreate.SundayEnd4 = "00:00:00";
            accessTimeCreate.MondayEnd4 = "00:00:00";
            accessTimeCreate.TuesdayEnd4 = "00:00:00";
            accessTimeCreate.WednesdayEnd4 = "00:00:00";
            accessTimeCreate.ThursdayEnd4 = "00:00:00";
            accessTimeCreate.FridayEnd4 = "00:00:00";
            accessTimeCreate.SaturdayEnd4 = "00:00:00";

            await _baseService.CreateAsync<AccessTimeSeg, AccessTimeSegCreateDto>(accessTimeCreate);

            //if company is school, init default grades, 
            if (company.CompanyType == AppConstants.CompanyType.SCHOOL)
            {
                // Khối mặc định, từ khối 1 đến khối 12
                for (int i = 0; i < 12; i++)
                {
                    var gradeNumber = i + 1;
                    string gradeName = "CompanyService.Message.GradesSchool " + gradeNumber;
                    GradesCreateDto gradeDto = new GradesCreateDto();
                    gradeDto.Id = 0;
                    gradeDto.CompId = company.Id;
                    gradeDto.Name = gradeName;
                    gradeDto.GradeNumber = gradeNumber;
                    gradeDto.Descriptions = "CompanyService.Message.GradesStudent" + gradeNumber;
                    gradeDto.IsDelete = false;
                    await _baseService.CreateAsync<Grades, GradesCreateDto>(gradeDto);
                }
            }
        }

        public async Task<CompanyDetailDto> GetCompanyById(int id)
        {
            var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(x => x.Id == _userIdentity.CompId);
            string hiddentParentFieldAuthor = "";
            if (curComp != null)
            {
                hiddentParentFieldAuthor = curComp.HiddenParentField;
            }
            else
            {
                var errors1 = "CompanyService.Message.UserPrivileges";
                throw new EPSException(errors1);
            }
            var thisComp = await _baseService.Filter<Company, CompanyDetailDto>(x => x.Id == id && x.IsDelete == false).FirstAsync();

            if (thisComp != null)
            {
                string parentHiddenField = thisComp.HiddenParentField;
                if (!parentHiddenField.StartsWith(hiddentParentFieldAuthor))
                {
                    var errors1 = "CompanyService.Message.UserPrivileges";
                    throw new EPSException(errors1);
                }
            }
            else
            {
                var errors1 = "AuthorizationService.Message.UserPrivileges4";
                throw new EPSException(errors1);
            }

            return await _baseService.FindAsync<Company, CompanyDetailDto>(id);
        }

        public async Task<int> UpdateCompany(int id, CompanyUpdateDto editedCompany)
        {
            var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(x => x.Id == _userIdentity.CompId);
            string hiddentParentFieldAuthor = "";
            if (curComp != null)
            {
                hiddentParentFieldAuthor = curComp.HiddenParentField;
            }
            else
            {
                var errors1 = "CompanyService.Message.UserPrivileges";
                throw new EPSException(errors1);
            }
            var count = await _repository.CountAsync<Company>(x => x.Code == editedCompany.Code.Trim() && x.IsDelete == false && x.Id != id);
            if (count > 0)
            {
                var errors1 = "CompanyService.Message.CompanyCode";
                throw new EPSException(errors1);
            }
            else
            {
                var thisComp = await _baseService.Filter<Company, CompanyDetailDto>(x => x.Id == id && x.IsDelete == false).FirstAsync();

                if (thisComp != null)
                    if (thisComp != null)
                    {
                        string parentHiddenField = thisComp.HiddenParentField;
                        if (!parentHiddenField.StartsWith(hiddentParentFieldAuthor))
                        {
                            var errors1 = "CompanyService.Message.UserPrivileges";
                            throw new EPSException(errors1);
                        }
                    }
                    else
                    {
                        var errors1 = "AuthorizationService.Message.UserPrivileges4";
                        throw new EPSException(errors1);
                    }

                if (editedCompany.ParentId != thisComp.ParentId)
                {
                    var errors1 = "CompanyService.Message.UserPrivileges";
                    throw new EPSException(errors1);
                }
                if (id != editedCompany.Id)
                    if (id != editedCompany.Id)
                    {
                        var errors1 = "AuthorizationService.Message.UserNew";
                        throw new EPSException(errors1);
                    }

                await _baseService.UpdateAsync<Company, CompanyUpdateDto>(id, editedCompany);
                return 0;
            }
        }

        //Xóa đơn vị upadte Isdelete = 1
        public async Task<int> UpdateIsDelete(int id)
        {
            var currentComp = await _baseService.FindAsync<Company, CompanyDetailDto>(x => x.Id == _userIdentity.CompId);
            string hiddentParentFieldAuthor = "";
            if (currentComp != null)
            {
                hiddentParentFieldAuthor = currentComp.HiddenParentField;
            }
            else
            {
                var errors1 = "CompanyService.Message.UserPrivileges";
                throw new EPSException(errors1);
            }

            var departmentParent = await _baseService.FindAsync<Department, DepartmentDetailDto>(id);
            var user = await _baseService.FindAsync<User, EPS.Service.Dtos.User.UserDetailDto>(id);
            var count = await _repository.CountAsync<Department>(x => x.ComId == id && x.IsDelete == false);
            var countUser = await _repository.CountAsync<User>(x => x.CompId == id && x.IsDelete == false);
            var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.CompId == id && x.IsDelete == false);
            var countGroup = await _repository.CountAsync<Group>(x => x.CompId == id && x.IsDelete == false);
            var countMachine = await _repository.CountAsync<Machine>(x => x.CompId == id && x.IsDelete == false);
            var countShiftTime = await _repository.CountAsync<ShiftTime>(x => x.CompId == id && x.IsDelete == false);
            var countAcessTime = await _repository.CountAsync<AccessTimeSeg>(x => x.CompId == id && x.IsDelete == false);
            var countPerson = await _repository.CountAsync<Person>(x => x.CompId == id && x.IsDelete == false);
            var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(x => x.Id == id);
            var countCompanyChild = 0;

            if (curComp != null)
            {
                string hiddentParentField = curComp.HiddenParentField;
                if (!string.IsNullOrEmpty(hiddentParentField))
                {
                    countCompanyChild = await _repository.CountAsync<Company>(x => x.HiddenParentField.StartsWith(hiddentParentField) && x.Id != id && x.IsDelete == false);
                }
            }

            if (count > 0 || countUser > 0 || countGroupAccess > 0 || countGroup > 0 || countMachine > 0 || countShiftTime > 0 || countAcessTime > 0 || countPerson > 0 || countCompanyChild > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                throw new EPSException(errors1);
            }
            else
            {
                var face = await _baseService.FindAsync<Company, CompanyUpdateDto>(x => x.Id == id);
                face.IsDelete = true;
                string parentHiddenField = face.HiddenParentField;
                if (!parentHiddenField.StartsWith(hiddentParentFieldAuthor))
                {
                    var errors1 = "CompanyService.Message.UserPrivileges";
                    throw new EPSException(errors1);
                }
                await _baseService.UpdateAsync<Company, CompanyUpdateDto>(id, face);
            }
            return 0;
        }

        //Xóa đơn vị upadte Isdelete =1
        public async Task<int> UpdateIsDeleteMuilti(int[] ids)
        {
            foreach (var item in ids)
            {
                var company = await _baseService.FindAsync<Company, CompanyUpdateDto>(x => x.Id == item);
                var departmentParent = await _baseService.FindAsync<Department, DepartmentDetailDto>(company.Id);
                var user = await _baseService.FindAsync<User, EPS.Service.Dtos.User.UserDetailDto>(company.Id);
                var count = await _repository.CountAsync<Department>(x => x.ComId == company.Id && x.IsDelete == false);
                var countUser = await _repository.CountAsync<User>(x => x.CompId == company.Id && x.IsDelete == false);
                var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.CompId == company.Id && x.IsDelete == false);
                var countGroup = await _repository.CountAsync<Group>(x => x.CompId == company.Id && x.IsDelete == false);
                var countMachine = await _repository.CountAsync<Machine>(x => x.CompId == company.Id && x.IsDelete == false);
                var countShiftTime = await _repository.CountAsync<ShiftTime>(x => x.CompId == company.Id && x.IsDelete == false);
                var countAcessTime = await _repository.CountAsync<AccessTimeSeg>(x => x.CompId == company.Id && x.IsDelete == false);
                var countPerson = await _repository.CountAsync<Person>(x => x.CompId == company.Id && x.IsDelete == false);

                if (count > 0 || countUser > 0 || countGroupAccess > 0 || countGroup > 0 || countMachine > 0 || countShiftTime > 0 || countAcessTime > 0 || countPerson > 0)
                {
                    var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                    throw new EPSException(errors1);
                }
                else
                {
                    if (company != null)
                    {
                        company.IsDelete = true;
                        await _baseService.UpdateAsync<Company, CompanyUpdateDto>(company.Id, company);
                    }
                }
            }
            return 0;
        }

        public async Task<int> DeleteCompany(int id)
        {
            return await _baseService.DeleteAsync<Company, int>(id);
        }

        public async Task<bool> CheckExistTaxCode(int id, string taxCode)
        {
            int count = 0;
            if (id != 0)
                count = await _repository.CountAsync<Company>(x => x.TaxCode == taxCode.Trim() && x.Id != id && x.IsDelete == false);
            else
                count = await _repository.CountAsync<Company>(x => x.TaxCode == taxCode.Trim() && x.IsDelete == false);

            if (count > 0)
                return false;
            else
                return true;
        }

        public async Task<bool> CheckExistCode(int id, string code)
        {
            int count = 0;
            if (id != 0)
                count = await _repository.CountAsync<Company>(x => x.Code == code.Trim() && x.Id != id && x.IsDelete == false);
            else
                count = await _repository.CountAsync<Company>(x => x.Code == code.Trim() && x.IsDelete == false);

            if (count > 0)
                return false;
            else
                return true;
        }

        public async Task<int> DeleteCompany(int[] ids)
        {
            return await _baseService.DeleteAsync<Company, int>(ids);
        }
    }
}
