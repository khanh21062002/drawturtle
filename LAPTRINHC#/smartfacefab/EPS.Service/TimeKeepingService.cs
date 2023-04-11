using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Company;
using EPS.Service.Dtos.Department;
using EPS.Service.Dtos.Group;
using EPS.Service.Dtos.OverTimeHours;
using EPS.Service.Dtos.RegisterWorkingShift;
using EPS.Service.Dtos.TimeKeeping;
using EPS.Service.Dtos.WorkingHours;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class TimeKeepingService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public TimeKeepingService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        //list
        public async Task<PagingResult<WorkingHoursGridDto>> GetTimeKeepings(WorkingHoursGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<WorkingHours, WorkingHoursGridDto>(pagingModel);
        }

        //create
        public async Task<int> CreateTimeKeeping(WorkingHoursCreateDto categoryCreate, bool isExploiting = false)
        {
            var count1 = await _repository.CountAsync<WorkingHours>(x => x.PersonId == categoryCreate.PersonId && x.WorkingShiftId == categoryCreate.WorkingShiftId && x.Day == categoryCreate.Day);
            categoryCreate.TypeDay = 5;
            await _baseService.CreateAsync<WorkingHours, WorkingHoursCreateDto>(categoryCreate);
            if (count1 > 0)
            {
                return count1;
            }
            return 0;
        }

        //detail
        public async Task<WorkingHoursDetailsDto> GetTimeKeepingById(int id)
        {
            return await _baseService.FindAsync<WorkingHours, WorkingHoursDetailsDto>(x => x.ID == id);
        }

        //update
        public async Task<int> UpdateTimeKeeping(int id, WorkingHoursUpdateDto editedTimeKeeping)
        {
            var count1 = await _repository.CountAsync<WorkingHours>(x => x.PersonId == editedTimeKeeping.PersonId && x.ID != id && x.WorkingShiftId == editedTimeKeeping.WorkingShiftId && x.Day == editedTimeKeeping.Day);
            if (count1 > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_EMPLOYEE2;
                throw new EPSException(errors1);
            }
            return await _baseService.UpdateAsync<WorkingHours, WorkingHoursUpdateDto>(id, editedTimeKeeping);
        }

        //delete
        public async Task<int> DeleteTimeKeeping(int id)
        {
            return await _baseService.DeleteAsync<WorkingHours, int>(id);
        }

        public async Task<int> CheckExistWorkingHours(Guid personId, int workingShiftTimeId, DateTime day)
        {
            var count1 = await _repository.CountAsync<WorkingHours>(x => x.PersonId == personId && x.WorkingShiftId == workingShiftTimeId && x.Day == day);
            return count1;
        }

        public async Task<List<Guid>> GetListPersonIdByGroupId(int groupId)
        {
            var rs = await _repository.Filter<PersonGroup>(x => x.GroupId == groupId).Select(x => x.PersonId).ToListAsync();
            return rs;
        }

        public async Task<GroupDetailDto> GetGroupById(int groupId)
        {
            return await _baseService.FindAsync<Group, GroupDetailDto>(groupId);
        }

        public async Task<List<GroupDetailDto>> GetListGroupByPersonId(Guid personId)
        {
            var listPersonGroup = await _repository.Filter<PersonGroup>(i => i.PersonId == personId && i.IsDelete == false).Select(i => i.GroupId).ToListAsync();
            var rs = await _baseService.Filter<Group, GroupDetailDto>(x => listPersonGroup.Contains(x.GroupId) && x.IsDelete == false).ToListAsync();
            return rs;
        }

        //detail
        public async Task<string> GetHeaderBySearchForm(WorkingHoursGridPagingDto searchForm)
        {
            if (searchForm.Lang == "vi")
            {
                string line1 = "DANH SÁCH LỊCH SỬ CHẤM CÔNG";
                string line2 = "Đơn vị: ";
                var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(searchForm.CompId);
                if (curComp != null && curComp.Name != null)
                {
                    line2 += curComp.Name;
                }
                string line3 = "Phòng ban: ";
                if (string.IsNullOrEmpty(searchForm.DeptId))
                {
                    line3 += "Tất cả";
                }
                else
                {
                    List<int> lstperId = searchForm.DeptId.Split(',').Select(Int32.Parse).ToList();
                    var lstDepart = _baseService.Filter<Department, DepartmentDetailDto>(i => lstperId.Contains(i.Id)).ToList();

                    int index = 0;

                    foreach (var item in lstDepart)
                    {
                        if (index == lstDepart.Count() - 1)
                        {
                            line3 += item.Name;
                        }
                        else
                        {
                            line3 += item.Name + ", ";
                        }
                        index++;
                    }
                }
                string line4 = "Nhóm truy cập: ";
                if (!searchForm.GroupId.HasValue)
                {
                    line4 += "Tất cả";
                }
                else
                {
                    var group = await _baseService.FindAsync<Group, GroupDetailDto>(searchForm.GroupId);
                    if (group != null && group.GroupName != null)
                    {
                        line4 += group.GroupName;
                    }
                }
                string line5 = "Thời gian: ";
                if (searchForm.FilterDateFrom != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateFrom, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy") + " - ";
                    }
                    else { }
                }
                if (searchForm.FilterDateTo != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateTo, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy");
                    }
                    else { }
                }
                string rs = line1 + " \r\n " + line2 + " \r\n " + line3 + "\r\n" + line4 + "\r\n" + line5 + "\r\n";
                return rs;
            }
            else
            {
                string line1 = "Timekeeping history list";
                string line2 = "Company: ";
                var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(searchForm.CompId);
                if (curComp != null && curComp.Name != null)
                {
                    line2 += curComp.Name;
                }
                string line3 = "Department: ";
                if (string.IsNullOrEmpty(searchForm.DeptId))
                {
                    line3 += "All";
                }
                else
                {
                    List<int> lstperId = searchForm.DeptId.Split(',').Select(Int32.Parse).ToList();
                    var lstDepart = _baseService.Filter<Department, DepartmentDetailDto>(i => lstperId.Contains(i.Id)).ToList();

                    int index = 0;

                    foreach (var item in lstDepart)
                    {
                        if (index == lstDepart.Count() - 1)
                        {
                            line3 += item.Name;
                        }
                        else
                        {
                            line3 += item.Name + ", ";
                        }
                        index++;
                    }
                }
                string line4 = "Access Group: ";
                if (!searchForm.GroupId.HasValue)
                {
                    line4 += "All";
                }
                else
                {
                    var group = await _baseService.FindAsync<Group, GroupDetailDto>(searchForm.GroupId);
                    if (group != null && group.GroupName != null)
                    {
                        line4 += group.GroupName;
                    }
                }
                string line5 = "Time: ";
                if (searchForm.FilterDateFrom != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateFrom, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy") + " - ";
                    }
                    else { }
                }
                if (searchForm.FilterDateTo != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateTo, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy");
                    }
                    else { }
                }
                string rs = line1 + " \r\n " + line2 + " \r\n " + line3 + "\r\n" + line4 + "\r\n" + line5 + "\r\n";
                return rs;
            }
        }

        //detail
        public async Task<string> GetHeaderOTBySearchForm(OverTimeHoursGridPagingDto searchForm)
        {
            if (searchForm.Lang == "vi")
            {
                string line1 = "DANH SÁCH LỊCH SỬ LÀM THÊM NGOÀI GIỜ";
                string line2 = "Đơn vị: ";
                var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(searchForm.CompId);
                if (curComp != null && curComp.Name != null)
                {
                    line2 += curComp.Name;
                }
                string line3 = "Phòng ban: ";
                if (string.IsNullOrEmpty(searchForm.DeptId))
                {
                    line3 += "Tất cả";
                }
                else
                {
                    List<int> lstperId = searchForm.DeptId.Split(',').Select(Int32.Parse).ToList();
                    var lstDepart = _baseService.Filter<Department, DepartmentDetailDto>(i => lstperId.Contains(i.Id)).ToList();

                    int index = 0;

                    foreach (var item in lstDepart)
                    {
                        if (index == lstDepart.Count() - 1)
                        {
                            line3 += item.Name;
                        }
                        else
                        {
                            line3 += item.Name + ", ";
                        }
                        index++;
                    }
                }
                string line4 = "Nhóm truy cập: ";
                if (!searchForm.GroupId.HasValue)
                {
                    line4 += "Tất cả";
                }
                else
                {
                    var group = await _baseService.FindAsync<Group, GroupDetailDto>(searchForm.GroupId);
                    if (group != null && group.GroupName != null)
                    {
                        line4 += group.GroupName;
                    }
                }
                string line5 = "Thời gian: ";
                if (searchForm.FilterDateFrom != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateFrom, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy") + " - ";
                    }
                    else { }
                }
                if (searchForm.FilterDateTo != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateTo, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy");
                    }
                    else { }
                }
                string rs = line1 + " \r\n " + line2 + " \r\n " + line3 + "\r\n" + line4 + "\r\n" + line5 + "\r\n";
                return rs;
            }
            else
            {
                string line1 = "List of overtime history";
                string line2 = "Company: ";
                var curComp = await _baseService.FindAsync<Company, CompanyDetailDto>(searchForm.CompId);
                if (curComp != null && curComp.Name != null)
                {
                    line2 += curComp.Name;
                }
                string line3 = "Department: ";
                //if (searchForm.DeptId.Count() == 0)
                if (string.IsNullOrEmpty(searchForm.DeptId))

                {
                    line3 += "All";
                }
                else
                {
                    List<int> lstperId = searchForm.DeptId.Split(',').Select(Int32.Parse).ToList();
                    var lstDepart = _baseService.Filter<Department, DepartmentDetailDto>(i => lstperId.Contains(i.Id)).ToList();

                    int index = 0;

                    foreach (var item in lstDepart)
                    {
                        if (index == lstDepart.Count() - 1)
                        {
                            line3 += item.Name;
                        }
                        else
                        {
                            line3 += item.Name + ", ";
                        }
                        index++;
                    }
                }
                string line4 = "Access Group: ";
                if (!searchForm.GroupId.HasValue)
                {
                    line4 += "All";
                }
                else
                {
                    var group = await _baseService.FindAsync<Group, GroupDetailDto>(searchForm.GroupId);
                    if (group != null && group.GroupName != null)
                    {
                        line4 += group.GroupName;
                    }
                }
                string line5 = "Time: ";
                if (searchForm.FilterDateFrom != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateFrom, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy") + " - ";
                    }
                    else { }
                }
                if (searchForm.FilterDateTo != null)
                {
                    DateTime myDate;
                    if (DateTime.TryParse(searchForm.FilterDateTo, out myDate))
                    {
                        // handle parse failure
                        line5 += myDate.ToString("dd/MM/yyyy");
                    }
                    else { }
                }
                string rs = line1 + " \r\n " + line2 + " \r\n " + line3 + "\r\n" + line4 + "\r\n" + line5 + "\r\n";
                return rs;
            }
        }
    }
}
