using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Common;
using EPS.Service.Dtos.Company;
using EPS.Service.Dtos.Department;
using EPS.Service.Dtos.FAQuestions;
using EPS.Service.Dtos.Group;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.User;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
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
        private ShiftTimeService _shiftTimeService;

        //All look up service need to filter data by current user login company
        public LookupService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity, ShiftTimeService shiftTimeService)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
            _shiftTimeService = shiftTimeService;
        }

        //Danh sách công ty
        public async Task<List<SelectItem>> GetCompanys()
        {
            int curUserId = _userIdentity.UserId;
            //check if user is admin, and get compnay to fill
            var user = await _baseService.Filter<User, UserDetailDto>(user => user.Id == curUserId).FirstAsync();
            if (user.isAdminByCompany)
            {
                return await _baseService.Filter<Company, SelectItem>(comp => comp.IsDelete == false).ToListAsync();
            }
            else
            {
                string compId = user.CompId.ToString();
                //Neu khong phai thuoc cong ti admin
                //chi return cong ti cua chinh no
                return await _baseService.Filter<Company, SelectItem>(comp => comp.Id.Equals(compId) && comp.IsDelete == false).ToListAsync();
            }
            return await _baseService.Filter<Company, SelectItem>(comp => comp.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetCompanyas()
        {
            return await _baseService.Filter<Company, SelectItem>(comp => comp.IsDelete == false).ToListAsync();
        }

        //Load thời gian hiện tại
        public DateTime GetCurrentDateTime()
        {
            var current = DateTime.Now;
            return current;
        }

        //Danh sách công ty tree
        public async Task<List<SelectItem>> GetCompanysTrees()
        {
            int curUserId = _userIdentity.UserId;
            //check if user is admin, and get compnay to fill
            var user = await _baseService.Filter<User, UserDetailDto>(user => user.Id == curUserId).FirstAsync();
            return await _baseService.Filter<Company, SelectItem>(comp => comp.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetGroupAccesss()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<GroupAccess, SelectItem>(groupAccess => groupAccess.IsDelete == false && groupAccess.CompId == curCompId).ToListAsync();
        }

        public async Task<List<SelectItem>> GetGroupAccesssByCompId(int compId)
        {
            return await _baseService.Filter<GroupAccess, SelectItem>(groupAccess => groupAccess.filterId == compId && groupAccess.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetAccessTimeSegs()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<AccessTimeSeg, SelectItem>(groupAccessTime => groupAccessTime.IsDelete == false && groupAccessTime.CompId == curCompId).ToListAsync();
        }

        //danh ách câu hỏi
        public async Task<List<FAQuestionsGridDto>> GetQuestions(string locale)
        {
            var ret = await _baseService.Filter<FAQuestions, FAQuestionsGridDto>(x => x.Type == locale).ToListAsync();
            return ret;
        }

        public async Task<List<SelectItem>> GetAccessTimeSegsByCompId(int compId)
        {
            return await _baseService.Filter<AccessTimeSeg, SelectItem>(groupAccessTime => groupAccessTime.filterId == compId && groupAccessTime.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetAccessTimeSegsByGroupId(int groupId)
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<AccessTimeSeg, SelectItem>(groupAccessTime => groupAccessTime.filterId == curCompId && groupAccessTime.GroupId == groupId && groupAccessTime.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetDepartments()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var com = await _baseService.FindAsync<Company, CompanyDetailDto>(curCompId);
            //check com is school?
            return await _baseService.Filter<Department, SelectItem>(dept => dept.IsDelete == false && dept.filterId == curCompId && dept.DepartmentType != AppConstants.DepartmentType.SCHOOL).ToListAsync();
        }

        //Danh sách tất cả phòng ban (cả lớp)
        public async Task<List<SelectItem>> GetAllDepartments()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var com = await _baseService.FindAsync<Company, CompanyDetailDto>(curCompId);
            //check com is school?
            return await _baseService.Filter<Department, SelectItem>(dept => dept.IsDelete == false && dept.filterId == curCompId).ToListAsync();
        }

        public async Task<List<SelectItem>> GetDepartmentsByCompId(int compId)
        {
            return await _baseService.Filter<Department, SelectItem>(dept => dept.filterId == compId && dept.IsDelete == false).ToListAsync();
        }

        public async Task<List<UserNameItem>> GetDevice()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Device, UserNameItem>(x => x.IsDelete == false && x.CompId == curCompId).ToListAsync();
        }

        public async Task<List<SelectItem>> GetDevices()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Device, SelectItem>(x => x.IsDelete == false && x.CompId == curCompId).ToListAsync();
        }

        public async Task<List<SelectItem>> GetAllDependentDevice()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<ViewDeviceFeatures, SelectItem>(x => x.IsDelete == false && x.CompId == curCompId && x.FeatureId == 3).ToListAsync();
        }
        public async Task<List<SelectItem>> GetMon()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Menu, SelectItem>(x => x.IsDelete == false && x.CompId == curCompId && x.Type == 1).ToListAsync();
        }
        public async Task<List<SelectItem>> GetAreas()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Areas, SelectItem>(x => x.IsDelete == false && x.CompId == curCompId).ToListAsync();
        }

        public async Task<List<UserNameItem>> GetAreaName()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Areas, UserNameItem>(x => x.IsDelete == false && x.CompId == curCompId).ToListAsync();
        }

        //get areas where status == 1
        public async Task<List<SelectItem>> GetAreasStatus()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Areas, SelectItem>(x => x.IsDelete == false && x.CompId == curCompId && x.Status == 1).ToListAsync();
        }

        public async Task<List<SelectItem>> GetFeatures()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Features, SelectItem>(machine => machine.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetDepartmentsStatus()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var com = await _baseService.FindAsync<Company, CompanyDetailDto>(curCompId);
            //if(com != null && com.Result.)
            //check com is school?
            return await _baseService.Filter<Department, SelectItem>(dept => dept.IsDelete == false && dept.filterId == curCompId && dept.DepartmentType != AppConstants.DepartmentType.SCHOOL && dept.Status == 1).ToListAsync();
        }

        public async Task<List<SelectItem>> GetFeaturesStatus()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Features, SelectItem>(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetGroups()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Group, SelectItem>(group => group.IsDelete == false && group.filterId == curCompId).ToListAsync();
        }

        public async Task<List<SelectItem>> GetGroupAllowAnonymous()
        {
            return await _baseService.Filter<Group, SelectItem>(group => group.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetGroupsByCompId(int compId)
        {
            return await _baseService.Filter<Group, SelectItem>(group => group.filterId == compId && group.IsDelete == false).ToListAsync();
        }

        public async Task<List<SelectItem>> GetGroupsByDeptId(int deptId)
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Group, SelectItem>(group => group.filterId == curCompId && group.DeptId == deptId && group.IsDelete == false).ToListAsync();
        }

        //Danh sách thiết bị
        public async Task<List<SelectItem>> GetMachines()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Machine, SelectItem>(machine => machine.IsDelete == false && machine.filterId == curCompId).ToListAsync();
        }

        public async Task<List<SelectItem>> GetMachinesByCompId(int compId)
        {
            return await _baseService.Filter<Machine, SelectItem>(machine => machine.filterId == compId && machine.IsDelete == false).ToListAsync();
        }

        //Danh sách thiết bị theo khu vực
        public async Task<List<SelectItem>> GetMachineByAreaId(string areaId)
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var areaIdArr = areaId.Split(',').Select(Int32.Parse).ToList(); ;
            var lstMachineId = await _repository.Filter<AreaMachine>(am => areaIdArr.Contains(am.AreaId.Value)).Select(x => x.MachineId).ToListAsync();
            return await _baseService.Filter<Machine, SelectItem>(x => lstMachineId.Contains(x.AreaId) && x.IsDelete == false && x.filterId == curCompId).ToListAsync();
        }

        //Danh sách người dùng
        public async Task<List<SelectItem>> GetPersons()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Person, SelectItem>(person => person.IsDelete == false && person.Status == 1 && person.filterId == curCompId && ((person.PersonType.HasValue && person.PersonType != 2) || (!person.PersonType.HasValue))).ToListAsync();
        }

        public async Task<List<SelectItem>> GetPersonsByCompId(int compId)
        {
            return await _baseService.Filter<Person, SelectItem>(person => person.filterId == compId && person.IsDelete == false && person.Status == 1).ToListAsync();
        }

        public async Task<List<SelectItem>> GetPersonsByDeptId(int deptId)
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Person, SelectItem>(person => person.IsDelete == false && person.Status == 1 && person.filterId == curCompId && person.DeptId == deptId).ToListAsync();
        }

        public async Task<List<SelectItem>> GetRoles()
        {
            return await _baseService.Filter<Role, SelectItem>(Role => Role.IsDelete == false && Role.Status == 1).ToListAsync();
        }

        public async Task<List<SelectItem>> GetPrivileges()
        {
            return await _baseService.Filter<Privilege, SelectItem>().ToListAsync();
        }

        public async Task<List<UnitTreeDto>> GetUnitTree()
        {
            var units = await _baseService.All<Unit, UnitTreeDto>().ToListAsync();
            return BuildTree(units);
        }

        public async Task<List<DepartmentTreeDto>> GetDepartmentTree()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var department = await _baseService.Filter<Department, DepartmentTreeDto>(x => x.ComId == curCompId && x.IsDelete == false && x.Type != AppConstants.DepartmentType.SCHOOL && x.Type != AppConstants.DepartmentType.DEFAULT_PARENT_DEPT).ToListAsync();
            return BuildTree1(department, null);
        }

        public async Task<List<DepartmentTreeDto>> GetDepartmentTreeExceptItself(int id)
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var department = await _baseService.Filter<Department, DepartmentTreeDto>(x => x.Id != id && x.ComId == curCompId && x.IsDelete == false && x.Type != AppConstants.DepartmentType.SCHOOL && x.Type != AppConstants.DepartmentType.DEFAULT_PARENT_DEPT).ToListAsync();
            return BuildTree1(department, null);
        }

        public async Task<List<DepartmentTreeDto>> GetCompanyTree()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var company = await _baseService.Filter<Company, DepartmentTreeDto>(x => x.IsDelete == false).ToListAsync();
            return BuildTree1(company, curCompId);
        }

        public async Task<List<DepartmentTreeDto>> GetCompanyTreeView()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var company = await _baseService.Filter<Company, DepartmentTreeDto>(x => x.IsDelete == false).ToListAsync();
            return BuildTree1(company, curCompId, true);
        }

        private List<UnitTreeDto> BuildTree(IEnumerable<UnitTreeDto> allUnits, int? rootUnit = null)
        {
            var tree = new List<UnitTreeDto>();
            List<UnitTreeDto> roots = new List<UnitTreeDto>();
            if (rootUnit == null)
            {
                roots = allUnits.Where(x => x.ParentId == null).ToList();
            }
            else
            {
                var rootNode = allUnits.FirstOrDefault(x => x.Id == rootUnit);
                if (rootNode != null)
                {
                    roots.Add(rootNode);
                }
            }

            foreach (var r in roots)
            {
                tree.Add(r);
                UnitTreeDto nextNode = r;
                UnitTreeDto currentNode;
                UnitTreeDto parentNode;
                int index;
                int level = 1;
                while (nextNode != null)
                {
                    currentNode = nextNode;
                    nextNode = null;
                    foreach (var child in allUnits.Where(x => x.ParentId == currentNode.Id))
                    {
                        child.Parent = currentNode;
                        currentNode.Children.Add(child);
                    }
                    if (currentNode.Children.Any())
                    {
                        nextNode = currentNode.Children[0];
                        level++;
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
                                level--;
                            }
                        }
                    }
                }
            }
            return tree;
        }

        private List<DepartmentTreeDto> BuildTree1(IEnumerable<DepartmentTreeDto> allDepartments, int? rootDepartment = null, bool? showOnlyChild = false)
        {
            var tree = new List<DepartmentTreeDto>();
            List<DepartmentTreeDto> roots = new List<DepartmentTreeDto>();
            if (showOnlyChild == false)
            {
                if (rootDepartment == null)
                {
                    roots = allDepartments.Where(x => x.ParentId == null).ToList();
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
                    roots = allDepartments.Where(x => x.ParentId == null).ToList();
                }
                else
                {
                    roots = allDepartments.Where(x => x.ParentId == rootDepartment).ToList();
                }
            }
            foreach (var r in roots)
            {
                tree.Add(r);
                DepartmentTreeDto nextNode = r;
                DepartmentTreeDto currentNode;
                DepartmentTreeDto parentNode;
                int index;
                int level = 1;
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
                        level++;
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
                                level--;
                            }
                        }
                    }
                }
            }
            return tree;
        }

        public async Task<List<ChartDataItem>> GetDataCheckInOutPerDays(int compId)
        {
            var today = DateTime.Today;
            var _15daysBefore = today.AddDays(-15);
            var _15daysBeforeStr = _15daysBefore.ToString("yyyy-MM-dd");
            var startDateSQL = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            string sqlQuery = " SELECT row_number() OVER (ORDER BY AccessDate desc)  as 'Id', DATEDIFF_BIG(MILLISECOND, '1970-01-01',  e.AccessDate) as 'timeInMilis'," +
                " count(case when e.PersonId = '00000000-0000-0000-0000-000000000000' then 1 else null end) as 'unknown', " +
                " count(case when e.PersonId != '00000000-0000-0000-0000-000000000000' then 1 else null end) as 'employee', " +
                " count(*) as 'total'  " +
                " FROM Events e";
            sqlQuery = sqlQuery + " WHERE e.CompId = " + compId + " AND e.AccessDate >= '" + _15daysBeforeStr + "'";
            sqlQuery = sqlQuery + " GROUP BY e.AccessDate";
            var rs = await _repository.FromSqlRaw<ChartDataItem>(sqlQuery).ToListAsync();
            var todayMs = (today - startDateSQL).TotalMilliseconds;
            var _15daysBeforeMs = (_15daysBefore - startDateSQL).TotalMilliseconds;
            const long day = 86400000;
            var startLoop = _15daysBeforeMs;
            var result = new List<ChartDataItem>();
            while (startLoop <= todayMs)
            {
                ChartDataItem itemEmpty = new ChartDataItem();
                itemEmpty.timeInMilis = (long?)startLoop;
                itemEmpty.total = 0;
                itemEmpty.unknown = 0;
                itemEmpty.employee = 0;

                startLoop = startLoop + day;
                for (int i = 0; i < rs.Count; i++)
                {
                    var item = rs.ElementAt(i);
                    if (item.timeInMilis == startLoop)
                    {
                        itemEmpty = item;
                        break;
                    }
                }
                result.Add(itemEmpty);
            }
            return result;
        }

        public async Task<string> GetCurrentCompName()
        {
            int curUserId = _userIdentity.UserId;
            var user = await _baseService.Filter<User, UserDetailDto>(user => user.Id == curUserId).FirstAsync();
            return user.Company;
        }

        public async Task<List<SelectItem>> GetWorkingShiftTime()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<WorkingShiftTimes, SelectItem>(x => x.filterId == curCompId && x.IsDelete == false).ToListAsync(); ;
        }

        //Danh sách khối
        public async Task<List<SelectItem>> GetGrades()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Grades, SelectItem>(grades => grades.IsDelete == false && grades.CompId == curCompId).ToListAsync();
        }

        public async Task<PagingResult<p_TopLatePerson>> GetTopLate(int options)
        {
            int curUserId = _userIdentity.UserId;
            var user = await _baseService.Filter<User, UserDetailDto>(user => user.Id == curUserId).FirstAsync();
            int compId = user.CompId.HasValue ? user.CompId.Value : 0;

            DateTime dateTo = DateTime.Today;
            DateTime dateFrom = dateTo.AddDays(-1);

            if (options == 1)
            {
                // get start of current week before
                // start of week is 1
                dateFrom = dateTo.StartOfWeek(DayOfWeek.Monday);
            }
            else if (options == 2)
            {
                // start of month
                dateFrom = new DateTime(dateTo.Year, dateTo.Month, 1);
            }
            else if (options == 3)
            {
                // 1st day quarter month before
                int quarterNumber = (dateTo.Month - 1) / 3 + 1;
                dateFrom = new DateTime(dateTo.Year, (quarterNumber - 1) * 3 + 1, 1);
            }
            var dateFromStr = dateFrom.ToString("yyyy-MM-dd");
            var dateToStr = dateTo.ToString("yyyy-MM-dd");
            string sqlQuery = " EXEC [dbo].[GetTopPersonLate] "
                + " @CompId = " + compId + ", "
                + " @TimeFrom = '" + dateFromStr + "', "
                + " @TimeTo = '" + dateToStr + "' ";

            IQueryable<p_TopLatePerson> query = _repository.FromSqlRaw<p_TopLatePerson>(sqlQuery);
            var exData = query.AsEnumerable();
            PagingParams<p_TopLatePerson> pagingModel = new PagingParams<p_TopLatePerson>();
            var result = new PagingResult<p_TopLatePerson>() { PageSize = pagingModel.ItemsPerPage, CurrentPage = pagingModel.Page };

            result.TotalRows = exData.Count();

            if (pagingModel.StartingIndex > 0)
            {
                exData = exData.Skip(pagingModel.StartingIndex);
            }
            if (pagingModel.ItemsPerPage != -1 && pagingModel.ItemsPerPage <= 0)
            {
                pagingModel.ItemsPerPage = 100;
            }
            if (pagingModel.ItemsPerPage > 0)
            {
                exData = exData.Take(pagingModel.ItemsPerPage);
            }
            result.Data = exData.ToList();
            return result;
        }

        public async Task<p_PieChartData> GetDataPieChartByDay(DateTime dateSearch)
        {
            int compId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;

            var dateSearchStr = dateSearch.ToString("yyyy-MM-dd");
            string sqlQuery = " EXEC [dbo].[GetDataPieChart] "
                + " @CompId = " + compId + ", "
                + " @DateSearch = '" + dateSearchStr + "' ";
            IQueryable<p_PieChartData> query = _repository.FromSqlRaw<p_PieChartData>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.FirstOrDefault();
            return result;
        }

        private int convertStringTimeToMinute(string time)
        {
            if (!String.IsNullOrEmpty(time))
            {
                string[] timeArr = time.Split(":");
                if (timeArr.Length == 2)
                {
                    int hourToMinute = 0;
                    string hour = timeArr[0].Trim();
                    if (Int32.TryParse(hour, out hourToMinute))
                    {
                        // you know that the parsing attempt
                        // was successful
                        hourToMinute = hourToMinute * 60;
                    }
                    int minute = 0;
                    string minuteStr = timeArr[1].Trim();
                    if (Int32.TryParse(minuteStr, out minute))
                    {
                        return hourToMinute + minute;
                    }
                }
            }
            return 0;
        }

        //Danh sách lớp
        public async Task<List<SelectItem>> GetClasses()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var com = await _baseService.FindAsync<Company, CompanyDetailDto>(curCompId);
            //check com is school?
            return await _baseService.Filter<Department, SelectItem>(dept => dept.IsDelete == false && dept.filterId == curCompId && dept.DepartmentType == AppConstants.DepartmentType.SCHOOL).ToListAsync();
        }

        public async Task<List<SelectItem>> GetClassesById(string id)
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            var com = await _baseService.FindAsync<Company, CompanyDetailDto>(curCompId);

            return await _baseService.Filter<Department, SelectItem>(dept => dept.IsDelete == false && dept.filterId == curCompId && dept.DepartmentType == AppConstants.DepartmentType.SCHOOL && id.Contains(dept.gradesId.ToString() + ',')).OrderBy(x => x.gradesId).ToListAsync();
        }

        public async Task<int> GetDefaultParentDept()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            //check com is school?
            var defaultDept = await _baseService.Filter<Department, DepartmentDetailDto>(dept => dept.IsDelete == false && dept.ComId == curCompId && dept.Type == AppConstants.DepartmentType.DEFAULT_PARENT_DEPT).ToListAsync();

            if (defaultDept != null && defaultDept.Count > 0) return defaultDept[0].Id;
            return 0;
        }

        public async Task<int> GetDefaultParentGroup()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            //check com is school?
            var defaultGroup = await _baseService.Filter<Group, GroupDetailDto>(group => group.IsDelete == false && group.CompId == curCompId && group.DepartmentType == AppConstants.DepartmentType.DEFAULT_PARENT_DEPT).ToListAsync();
            if (defaultGroup != null && defaultGroup.Count > 0) return defaultGroup[0].GroupId;
            return 0;
        }

        public async Task<List<SelectItem>> GetGuest()
        {
            int curCompId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            return await _baseService.Filter<Person, SelectItem>(x => x.IsDelete == false && x.CompId == curCompId && x.PersonType != 1).ToListAsync();
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
