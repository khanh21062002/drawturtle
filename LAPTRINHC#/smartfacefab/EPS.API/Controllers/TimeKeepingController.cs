using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.OverTimeHours;
using EPS.Service.Dtos.RegisterWorkingShift;
using EPS.Service.Dtos.TimeKeeping;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.WorkingHours;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/timesheet/timekeeping")]
    [Authorize]
    public class TimeKeepingController : BaseController
    {
        private TimeKeepingService _timeKeepingSevice;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings;
        private IUserIdentity<int> _userIdentity;
        private DepartmentService _departmentService;
        private CompanyService _companyService;
        private WorkingShiftTimeService _workingShiftTimeService;
        private PersonService _personService;
        private CheckDataService _checkDataService;
        private OverTimeHoursService _overTimeHoursService;

        public TimeKeepingController(TimeKeepingService timeKeepingSevice, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings, IUserIdentity<int> userIdentity, DepartmentService departmentService, CompanyService CompanyService, WorkingShiftTimeService workingShiftTimeService, PersonService personService, CheckDataService checkDataService, OverTimeHoursService overTimeHoursService)
        {
            _timeKeepingSevice = timeKeepingSevice;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value;
            _userIdentity = userIdentity;
            _departmentService = departmentService;
            _companyService = CompanyService;
            _workingShiftTimeService = workingShiftTimeService;
            _personService = personService;
            _checkDataService = checkDataService;
            _overTimeHoursService = overTimeHoursService;
        }

        //list
        [CustomAuthorize(PrivilegeList.ViewTimeKeeping, PrivilegeList.ManageTimeKeeping)]
        [HttpGet]
        public async Task<IActionResult> GetListTimeKeepings([FromQuery] WorkingHoursGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            pagingModel.SortDesc = false;
            pagingModel.SortBy = " PersonId,  Day ";
            int? groupId = pagingModel.GroupId;
            if (groupId.HasValue)
            {
                //get all list person id in this personid
                List<Guid> listPersonId = await _timeKeepingSevice.GetListPersonIdByGroupId(groupId.Value);
                pagingModel.ListPersonId = listPersonId;
            }
            var rs = await _timeKeepingSevice.GetTimeKeepings(pagingModel);
            if (pagingModel.IsExportRequest.HasValue && pagingModel.IsExportRequest.Value == 1)
            {
                return Ok(rs);
            }
            foreach (var item in rs.Data)
            {
                if (item.PersonId.HasValue)
                {
                    var lstGroup = await _timeKeepingSevice.GetListGroupByPersonId(item.PersonId.Value);
                    item.GroupName = "";
                    int index = 0;
                    foreach (var group in lstGroup)
                    {
                        if (!string.IsNullOrEmpty(group.GroupName))
                        {
                            if (index == 0)
                            {
                                item.GroupName = group.GroupName;
                            }
                            else
                            {
                                item.GroupName = item.GroupName + "; " + group.GroupName;
                            }
                            index++;
                        }
                    }
                }
            }
            return Ok(rs);
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageTimeKeeping)]
        [HttpPost]
        public async Task<IActionResult> Create(WorkingHoursCreateDto timeKeepingCreateDto)
        {
            if (timeKeepingCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(timeKeepingCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            return Ok(await _timeKeepingSevice.CreateTimeKeeping(timeKeepingCreateDto));
        }

        //import chấm công
        [CustomAuthorize(PrivilegeList.ManageTimeKeeping)]
        [HttpPost("import")]
        public async Task<IActionResult> Import(WorkingHoursImportDto file)
        {
            if (file.Lang == "vi")
            {
                var fileDownloadName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ImportChamCong.xlsx";
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                var rootPath = System.IO.Directory.GetParent(contentRootPath);
                string uploadFolder = _appSettings.DownloadFolder + _userIdentity.CompId + "\\";
                string path = Path.Combine(rootPath.FullName, uploadFolder);
                ImportDto imp = new ImportDto();
                var countSuccess = 0;
                if (file.FileData != null)
                {
                    byte[] byteArray = Convert.FromBase64String(file.FileData);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //Save file upload
                    System.IO.File.WriteAllBytes(path + fileDownloadName, byteArray);
                    //Get All company
                    var lstCompany = _companyService.GetAllCompany();
                    //Get All nhân sự
                    var lstPerson = _personService.GetAllPerson();
                    //Get All phòng ban
                    var lstDepart = _departmentService.GetAllDepartment();
                    //Get All WorkingShift
                    var lstWorkingShift = _workingShiftTimeService.GetAllWorkingShiftTime();
                    var fi = new FileInfo(path + fileDownloadName);
                    using (var package = new ExcelPackage(fi))
                    {
                        var worksheet = package.Workbook.Worksheets[1];
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 3; row <= rowCount; row++)
                        {
                            try
                            {
                                WorkingHoursCreateDto timekeep = new WorkingHoursCreateDto();
                                //add company
                                timekeep.CompId = _userIdentity.CompId;
                                //add mã nhân viên
                                var personCode = worksheet.Cells[row, 2].Value;
                                Guid personId;
                                if (personCode != null && !string.IsNullOrEmpty(personCode.ToString().Trim()))
                                {
                                    var person = lstPerson.Find(x => x.PersonCode == personCode.ToString().Trim() && x.CompId == _userIdentity.CompId && x.IsDelete == false);
                                    if (person != null)
                                    {
                                        personId = person.PersonId;
                                        timekeep.PersonId = personId;
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Không tìm thấy mã nhân viên";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Employee code not found";
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Thiếu mã nhân viên";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Employee code is null ";
                                        continue;
                                    }
                                }
                                try
                                {
                                    var birthday = worksheet.Cells[row, 3].Value;
                                    DateTime d;
                                    bool chValidity = DateTime.TryParseExact(
                                                               birthday.ToString(),
                                                               "dd/MM/yyyy",
                                                               CultureInfo.InvariantCulture,
                                                               DateTimeStyles.None,
                                                               out d);
                                    if (chValidity == true && birthday != null && !string.IsNullOrEmpty(birthday.ToString().Trim()))
                                    {
                                        string strBirthday = birthday.ToString().Trim();
                                        timekeep.Day = DateTime.ParseExact(strBirthday, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Sai định dạng ngày";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Incorrect date format";
                                            continue;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Sai định dạng ngày ";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Incorrect date format";
                                        continue;
                                    }
                                }
                                //add số công
                                try
                                {
                                    var totalperday = worksheet.Cells[row, 4].Value;
                                    float total;
                                    if (totalperday != null && !string.IsNullOrEmpty(totalperday.ToString().Trim()))
                                    {
                                        string strTotalPerDay = totalperday.ToString().Trim().ToLower();
                                        total = float.Parse(strTotalPerDay);
                                        timekeep.Total = total;
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Thiếu số công";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Workday is null";
                                            continue;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    //Lỗi
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Sai định dạng số công";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Incorrect workday format";
                                        continue;
                                    }
                                }
                                //add ca làm việc
                                var workingShiftName = worksheet.Cells[row, 5].Value;
                                int workingShiftId;
                                if (workingShiftName != null && !string.IsNullOrEmpty(workingShiftName.ToString().Trim()))
                                {
                                    var workingShift = lstWorkingShift.Find(x => x.Name == workingShiftName.ToString().Trim());
                                    if (workingShift != null)
                                    {
                                        workingShiftId = workingShift.ID;
                                        timekeep.WorkingShiftId = workingShiftId;
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Không tìm thấy ca làm việc";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Work shift not found";
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Thiếu ca làm việc";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Work shift is null";
                                        continue;
                                    }
                                }
                                //add ghi chú
                                var note = worksheet.Cells[row, 6].Value;
                                if (note != null && !string.IsNullOrEmpty(note.ToString().Trim()))
                                {
                                    timekeep.NoteDay = note.ToString().Trim();
                                }
                                //check trùng ca làm việc
                                if (personId != null && workingShiftId != null && timekeep.Day.HasValue)
                                {
                                    int numberExist = await _timeKeepingSevice.CheckExistWorkingHours(personId, workingShiftId, timekeep.Day.Value);
                                    if (numberExist > 0)
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Thành công,nhân viên đã có dữ liệu chấm công trên hệ thống";
                                            countSuccess = countSuccess + 1;
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Successfully";
                                            countSuccess = countSuccess + 1;
                                            continue;
                                        }
                                    }
                                }
                                await _timeKeepingSevice.CreateTimeKeeping(timekeep);
                                countSuccess = countSuccess + 1;
                                if (file.Lang == "vi")
                                {
                                    worksheet.Cells[row, 9].Value = "Thành công";
                                    continue;
                                }
                                else
                                {
                                    worksheet.Cells[row, 9].Value = "Success";
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                //Lỗi
                                worksheet.Cells[row, 9].Value = ex.Message;
                                continue;
                            }
                        }
                        imp.COUNT = countSuccess;
                        imp.URL = "download\\" + _userIdentity.CompId + "\\" + fileDownloadName;
                        imp.SUMCOUNT = rowCount - 2;
                        package.Save();
                        System.Threading.Thread.Sleep(1000); //Dừng tí chờ save
                    }
                }
                return Ok(imp);
            }
            else
            {
                var fileDownloadName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ImportTimeKepping.xlsx";
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                var rootPath = System.IO.Directory.GetParent(contentRootPath);
                string uploadFolder = _appSettings.DownloadFolder + _userIdentity.CompId + "\\";
                string path = Path.Combine(rootPath.FullName, uploadFolder);
                ImportDto imp = new ImportDto();
                var countSuccess = 0;
                if (file.FileData != null)
                {
                    byte[] byteArray = Convert.FromBase64String(file.FileData);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //Save file upload
                    System.IO.File.WriteAllBytes(path + fileDownloadName, byteArray);
                    //Get All company
                    var lstCompany = _companyService.GetAllCompany();
                    //Get All nhân sự
                    var lstPerson = _personService.GetAllPerson();
                    //Get All phòng ban
                    var lstDepart = _departmentService.GetAllDepartment();
                    //Get All WorkingShift
                    var lstWorkingShift = _workingShiftTimeService.GetAllWorkingShiftTime();
                    var fi = new FileInfo(path + fileDownloadName);
                    using (var package = new ExcelPackage(fi))
                    {
                        var worksheet = package.Workbook.Worksheets[1];
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 3; row <= rowCount; row++)
                        {
                            try
                            {
                                WorkingHoursCreateDto timekeep = new WorkingHoursCreateDto();
                                //add company
                                timekeep.CompId = _userIdentity.CompId;
                                //add mã nhân viên
                                var personCode = worksheet.Cells[row, 2].Value;
                                Guid personId;
                                if (personCode != null && !string.IsNullOrEmpty(personCode.ToString().Trim()))
                                {
                                    var person = lstPerson.Find(x => x.PersonCode == personCode.ToString().Trim() && x.CompId == _userIdentity.CompId && x.IsDelete == false);
                                    if (person != null)
                                    {
                                        personId = person.PersonId;
                                        timekeep.PersonId = personId;
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Không tìm thấy mã nhân viên";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Employee code not found";
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Thiếu mã nhân viên";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Employee code is null ";
                                        continue;
                                    }
                                }
                                try
                                {
                                    var birthday = worksheet.Cells[row, 3].Value;
                                    DateTime d;
                                    bool chValidity = DateTime.TryParseExact(
                                                               birthday.ToString(),
                                                               "dd/MM/yyyy",
                                                               CultureInfo.InvariantCulture,
                                                               DateTimeStyles.None,
                                                               out d);
                                    if (chValidity == true && birthday != null && !string.IsNullOrEmpty(birthday.ToString().Trim()))
                                    {
                                        string strBirthday = birthday.ToString().Trim();
                                        timekeep.Day = DateTime.ParseExact(strBirthday, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Sai định dạng ngày";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Incorrect date format";
                                            continue;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Sai định dạng ngày ";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Incorrect date format";
                                        continue;
                                    }
                                }
                                //add số công
                                try
                                {
                                    var totalperday = worksheet.Cells[row, 4].Value;
                                    float total;
                                    if (totalperday != null && !string.IsNullOrEmpty(totalperday.ToString().Trim()))
                                    {
                                        string strTotalPerDay = totalperday.ToString().Trim().ToLower();
                                        total = float.Parse(strTotalPerDay);
                                        timekeep.Total = total;
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Thiếu số công";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Workday is null";
                                            continue;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    //Lỗi
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Sai định dạng số công";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Incorrect workday format";
                                        continue;
                                    }
                                }
                                //add ca làm việc
                                var workingShiftName = worksheet.Cells[row, 5].Value;
                                int workingShiftId;
                                if (workingShiftName != null && !string.IsNullOrEmpty(workingShiftName.ToString().Trim()))
                                {
                                    var workingShift = lstWorkingShift.Find(x => x.Name == workingShiftName.ToString().Trim());
                                    if (workingShift != null)
                                    {
                                        workingShiftId = workingShift.ID;
                                        timekeep.WorkingShiftId = workingShiftId;
                                    }
                                    else
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Không tìm thấy ca làm việc";
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Work shift not found";
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 9].Value = "Thiếu ca làm việc";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 9].Value = "Work shift is null";
                                        continue;
                                    }
                                }
                                //add ghi chú
                                var note = worksheet.Cells[row, 6].Value;
                                if (note != null && !string.IsNullOrEmpty(note.ToString().Trim()))
                                {
                                    timekeep.NoteDay = note.ToString().Trim();
                                }
                                await _timeKeepingSevice.CreateTimeKeeping(timekeep);
                                //check trùng ca làm việc
                                if (personId != null && workingShiftId != null && timekeep.Day.HasValue)
                                {
                                    int numberExist = await _timeKeepingSevice.CheckExistWorkingHours(personId, workingShiftId, timekeep.Day.Value);
                                    if (numberExist > 0)
                                    {
                                        if (file.Lang == "vi")
                                        {
                                            worksheet.Cells[row, 9].Value = "Thành công,nhân viên đã có dữ liệu chấm công trên hệ thống";
                                            countSuccess = countSuccess + 1;
                                            continue;
                                        }
                                        else
                                        {
                                            worksheet.Cells[row, 9].Value = "Successfully";
                                            countSuccess = countSuccess + 1;
                                            continue;
                                        }
                                    }
                                }
                                countSuccess = countSuccess + 1;
                                if (file.Lang == "vi")
                                {
                                    worksheet.Cells[row, 9].Value = "Thành công";
                                    continue;
                                }
                                else
                                {
                                    worksheet.Cells[row, 9].Value = "Success";
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                //Lỗi
                                worksheet.Cells[row, 9].Value = ex.Message;
                                continue;
                            }
                        }
                        imp.COUNT = countSuccess;
                        imp.URL = "download\\" + _userIdentity.CompId + "\\" + fileDownloadName;
                        imp.SUMCOUNT = rowCount - 2;
                        package.Save();
                        System.Threading.Thread.Sleep(1000); //Dừng tí chờ save
                    }
                }
                return Ok(imp);
            }
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewTimeKeeping, PrivilegeList.ManageTimeKeeping)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeKeepingById(int id)
        {
            await _checkDataService.CheckTimeKeeping(id, (int)_userIdentity.CompId);
            return Ok(await _timeKeepingSevice.GetTimeKeepingById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageTimeKeeping)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkingHoursUpdateDto timeKeepingUpdateDto)
        {
            await _checkDataService.CheckTimeKeeping(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckTimeKeeping(id, (int)timeKeepingUpdateDto.CompId);
            return Ok(await _timeKeepingSevice.UpdateTimeKeeping(id, timeKeepingUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageTimeKeeping)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckTimeKeeping(id, (int)_userIdentity.CompId);
            return Ok(await _timeKeepingSevice.DeleteTimeKeeping(id));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewTimeKeeping, PrivilegeList.ManageTimeKeeping)]
        [HttpGet("exportExcel")]
        public async Task<IActionResult> GetTimeKeepingById([FromQuery] WorkingHoursGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            pagingModel.SortDesc = false;
            pagingModel.SortBy = " PersonId,  Day ";
            int? groupId = pagingModel.GroupId;
            if (groupId.HasValue)
            {
                //get all list person id in this personid
                List<Guid> listPersonId = await _timeKeepingSevice.GetListPersonIdByGroupId(groupId.Value);
                pagingModel.ListPersonId = listPersonId;
            }
            var rs = await _timeKeepingSevice.GetTimeKeepings(pagingModel);
            var fileDownloadName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_QuanLyChamCong.xlsx";
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var rootPath = System.IO.Directory.GetParent(contentRootPath);
            string uploadFolder = _appSettings.DownloadFolder + "ChamCong\\" + _userIdentity.CompId + "\\";
            string path = Path.Combine(rootPath.FullName, uploadFolder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string modelFolder;
            //get the path of model file
            if (pagingModel.Lang == "vi")
            {
                modelFolder = _appSettings.DownloadFolder + "TemplateFiles\\MauQuanLyChamCong.xlsx";
            }
            else
            {
                modelFolder = _appSettings.DownloadFolder + "TemplateFiles\\SampleManagementTimeKeeping.xlsx";
            }
            string modelPath = Path.Combine(rootPath.FullName, modelFolder);
            byte[] byteArray = System.IO.File.ReadAllBytes(modelPath);
            //clone model file to dowdload file
            //Save file upload
            System.IO.File.WriteAllBytes(path + fileDownloadName, byteArray);
            var fi = new FileInfo(path + fileDownloadName);
            using (var package = new ExcelPackage(fi))
            {
                var worksheet = package.Workbook.Worksheets[1];
                //header
                string header = await _timeKeepingSevice.GetHeaderBySearchForm(pagingModel);
                worksheet.Cells[1, 1].Value = header;
                worksheet.Cells[1, 1, 3, 13].Merge = true;
                worksheet.Cells[1, 1, 3, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, 3, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, 1, 3, 13].Style.WrapText = true;
                //body
                const int START_ROW = 7;
                int rowIndex = START_ROW;
                int startRowPerItem = START_ROW;
                int indexPerson = 1;
                for (int i = 0; i < rs.Data.Count; i++)
                {
                    var item = rs.Data[i];
                    if (item.PersonId.HasValue)
                    {
                        var lstGroup = await _timeKeepingSevice.GetListGroupByPersonId(item.PersonId.Value);
                        item.GroupName = "";
                        int index = 0;
                        foreach (var group in lstGroup)
                        {
                            if (!string.IsNullOrEmpty(group.GroupName))
                            {
                                if (index == 0)
                                {
                                    item.GroupName = group.GroupName;
                                }
                                else
                                {
                                    item.GroupName = item.GroupName + "; " + group.GroupName;
                                }
                                index++;
                            }
                        }
                    }
                    //export excel
                    try
                    {
                        //column 1 is INDEX;
                        worksheet.Cells[rowIndex, 1].Value = indexPerson;
                        worksheet.Cells[rowIndex, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 2 is DEPARTMENT_NAME
                        worksheet.Cells[rowIndex, 2].Value = item.DeptName;
                        //column 3 is GROUP_NAME
                        worksheet.Cells[rowIndex, 3].Value = item.GroupName;
                        //column 4 is PERSON_CODE
                        worksheet.Cells[rowIndex, 4].Value = item.PersonCode;
                        //column 5 is FULL_NAME
                        worksheet.Cells[rowIndex, 5].Value = item.PersonName;
                        worksheet.Cells[rowIndex, 6].Value = item.DayFormat;
                        worksheet.Cells[rowIndex, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[rowIndex, 6].Value = item.DayFormat;
                        if (item.TypeDay == 1 || item.TypeDay == 2)
                        {
                            // day la ngay le hoac ngay nghi phep
                            //column 6 is DAY_FORMAT
                            Color colorHoliday = System.Drawing.ColorTranslator.FromHtml("#2FD14A");
                            worksheet.Cells[rowIndex, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[rowIndex, 6].Style.Fill.BackgroundColor.SetColor(colorHoliday);
                        }
                        else
                        if (item.DayOfWeek == 6 || item.DayOfWeek == 0)
                        {
                            //column 6 is DAY_FORMAT
                            Color colorWeekend = System.Drawing.ColorTranslator.FromHtml("#D5E4F7");
                            worksheet.Cells[rowIndex, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[rowIndex, 6].Style.Fill.BackgroundColor.SetColor(colorWeekend);
                        }
                        //column 7 is WORK_SHIFT
                        worksheet.Cells[rowIndex, 7].Value = item.WorkShiftName;
                        worksheet.Cells[rowIndex, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 8 is CHECK_IN
                        worksheet.Cells[rowIndex, 8].Value = item.MinCheckInFormat;
                        worksheet.Cells[rowIndex, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 9 is LATE
                        worksheet.Cells[rowIndex, 9].Value = item.LateStr;
                        worksheet.Cells[rowIndex, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 10 is CHECK_OUT
                        worksheet.Cells[rowIndex, 10].Value = item.MaxCheckOutFormat;
                        worksheet.Cells[rowIndex, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 11 is EARLY
                        worksheet.Cells[rowIndex, 11].Value = item.EarlyStr;
                        worksheet.Cells[rowIndex, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 12 is TOTAL_SHIFT
                        worksheet.Cells[rowIndex, 12].Value = item.TotalFormat;
                        worksheet.Cells[rowIndex, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    catch (Exception ex)
                    {
                        //Lỗi
                        worksheet.Cells[rowIndex, 13].Value = "Errors Occur! - " + ex.Message;
                    }
                    rowIndex++;
                    int currentRowIndex = rowIndex - 1;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#efeadf");
                    //check if this is the end of that person information
                    if (i < rs.Data.Count - 1)
                    {
                        var afterItem = rs.Data[i + 1];
                        if (!string.IsNullOrEmpty(item.PersonCode) && !string.IsNullOrEmpty(afterItem.PersonCode))
                        {
                            string curPerCode = item.PersonCode.Trim();
                            string afterPerCode = afterItem.PersonCode.Trim();
                            if (curPerCode != afterPerCode)
                            {
                                //merge old person 
                                indexPerson++;
                                for (int j = 1; j < 6; j++)
                                {
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Merge = true;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                }
                                //insert a black space to end of this person (after this row)
                                worksheet.Cells[rowIndex, 1, rowIndex, 13].Merge = true;
                                //next person
                                rowIndex++;
                                startRowPerItem = rowIndex;
                            }
                        }
                    }
                }
                int lastRow = rowIndex;
                Color colFromHex2 = System.Drawing.ColorTranslator.FromHtml("#efeadf");
                // merge
                for (int j = 1; j < 6; j++)
                {
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Merge = true;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.Fill.BackgroundColor.SetColor(colFromHex2);
                }
                //border all 
                var modelTable = worksheet.Cells[START_ROW, 1, lastRow, 13];
                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //modelTable.AutoFitColumns();
                package.Save();
                System.Threading.Thread.Sleep(1000); //Dừng tí chờ save
            }
            return Ok("download\\ChamCong\\" + _userIdentity.CompId + "\\" + fileDownloadName);
        }

        //--export OT--//
        //detail
        [CustomAuthorize(PrivilegeList.ViewOverTimeHours, PrivilegeList.ManageOverTimeHours)]
        [HttpGet("exportExcelForOT")]
        public async Task<IActionResult> ExportExcelOT([FromQuery] OverTimeHoursGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            pagingModel.SortDesc = false;
            pagingModel.SortBy = " PersonId,  Day ";
            int? groupId = pagingModel.GroupId;
            if (groupId.HasValue)
            {
                //get all list person id in this personid
                List<Guid> listPersonId = await _timeKeepingSevice.GetListPersonIdByGroupId(groupId.Value);
                pagingModel.ListPersonId = listPersonId;
            }
            var rs = await _overTimeHoursService.GetAll(pagingModel);
            var fileDownloadName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_QuanLyOT.xlsx";
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var rootPath = System.IO.Directory.GetParent(contentRootPath);
            string uploadFolder = _appSettings.DownloadFolder + "ChamCong\\" + _userIdentity.CompId + "\\";
            string path = Path.Combine(rootPath.FullName, uploadFolder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string modelFolder;
            if (pagingModel.Lang == "vi")
            {
                modelFolder = _appSettings.DownloadFolder + "TemplateFiles\\MauOT.xlsx";
            }
            else
            {
                modelFolder = _appSettings.DownloadFolder + "TemplateFiles\\SampleOT.xlsx";
            }
            //get the path of model file
            //string modelFolder = _appSettings.DownloadFolder + "TemplateFiles\\MauOT.xlsx";
            string modelPath = Path.Combine(rootPath.FullName, modelFolder);
            byte[] byteArray = System.IO.File.ReadAllBytes(modelPath);
            //clone model file to dowdload file
            //Save file upload
            System.IO.File.WriteAllBytes(path + fileDownloadName, byteArray);
            var fi = new FileInfo(path + fileDownloadName);
            using (var package = new ExcelPackage(fi))
            {
                var worksheet = package.Workbook.Worksheets[1];
                //header
                string header = await _timeKeepingSevice.GetHeaderOTBySearchForm(pagingModel);
                worksheet.Cells[1, 1].Value = header;
                worksheet.Cells[1, 1, 3, 14].Merge = true;
                worksheet.Cells[1, 1, 3, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, 3, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, 1, 3, 14].Style.WrapText = true;
                //body
                const int START_ROW = 7;
                int rowIndex = START_ROW;
                int startRowPerItem = START_ROW;
                for (int i = 0; i < rs.Data.Count; i++)
                {
                    var item = rs.Data[i];
                    if (item.PersonId.HasValue)
                    {
                        var lstGroup = await _timeKeepingSevice.GetListGroupByPersonId(item.PersonId.Value);
                        item.GroupName = "";
                        int index = 0;
                        foreach (var group in lstGroup)
                        {
                            if (!string.IsNullOrEmpty(group.GroupName))
                            {
                                if (index == 0)
                                {
                                    item.GroupName = group.GroupName;
                                }
                                else
                                {
                                    item.GroupName = item.GroupName + "; " + group.GroupName;
                                }
                                index++;
                            }
                        }
                    }
                    //export excel
                    try
                    {
                        //column 1 is INDEX;
                        worksheet.Cells[rowIndex, 1].Value = i + 1;
                        worksheet.Cells[rowIndex, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 2 is DEPARTMENT_NAME
                        worksheet.Cells[rowIndex, 2].Value = item.DeptName;
                        //column 3 is GROUP_NAME
                        worksheet.Cells[rowIndex, 3].Value = item.GroupName;
                        //column 4 is PERSON_CODE
                        worksheet.Cells[rowIndex, 4].Value = item.PersonCode;
                        //column 5 is FULL_NAME
                        worksheet.Cells[rowIndex, 5].Value = item.PersonName;
                        worksheet.Cells[rowIndex, 6].Value = item.DayFormat;
                        worksheet.Cells[rowIndex, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[rowIndex, 6].Value = item.DayFormat;
                        if (item.DayOfWeek == 6 || item.DayOfWeek == 0)
                        {
                            //column 6 is DAY_FORMAT
                            Color colorWeekend = System.Drawing.ColorTranslator.FromHtml("#D5E4F7");
                            worksheet.Cells[rowIndex, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[rowIndex, 6].Style.Fill.BackgroundColor.SetColor(colorWeekend);
                        }
                        //column 7 is WORK_SHIFT
                        worksheet.Cells[rowIndex, 7].Value = item.WorkingShiftName;
                        worksheet.Cells[rowIndex, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 8 is CHECK_IN
                        worksheet.Cells[rowIndex, 8].Value = item.TimeFromFormat;
                        worksheet.Cells[rowIndex, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 9 is break time
                        worksheet.Cells[rowIndex, 9].Value = item.BreakTime;
                        worksheet.Cells[rowIndex, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 10 is CHECK_OUT
                        worksheet.Cells[rowIndex, 10].Value = item.TimeToFormat;
                        worksheet.Cells[rowIndex, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 10 is CHECK_OUT
                        worksheet.Cells[rowIndex, 11].Value = item.TotalReal;
                        worksheet.Cells[rowIndex, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 11 is EARLY
                        worksheet.Cells[rowIndex, 12].Value = item.NumberPartion;
                        worksheet.Cells[rowIndex, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //column 12 is TOTAL_SHIFT
                        worksheet.Cells[rowIndex, 13].Value = item.TotalFormat;
                        worksheet.Cells[rowIndex, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    catch (Exception ex)
                    {
                        //Lỗi
                        worksheet.Cells[rowIndex, 14].Value = "Errors Occur! - " + ex.Message;
                    }
                    rowIndex++;
                    int currentRowIndex = rowIndex - 1;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#efeadf");
                    //check if this is the end of that person information
                    if (i < rs.Data.Count - 1)
                    {
                        var afterItem = rs.Data[i + 1];
                        if (!string.IsNullOrEmpty(item.PersonCode) && !string.IsNullOrEmpty(afterItem.PersonCode))
                        {
                            string curPerCode = item.PersonCode.Trim();
                            string afterPerCode = afterItem.PersonCode.Trim();
                            if (curPerCode != afterPerCode)
                            {
                                //merge old person 
                                for (int j = 2; j < 6; j++)
                                {
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Merge = true;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[startRowPerItem, j, currentRowIndex, j].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                }
                                //insert a black space to end of this person (after this row)
                                worksheet.Cells[rowIndex, 1, rowIndex, 14].Merge = true;
                                //next person
                                rowIndex++;
                                startRowPerItem = rowIndex;
                            }
                        }
                    }
                }
                int lastRow = rowIndex - 1;
                Color colFromHex2 = System.Drawing.ColorTranslator.FromHtml("#efeadf");
                // merge
                for (int j = 2; j < 6; j++)
                {
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Merge = true;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[startRowPerItem, j, lastRow, j].Style.Fill.BackgroundColor.SetColor(colFromHex2);
                }
                //border all 
                var modelTable = worksheet.Cells[START_ROW, 1, lastRow, 14];
                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //modelTable.AutoFitColumns();
                package.Save();
                System.Threading.Thread.Sleep(1000); //Dừng tí chờ save
            }
            return Ok("download\\ChamCong\\" + _userIdentity.CompId + "\\" + fileDownloadName);
        }
    }
}
