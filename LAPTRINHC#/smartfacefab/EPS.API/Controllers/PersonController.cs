using ArcFaceService.Entity;
using ArcFaceService.Service;
using EPS.API.Helpers;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.ApiFace;
using EPS.Service.Dtos.ApiFace.ApiDetectFace;
using EPS.Service.Dtos.Card;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.FaceApiDto.Detect;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.PersonGroup;
using EPS.Service.Dtos.PersonsAccess;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/person")]
    [Authorize]

    public class PersonController : BaseController
    {
        private PersonService _personService;
        private GroupService _groupService;
        private DepartmentService _departmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings;
        private IUserIdentity<int> _userIdentity;
        private CheckDataService _checkDataService;
        private static readonly HttpClient client = new HttpClient();

        public PersonController(PersonService personService, IUserIdentity<int> userIdentity, GroupService groupService, DepartmentService departmentService, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings, CheckDataService checkDataService)
        {
            _personService = personService;
            _webHostEnvironment = webHostEnvironment;
            _departmentService = departmentService;
            _appSettings = appSettings.Value;
            _groupService = groupService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        [AllowAnonymous]
        [HttpPost("FileUpload")]
        [CustomAuthorize(PrivilegeList.ViewPerson, PrivilegeList.ManagePerson)]
        public IActionResult Index(PersonCreateDto files)
        {
            return Ok();
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewPerson, PrivilegeList.ManagePerson)]
        [HttpPost("list")]
        public async Task<IActionResult> GetListPersons(PersonGridPagingDto pagingModel)
        {
            try
            {
                pagingModel.CompId = _userIdentity.CompId;
                var lst = await _personService.GetPersons(pagingModel);
                return Ok(lst);
            }
            catch
            {
                return Ok(await _personService.GetPersons(pagingModel));
            }
        }

        [CustomAuthorize(PrivilegeList.ViewPerson, PrivilegeList.ManagePerson)]
        [HttpPost("listPerson")]
        public async Task<IActionResult> GetListEvents(PersonGridPagingDto pagingModel)
        {
            //pagingModel.CompId = _userIdentity.CompId;

            //ImageSearchDto a = new ImageSearchDto();
            //ImageSearchListDto b = new ImageSearchListDto();
            //b.collection_id = _userIdentity.CompId;
            //b.threshold = pagingModel.ValueThreshold;
            //b.num_result = 10;
            //b.img_url = "";
            //b.img_base64 = pagingModel.FileData;
            //var json = JsonConvert.SerializeObject(b);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");

            //var url = _appSettings.ApiSearchFace;
            //using var client = new HttpClient();
            //var response = await client.PostAsync(url, data);
            //var contents = response.Content.ReadAsStringAsync().Result;
            //var notifyUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageSearch3Dto<Object>>(contents);

            //if (notifyUsers.status == "200" && notifyUsers.data != null)
            //{
            //    var imageSearchDto = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageSearch3Dto<ImageSearchDto>>(contents).data;
            //    if (imageSearchDto.result.Count > 0)
            //    {
            //        int check = 0;
            //        foreach (var item in imageSearchDto.result)
            //        {
            //            if (check == 0)
            //            {
            //                pagingModel.PersonId += item.personId;
            //            }
            //            else
            //            {
            //                pagingModel.PersonId += ";" + item.personId;
            //            }
            //            check++;
            //        }
            //    }
            //}

            return Ok(await _personService.GetPersons(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.ViewEmployee, PrivilegeList.ManageEmployee)]
        [HttpGet("listEmployee")]
        public async Task<IActionResult> GetListEmployee([FromQuery] PersonGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            pagingModel.filterType = 1;
            return Ok(await _personService.GetEmployee(pagingModel));
        }

        [HttpGet("PersonMachine")]
        public async Task<IActionResult> GetListPersonsMachine([FromQuery] PersonMachineGridPagingDto pagingModel)
        {
            var lst = await _personService.GetPersonsMachine(pagingModel);
            return Ok(lst);
        }

        private void saveFaceImage(int compId, string fileName, byte[] data)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var rootPath = System.IO.Directory.GetParent(contentRootPath);
            string uploadFolder = _appSettings.UploadPersonImgFolder + compId + "\\";
            string path = Path.Combine(rootPath.FullName, uploadFolder);
            //check folder tồn tại hay chưa
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fullPath = Path.Combine(path, fileName);
            System.IO.File.WriteAllBytes(fullPath, data);
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ResponseMessage> ApiCreatePerson(PersonCreateDto personCreateDto)
        {
            ResponseMessage res = new ResponseMessage();

            try
            {
                res = await CreatePerson(personCreateDto);
                return res;
            }
            catch (Exception ex)
            {
                res.status = 500;
                res.message = ex.Message;
                res.data = null;

                if (ex.Message.Contains("Privilege"))
                {
                    res.message = ex.Message;
                }
                return res;
            }
        }

        [CustomAuthorize(PrivilegeList.ManagePerson, PrivilegeList.ManageEmployee)]
        public async Task<ResponseMessage> CreatePerson(PersonCreateDto personCreateDto)
        {
            ResponseMessage res = new ResponseMessage();

            if (personCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(personCreateDto.CompId.Value, personCreateDto.CompId.Value);
            }
            if (!String.IsNullOrEmpty(personCreateDto.Position))
            {
                personCreateDto.Position = personCreateDto.Position.Replace(",", "ExImport.Person.Person2");
            }
            if (!String.IsNullOrEmpty(personCreateDto.JobDuties))
            {
                personCreateDto.JobDuties = personCreateDto.JobDuties.Replace(",", " ExImport.Person.Person2 ");
            }

            byte[] faceImg = Convert.FromBase64String(personCreateDto.FileData);
            if (faceImg.Length == 0)
            {
                res.status = 400;
                res.message = "Face image is invalid";
                return res;
            }

            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (personCreateDto.PersonCode == null || personCreateDto.PersonCode == "")
            {
                personCreateDto.PersonCode = "USER_" + timeStamp;
                personCreateDto.FullName = "USER " + timeStamp;
            }

            //Lưu person            
            var personId = Guid.NewGuid();
            if (personCreateDto.PersonId != null && personCreateDto.PersonId != Guid.Empty)
            {
                personId = personCreateDto.PersonId;
            }
            else
            {
                personCreateDto.PersonId = personId;
            }

            var faceId = Guid.NewGuid();
            var filename = faceId + ".jpg";
            saveFaceImage(personCreateDto.CompId.Value, filename, faceImg);

            var AvatarPath = "img\\faces\\" + personCreateDto.CompId + "\\" + filename;
            personCreateDto.File1 = "img\\faces\\" + personCreateDto.CompId + "\\" + filename;
            await _personService.CreatePerson(personCreateDto);

            //Lưu face
            var faceCreate = new FaceCreateDto();
            faceCreate.FaceId = faceId;
            faceCreate.PersonId = personId;
            faceCreate.FacePath = AvatarPath;
            faceCreate.FeaturePath = personCreateDto.FaceFeature;
            faceCreate.ImageBase64 = personCreateDto.FileData;
            faceCreate.WearMask = personCreateDto.WearMask;
            faceCreate.FaceStatus = 2;
            faceCreate.Status = 1;
            await _personService.CreatePersonDetail(faceCreate);

            res.status = 200;
            res.message = "Create person success";
            res.data = personId;
            return res;
        }

        //create
        [CustomAuthorize(PrivilegeList.ManagePerson, PrivilegeList.ManageEmployee)]
        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto personCreateDto, float? quality, float imageQuality)
        {
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            //nếu k có mã nhân viên thì tự sinh
            if (string.IsNullOrEmpty(personCreateDto.PersonCode))
                personCreateDto.PersonCode = "USER_" + timeStamp;

            if (imageQuality == 0)
            {
                imageQuality = 1;
            }
            if (personCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(personCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            if (!String.IsNullOrEmpty(personCreateDto.Position))
            {
                personCreateDto.Position = personCreateDto.Position.Replace(",", "ExImport.Person.Person2");
            }
            if (!String.IsNullOrEmpty(personCreateDto.JobDuties))
            {
                personCreateDto.JobDuties = personCreateDto.JobDuties.Replace(",", " ExImport.Person.Person2 ");
            }

            var personId = Guid.NewGuid();
            if (personCreateDto.PersonId != null && personCreateDto.PersonId != Guid.Empty)
            {
                personId = personCreateDto.PersonId;
            }
            else
            {
                personCreateDto.PersonId = personId;
            }

            if (personCreateDto.FileData == null)
            {
                personCreateDto.PersonId = personId;
                await _personService.CreatePerson(personCreateDto);
                return Ok(personCreateDto.PersonId);
            }

            ApiInputDetectDto img = new ApiInputDetectDto();
            img.img_url = "";
            img.img_base64 = personCreateDto.FileData;

            var json = JsonConvert.SerializeObject(img);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = _appSettings.ApiDetectFace;
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            var contents = response.Content.ReadAsStringAsync().Result;
            var notifyUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultDetectDto>(contents);

            //var faceDetectInput = new DetectFaceDto();
            //faceDetectInput.img_base64 = personCreateDto.FileData;
            //var faceResult = DetectFace(faceDetectInput);
            //var faceAllFeature = (DetectFaceTransDataDto)faceResult.data;
            if (notifyUsers.data != null)
            {
                var checkQuality = 0.6;
                if (quality.HasValue)
                {
                    checkQuality = quality.Value;
                }
                if (notifyUsers.data.quality >= 0.63 && notifyUsers.data.wearmask == 0)
                {
                    byte[] newBytes = Convert.FromBase64String(personCreateDto.FileData);
                    //if (faceAllFeature.imageBase64 != null && faceAllFeature.imageBase64.Length > 0)
                    //{
                    //    newBytes = Convert.FromBase64String(faceAllFeature.imageBase64);
                    //}
                    var _file = newBytes;
                    if (_file.Length > 0)
                    {
                        string contentRootPath = _webHostEnvironment.ContentRootPath;
                        var rootPath = System.IO.Directory.GetParent(contentRootPath);
                        string uploadFolder = _appSettings.UploadPersonImgFolder + personCreateDto.CompId + "\\";
                        string path = Path.Combine(rootPath.FullName, uploadFolder);

                        //check folder tồn tại hay chưa
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        var faceId = Guid.NewGuid();
                        var filename = faceId + ".jpg";

                        var fullPath = Path.Combine(path, filename);
                        System.IO.File.WriteAllBytes(fullPath, newBytes);
                        var AvatarPath = "img\\faces\\" + personCreateDto.CompId + "\\" + filename;
                        personCreateDto.FileData = "";
                        personCreateDto.File1 = "img\\faces\\" + personCreateDto.CompId + "\\" + filename;
                        await _personService.CreatePerson(personCreateDto);
                        foreach (var item in personCreateDto.ListFace)
                        {
                            item.PersonId = personId;
                            item.FaceId = faceId;
                            item.FacePath = AvatarPath;
                            item.FeaturePath = notifyUsers.data.feature;
                            item.ImageBase64 = personCreateDto.FileData;
                            item.WearMask = notifyUsers.data.wearmask;
                            item.FaceStatus = 2;
                            item.Status = 1;
                            await _personService.CreatePersonDetail(item);

                            PersonsAccessCreateDto createPerAccess = new PersonsAccessCreateDto();
                            createPerAccess.PersonId = personId;
                            createPerAccess.MachineId = 0;
                            createPerAccess.FromDate = personCreateDto.FromDate;
                            createPerAccess.ToDate = personCreateDto.ToDate;
                            createPerAccess.IsDelete = false;
                            await _personService.CreateDateTimeToPersonAccess(createPerAccess);

                            var personUnit = new PersonUnit();
                            personUnit.groupId = personCreateDto.CompId.Value;
                            personUnit.personId = personId.ToString();
                            FaceServer.Instance.addFeature(personUnit);
                        }
                    }
                }
                else
                {
                    throw new Exception("Messages.ConfirmImage");
                }
            }
            else
            {
                throw new Exception("Messages.ConfirmImage");
            }

            return Ok(personCreateDto.PersonId);
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewPerson, PrivilegeList.ManagePerson, PrivilegeList.ViewEmployee, PrivilegeList.ManageEmployee)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            await _checkDataService.CheckPerson(id, (int)_userIdentity.CompId);
            return Ok(await _personService.GetPersonById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManagePerson, PrivilegeList.ManageEmployee)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Guid id, PersonUpdateDto personUpdateDto)
        {
            if (personUpdateDto.FileData == null)
            {
                //update thông tin
                await _personService.UpdatePerson(id, personUpdateDto);

                //update thời gian làm việc
                if (personUpdateDto.FromDate != null)
                {
                    if (personUpdateDto.ToDate == null)
                    {
                        personUpdateDto.ToDate = DateTime.MaxValue;
                    }
                }
                if (personUpdateDto.ToDate != null)
                {
                    if (personUpdateDto.FromDate == null)
                    {
                        personUpdateDto.FromDate = DateTime.Parse("1900-01-01");
                    }
                }
                await _personService.UpdateDateTimeToPersonAccess(id, personUpdateDto.FromDate, personUpdateDto.ToDate);

                return Ok();
            }

            //var faceDetectInput = new DetectFaceDto();
            //faceDetectInput.img_base64 = personUpdateDto.FileData;
            //var faceResult = DetectFace(faceDetectInput);
            //var faceAllFeature = (DetectFaceTransDataDto)faceResult.data;

            ApiInputDetectDto img = new ApiInputDetectDto();
            img.img_url = "";
            img.img_base64 = personUpdateDto.FileData;

            var json = JsonConvert.SerializeObject(img);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = _appSettings.ApiDetectFace;
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            var contents = response.Content.ReadAsStringAsync().Result;
            var notifyUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultDetectDto>(contents);

            if (notifyUsers.data != null)
            {
                if (notifyUsers.data.quality >= 0.63 && notifyUsers.data.wearmask == 0)
                {
                    //To do
                    byte[] newBytes = Convert.FromBase64String(personUpdateDto.FileData);
                    var _file = newBytes;
                    if (_file.Length > 0)
                    {
                        string contentRootPath = _webHostEnvironment.ContentRootPath;
                        var rootPath = System.IO.Directory.GetParent(contentRootPath);
                        string uploadFolder = _appSettings.UploadPersonImgFolder + personUpdateDto.CompId + "\\";
                        string path = Path.Combine(rootPath.FullName, uploadFolder);
                        //delete old image when update
                        string path1 = Directory.GetParent(path).ToString();
                        string path2 = Directory.GetParent(path1).ToString();
                        string path3 = Directory.GetParent(path2).ToString();
                        string path4 = Directory.GetParent(path3).ToString();
                        var oldPerson = await _personService.GetPersonById(id);
                        string deleteFilePath = "";
                        if (oldPerson != null)
                        {
                            deleteFilePath = oldPerson.file1;
                        }
                        string pathDelete = path4 + "\\" + deleteFilePath;
                        if (System.IO.File.Exists(pathDelete))
                        {
                            try
                            {
                                System.IO.File.Delete(pathDelete);
                            }
                            catch (Exception) { }
                        }

                        //check folder tồn tại hay chưa
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        var faceId = Guid.NewGuid();
                        var filename = faceId + ".jpg";
                        var fullPath = Path.Combine(path, filename);
                        System.IO.File.WriteAllBytes(fullPath, newBytes);
                        var AvatarPath = "img\\faces\\" + personUpdateDto.CompId + "\\" + filename;
                        personUpdateDto.FileData = "";
                        personUpdateDto.File1 = "img\\faces\\" + personUpdateDto.CompId + "\\" + filename;
                        //update thông tin
                        await _personService.UpdatePerson(id, personUpdateDto);

                        //update thời gian làm việc
                        await _personService.UpdateDateTimeToPersonAccess(id, personUpdateDto.FromDate, personUpdateDto.ToDate);

                        var lstActiveFace = await _personService.GetListActiveFaceByPersonId(id);

                        if (lstActiveFace.Count == 0)
                        {
                            foreach (var n in personUpdateDto.ListFace)
                            {
                                n.PersonId = id;
                                n.FaceId = faceId;
                                n.FacePath = AvatarPath;
                                n.FeaturePath = notifyUsers.data.feature;
                                n.ImageBase64 = personUpdateDto.FileData;
                                n.WearMask = notifyUsers.data.wearmask;
                                n.FaceStatus = 2;
                                n.Status = 1;
                                await _personService.CreatePersonDetail(n);

                                var personUnit = new PersonUnit();
                                personUnit.groupId = personUpdateDto.CompId.Value;
                                personUnit.personId = id.ToString();
                                FaceServer.Instance.updateFeature(personUnit);
                            }
                        }
                        else
                        {
                            foreach (var r in lstActiveFace)
                            {
                                r.Status = 0;
                                await _personService.UpdateFace(r.FaceId, r);
                            }

                            foreach (var e in personUpdateDto.ListFace)
                            {
                                e.PersonId = id;
                                e.FaceId = faceId;
                                e.FacePath = AvatarPath;
                                e.FeaturePath = notifyUsers.data.feature;
                                e.ImageBase64 = personUpdateDto.FileData;
                                e.WearMask = notifyUsers.data.wearmask;
                                e.FaceStatus = 2;
                                e.Status = 1;
                                await _personService.CreatePersonDetail(e);

                                var personUnit = new PersonUnit();
                                personUnit.groupId = personUpdateDto.CompId.Value;
                                personUnit.personId = id.ToString();
                                FaceServer.Instance.updateFeature(personUnit);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Messages.ConfirmImage");
                }
            }
            else
            {
                throw new Exception("Messages.ConfirmImage");
            }
            return Ok();
        }

        //public ResponseMessage DetectFace(DetectFaceDto detectDto)
        //{
        //    ResponseMessage res = new ResponseMessage();
        //    try
        //    {
        //        FaceResult<String, FaceFeatureUnit> analyze;
        //        if (string.IsNullOrEmpty(detectDto.img_url) && string.IsNullOrEmpty(detectDto.img_base64))
        //        {
        //            res.status = 500;
        //            res.message = "Input image must have value";
        //            res.data = null;
        //            return res;
        //        }

        //        if (!string.IsNullOrEmpty(detectDto.img_url))
        //        {
        //            analyze = FaceServer.Instance.analyzeFace(detectDto.img_url, 1);
        //        }
        //        else
        //        {
        //            analyze = FaceServer.Instance.analyzeFace(detectDto.img_base64, 3);
        //        }

        //        if (analyze.error != null)
        //        {
        //            res.status = analyze.status;
        //            res.message = analyze.error;
        //            res.data = null;
        //            return res;
        //        }
        //        else
        //        {
        //            DetectFaceTransDataDto result = new DetectFaceTransDataDto();
        //            result.quality = analyze.result.quality;

        //            //result.face_rectangle = new int[4];
        //            //result.face_rectangle[0] = analyze.result.faceRect.left;
        //            //result.face_rectangle[1] = analyze.result.faceRect.top;
        //            //result.face_rectangle[2] = analyze.result.faceRect.right;
        //            //result.face_rectangle[3] = analyze.result.faceRect.bottom;

        //            result.age = analyze.result.age;
        //            result.liveness = analyze.result.liveness;
        //            result.gender = analyze.result.gender;
        //            result.wearmask = analyze.result.mask;
        //            result.wearglass = analyze.result.wearGlass;
        //            result.lefteye = analyze.result.leftEyeClose;
        //            result.righteye = analyze.result.rightEyeClose;
        //            result.orient = analyze.result.orient;

        //            //result.angle3D = new float[3];
        //            //result.angle3D[0] = analyze.result.angle3D[0];
        //            //result.angle3D[1] = analyze.result.angle3D[1];
        //            //result.angle3D[2] = analyze.result.angle3D[2];

        //            result.feature = analyze.result.feature;
        //            if (analyze.result.imageBase64 != null && analyze.result.imageBase64.Length > 0)
        //            {
        //                result.imageBase64 = analyze.result.imageBase64;
        //            }

        //            switch (analyze.status)
        //            {
        //                case 500:
        //                    res.status = analyze.status;
        //                    res.message = analyze.error;
        //                    break;
        //                case 406:
        //                    res.status = analyze.status;
        //                    res.message = analyze.error;
        //                    break;
        //                case 200:
        //                    res.status = analyze.status;
        //                    res.message = "Success";
        //                    res.data = result;
        //                    break;
        //            }
        //            return res;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        res.status = 500;
        //        res.message = "INTERNAL_ERROR";
        //        res.data = null;

        //        return res;
        //    }
        //}

        [CustomAuthorize(PrivilegeList.ManagePerson)]
        [HttpDelete("{personid}")]
        public async Task<IActionResult> Delete(Guid personid)
        {
            await _checkDataService.CheckPerson(personid, (int)_userIdentity.CompId);
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var rootPath = System.IO.Directory.GetParent(contentRootPath);
            string uploadFolder = _appSettings.UploadPersonImgFolder + "\\";
            string path = Path.Combine(rootPath.FullName, uploadFolder);

            //delete old image when update
            string path1 = Directory.GetParent(path).ToString();
            string path2 = Directory.GetParent(path1).ToString();
            string path3 = Directory.GetParent(path2).ToString();
            var oldPerson = await _personService.GetPersonById(personid);
            string deleteFilePath = "";
            if (oldPerson != null)
            {
                deleteFilePath = oldPerson.file1;
            }
            string pathDelete = path3 + "\\" + deleteFilePath;
            if (System.IO.File.Exists(pathDelete))
            {
                try
                {
                    System.IO.File.Delete(pathDelete);
                }
                catch (Exception) { }
            }

            //Delete in database
            int res = await _personService.UpdateDelete(personid);

            //Xóa thời gian làm việc
            await _personService.DeleteDateTimeToPersonAccess(personid);

            //Delete in face engine
            var personUnit = new PersonUnit();
            personUnit.groupId = oldPerson.CompId.Value;
            personUnit.personId = personid.ToString();
            FaceServer.Instance.removeFeature(personUnit);

            return Ok(res);
        }

        [CustomAuthorize(PrivilegeList.ManageEmployee)]
        [HttpPost("import")]
        public async Task<IActionResult> Import(PersonImportDto file)
        {
            var fileDownloadName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ImportNhanVien.xlsx";
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var rootPath = System.IO.Directory.GetParent(contentRootPath);
            string uploadFolder = _appSettings.DownloadFolder + _userIdentity.CompId + "\\";
            string path = Path.Combine(rootPath.FullName, uploadFolder);
            Service.Dtos.Person.ImportDto imp = new Service.Dtos.Person.ImportDto();
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

                //Get All phòng ban
                var lstDepart = _departmentService.GetAllDepartment();

                //Get All nhân sự
                var lstPerson = _personService.GetAllPerson();
                var fi = new FileInfo(path + fileDownloadName);
                var lstPersonInsert = new List<PersonCreateDto>();
                var lstPersonAccessInsert = new List<PersonsAccessCreateDto>();
                using (var package = new ExcelPackage(fi))
                {
                    var worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            PersonCreateDto per = new PersonCreateDto();
                            per.PersonId = Guid.NewGuid();
                            per.CompId = _userIdentity.CompId;
                            per.PersonType = 1;
                            per.Status = 1;
                            per.RegisterBy = _userIdentity.UserId.ToString();
                            per.RegisterTime = DateTime.Now;
                            per.IsDelete = false;

                            var rowCode = worksheet.Cells[row, 1].Value;
                            if (rowCode != null && !string.IsNullOrEmpty(rowCode.ToString().Trim()))
                            {
                                //Check mã nhân viên
                                var isEx = lstPerson.Any(x => x.PersonCode == rowCode.ToString().Trim());
                                if (isEx)
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 12].Value = "Mã nhân viên đã tồn tại";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 12].Value = "Employee code already exist";
                                        continue;
                                    }
                                }
                                else
                                {
                                    per.PersonCode = rowCode.ToString().Trim();
                                }
                            }
                            else
                            {
                                if (file.Lang == "vi")
                                {
                                    worksheet.Cells[row, 12].Value = "Thiếu mã nhân viên";
                                    continue;
                                }
                                else
                                {
                                    worksheet.Cells[row, 12].Value = "Employee code is not null";
                                    continue;
                                }
                            }

                            var rowName = worksheet.Cells[row, 2].Value;
                            if (rowName != null && !string.IsNullOrEmpty(rowName.ToString().Trim()))
                            {
                                per.FullName = rowName.ToString().Trim();
                            }
                            else
                            {
                                if (file.Lang == "vi")
                                {
                                    worksheet.Cells[row, 12].Value = "Thiếu tên nhân viên";
                                    continue;
                                }
                                else
                                {
                                    worksheet.Cells[row, 12].Value = "Employee name is not null";
                                    continue;
                                }
                            }

                            var rowGender = worksheet.Cells[row, 3].Value;
                            if (rowGender != null && !string.IsNullOrEmpty(rowGender.ToString().Trim()))
                            {
                                string strGender = rowGender.ToString().Trim().ToLower();
                                if (strGender == "nam" || strGender == "Nam" || strGender == "NAM" || strGender == "Male" || strGender == "male" || strGender == "MALE")
                                    per.Gender = 1;
                                else if (strGender == "nữ" || strGender == "Nữ" || strGender == "NỮ" || strGender == "Female" || strGender == "female" || strGender == "FEMALE")
                                    per.Gender = 0;
                            }
                            else
                            {
                                per.Gender = null;
                            }

                            var rowBirthDate = worksheet.Cells[row, 4].Value;
                            DateTime dateBirth;
                            if (rowBirthDate != null && !string.IsNullOrEmpty(rowBirthDate.ToString().Trim()))
                            {
                                bool chValidity = DateTime.TryParseExact(rowBirthDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateBirth);
                                if (chValidity == true)
                                {
                                    string strBirthDate = rowBirthDate.ToString().Trim();
                                    per.Birthday = DateTime.ParseExact(strBirthDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 12].Value = "Sai định dạng ngày sinh";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 12].Value = "Incorrect date format";
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                per.Birthday = null;
                            }

                            var rowDeptCode = worksheet.Cells[row, 5].Value;
                            if (rowDeptCode != null && !string.IsNullOrEmpty(rowDeptCode.ToString().Trim()))
                            {
                                var deptDto = lstDepart.Find(x => x.Code == rowDeptCode.ToString().Trim());
                                if (deptDto != null)
                                {
                                    per.DeptId = deptDto.Id;
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 12].Value = "Không tìm thấy phòng ban";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 12].Value = "Department not found";
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (file.Lang == "vi")
                                {
                                    worksheet.Cells[row, 12].Value = "Vui lòng nhập mã phòng ban";
                                    continue;
                                }
                                else
                                {
                                    worksheet.Cells[row, 12].Value = "Please input department code";
                                    continue;
                                }
                            }

                            var rowPhoneNumber = worksheet.Cells[row, 6].Value;
                            if (rowPhoneNumber != null && !string.IsNullOrEmpty(rowPhoneNumber.ToString().Trim()))
                            {
                                per.PhoneNumber = rowPhoneNumber.ToString().Trim();
                            }

                            var rowHomeAddress = worksheet.Cells[row, 7].Value;
                            if (rowHomeAddress != null && !string.IsNullOrEmpty(rowHomeAddress.ToString().Trim()))
                            {
                                per.HomeAddress = rowHomeAddress.ToString().Trim();
                            }

                            PersonsAccessCreateDto createPerAccess = new PersonsAccessCreateDto();
                            createPerAccess.PersonId = per.PersonId;
                            createPerAccess.MachineId = 0;
                            createPerAccess.IsDelete = false;

                            //Thời gian làm việc
                            var rowWorkFrom = worksheet.Cells[row, 8].Value;
                            DateTime dateFrom;
                            if (rowWorkFrom != null && !string.IsNullOrEmpty(rowWorkFrom.ToString().Trim()))
                            {
                                bool chValidity = DateTime.TryParseExact(rowWorkFrom.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFrom);
                                if (chValidity == true)
                                {
                                    string strWorkFrom = rowWorkFrom.ToString().Trim();
                                    per.FromDate = DateTime.ParseExact(strWorkFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    createPerAccess.FromDate = per.FromDate;
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 12].Value = "Sai định dạng ngày bắt đầu làm việc";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 12].Value = "Incorrect start date format";
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (file.Lang == "vi")
                                {
                                    worksheet.Cells[row, 12].Value = "Thiếu thời gian bắt đầu làm việc";
                                    continue;
                                }
                                else
                                {
                                    worksheet.Cells[row, 12].Value = "Start date is not null";
                                    continue;
                                }
                            }

                            var rowWorkTo = worksheet.Cells[row, 9].Value;
                            DateTime dateTo;
                            if (rowWorkTo != null && !string.IsNullOrEmpty(rowWorkTo.ToString().Trim()))
                            {
                                bool chValidity = DateTime.TryParseExact(rowWorkTo.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTo);
                                if (chValidity == true)
                                {
                                    string strWorkFrom = rowWorkTo.ToString().Trim();
                                    per.ToDate = DateTime.ParseExact(strWorkFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    createPerAccess.ToDate = per.ToDate;
                                    if (per.FromDate != null)
                                    {
                                        if (per.FromDate > per.ToDate)
                                        {
                                            if (file.Lang == "vi")
                                            {
                                                worksheet.Cells[row, 12].Value = "Ngày kết thúc làm việc phải lớn hơn hoặc bằng ngày bắt đầu làm việc";
                                                continue;
                                            }
                                            else
                                            {
                                                worksheet.Cells[row, 12].Value = "Start working time must be smaller than end working time";
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (file.Lang == "vi")
                                    {
                                        worksheet.Cells[row, 12].Value = "Sai định dạng ngày kết thúc làm việc";
                                        continue;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, 12].Value = "Incorrect end date format";
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                createPerAccess.ToDate = DateTime.MaxValue;
                            }

                            lstPersonAccessInsert.Add(createPerAccess);
                            lstPersonInsert.Add(per);
                            countSuccess = countSuccess + 1;

                            if (file.Lang == "vi")
                            {
                                worksheet.Cells[row, 12].Value = "Thành công";
                                continue;
                            }
                            else
                            {
                                worksheet.Cells[row, 12].Value = "Success";
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            //Lỗi
                            worksheet.Cells[row, 12].Value = ex.Message;
                            continue;
                        }
                    }
                    //doing bulk insert to DB
                    try
                    {
                        var rsPer = await _personService.CreateListPerson(lstPersonInsert, lstPersonAccessInsert, false);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    imp.COUNT = countSuccess;
                    imp.URL = "download\\" + _userIdentity.CompId + "\\" + fileDownloadName;
                    imp.SUMCOUNT = rowCount - 1;
                    package.Save();
                    System.Threading.Thread.Sleep(1000); //Dừng tí chờ save
                }
            }
            return Ok(imp);
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManagePerson)]
        [HttpDelete]
        public async Task<IActionResult> DeletePersons(String ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var arrPersonId = ids.Split(',');

                //Delete in face engine
                foreach (var personid in arrPersonId)
                {
                    var personDelete = await _personService.GetPersonById(Guid.Parse(personid));
                    var personUnit = new PersonUnit();
                    personUnit.groupId = personDelete.CompId.Value;
                    personUnit.personId = personid.ToString();
                    FaceServer.Instance.removeFeature(personUnit);

                    //Xóa thời gian làm việc
                    await _personService.DeleteDateTimeToPersonAccess(Guid.Parse(personid));
                }

                //Delete in database
                var res = await _personService.UpdateDeletes(arrPersonId);

                return Ok(res);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

