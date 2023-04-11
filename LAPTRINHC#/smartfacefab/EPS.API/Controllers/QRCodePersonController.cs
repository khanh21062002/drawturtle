using EPS.API.Helpers;
using EPS.Service;
using EPS.Service.Dtos.ApiFace;
using EPS.Service.Dtos.ApiFace.ApiDetectFace;
using EPS.Service.Dtos.PersonQRUpdate;
using EPS.Service.Dtos.PersonQRUpdate.QRCodePerson;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EPS.Data;
using AutoMapper;
using EPS.Service.Helpers;
using EPS.Data.Entities;
using System.Linq;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.Person;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/qrCodePerson")]
    [Authorize]

    public class QRCodePersonController : BaseController
    {
        private QRCodePersonService _qrCodePerson;
        private PersonService _personService;
        private IUserIdentity<int> _userIdentity;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings;
        private EPSRepository _repository;
        private IMapper _mapper;
        private EPSBaseService _baseService;

        public QRCodePersonController(QRCodePersonService qrPersonCodeService, PersonService personService, IUserIdentity<int> userIdentity, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings, EPSRepository repository, IMapper mapper)
        {
            _qrCodePerson = qrPersonCodeService;
            _personService = personService;
            _userIdentity = userIdentity;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value;

            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        //list all
        [HttpGet]
        [CustomAuthorize(PrivilegeList.ViewApproveCodeQR, PrivilegeList.ManageApproveCodeQR)]
        public async Task<IActionResult> GetQRCodePersons([FromQuery] PersonQRUpdateGridPagingDto pagingModel)
        {
            var currentComp = _repository.Filter<Company>(x => x.Id == _userIdentity.CompId).FirstOrDefault();
            pagingModel.compId = currentComp.Id;
            return Ok(await _qrCodePerson.GetAll(pagingModel));
        }

        //get qr code current use for update person
        [HttpGet("QRCode")]
        [AllowAnonymous]
        public async Task<IActionResult> GetQRCodePersonDetail()
        {
            return Ok(await _qrCodePerson.GetQRCodePersonDetail());
        }

        //get qr update person by id
        [HttpGet("detailPerson/{id}")]
        public async Task<IActionResult> GetQRUpdateById(int id)
        {
            return Ok(await _qrCodePerson.GetQRUpdateById(id));
        }

        //create qr code for update person
        [HttpPost("QRCode")]
        [CustomAuthorize(PrivilegeList.ViewIssueCodeQR, PrivilegeList.ManageIssueCodeQR)]
        public async Task<IActionResult> CreateQRCodePerson(QRCodePersonUpdateDto updateDto)
        {
            await _qrCodePerson.CreateQRCodePerson(updateDto);
            return Ok();
        }

        //update qr code for update person
        [HttpPut("QRCode")]
        [CustomAuthorize(PrivilegeList.ViewIssueCodeQR, PrivilegeList.ManageIssueCodeQR)]
        public async Task<IActionResult> UpdateQRCodePerson(Guid id, QRCodePersonUpdateDto updateDto)
        {
            await _qrCodePerson.UpdateQRCodePerson(id, updateDto);
            return Ok();
        }

        //reject request qr code for update person
        [HttpPost("approveReq")]
        [CustomAuthorize(PrivilegeList.ViewApproveCodeQR, PrivilegeList.ManageApproveCodeQR)]
        public async Task<IActionResult> ApproveRequest(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var lstId = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                foreach (var item in lstId)
                {
                    string contentRootPath = _webHostEnvironment.ContentRootPath;
                    var rootPath = Directory.GetParent(contentRootPath);
                    string uploadFolder = _appSettings.UploadPersonImgFolder;
                    string path = Path.Combine(rootPath.FullName, uploadFolder);

                    var findQRUpd = await _repository.FindAsync<PersonQRUpdate>(x => x.Id == item && x.Status == 1);
                    if (findQRUpd != null)
                    {
                        var findPerson = await _repository.FindAsync<Person>(x => x.PersonId == findQRUpd.PersonId && x.IsDelete == false);
                        var facePersonUpd = await _baseService.FindAsync<Person, PersonUpdateDto>(x => x.PersonId == findQRUpd.PersonId && x.IsDelete == false);
                        if (findPerson.File1 != null)
                        {
                            var pathPs = findPerson.CompId + "\\" + findPerson.File1.Split("\\")[3];
                            var pathUpd = findQRUpd.ImageUpdate.Split("\\")[2] + "\\" + findQRUpd.ImageUpdate.Split("\\")[3];

                            string sourcePath = path + pathUpd;
                            string destPath = path + pathPs;
                            System.IO.File.Copy(sourcePath, destPath, true);

                            //update trường faceStatus trong bảng Faces
                            var facePerson = await _baseService.FindAsync<Face, FaceUpdateDto>(x => x.PersonId == findQRUpd.PersonId && x.Status == 1);
                            facePerson.FaceStatus = 1;
                            await _qrCodePerson.UpdateFaces(facePerson.FaceId, facePerson);
                        }
                        else
                        {
                            var compPs = findPerson.CompId + "\\";
                            string compPath = Path.Combine(path + compPs);
                            if (!Directory.Exists(compPath))
                            {
                                Directory.CreateDirectory(compPath);
                            }

                            var pathUpd = findQRUpd.ImageUpdate.Split("\\")[2] + "\\" + findQRUpd.ImageUpdate.Split("\\")[3];
                            var fullTargetPath = findPerson.CompId + "\\" + findQRUpd.ImageUpdate.Split("\\")[3];

                            string sourceFile = Path.Combine(path + pathUpd);
                            string destFile = Path.Combine(path + fullTargetPath);
                            System.IO.File.Copy(sourceFile, destFile, true);

                            //update image to Persons table
                            facePersonUpd.File1 = "img\\faces\\" + findPerson.CompId + "\\" + findQRUpd.ImageUpdate.Split("\\")[3];
                            await _personService.UpdatePersonImage(findQRUpd.PersonId, facePersonUpd);

                            //update trường faceStatus trong bảng Faces
                            var facePerson = await _baseService.FindAsync<Face, FaceUpdateDto>(x => x.PersonId == findQRUpd.PersonId && x.Status == 1);
                            if (facePerson != null)
                            {
                                facePerson.FaceStatus = 1;
                                await _qrCodePerson.UpdateFaces(facePerson.FaceId, facePerson);
                            }
                            else
                            {
                                //insert bang Face
                                FaceCreateDto faceCreate = new FaceCreateDto();
                                faceCreate.FaceId = Guid.NewGuid();
                                faceCreate.PersonId = findQRUpd.PersonId;
                                faceCreate.FacePath = "img\\faces\\" + findPerson.CompId + "\\" + findQRUpd.ImageUpdate.Split("\\")[3];
                                faceCreate.FaceStatus = 1;
                                faceCreate.Status = 1;
                                await _personService.CreatePersonDetail(faceCreate);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("QRCodePerson.BackEnd.Controller.ChooseRequest");
                    }
                }
                return Ok(await _qrCodePerson.ApproveRequest(lstId));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //reject request qr code for update person
        [HttpPost("rejectReq")]
        [CustomAuthorize(PrivilegeList.ViewApproveCodeQR, PrivilegeList.ManageApproveCodeQR)]
        public async Task<IActionResult> RejectRequest(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var lstId = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _qrCodePerson.RejectRequest(lstId));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("findByCode/{code}/{compId}")]
        public async Task<IActionResult> GetPersonDetailByCode(string code, int compId)
        {
            return Ok(await _qrCodePerson.GetPersonDetailByCode(code, compId));
        }

        //detail
        [AllowAnonymous]
        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            //await _qrCodePerson.CheckPerson(id, (int)_userIdentity.CompId);
            return Ok(await _qrCodePerson.GetPersonById(id));
        }

        [AllowAnonymous]
        [HttpPost("updateImage")]
        public async Task<IActionResult> CreateImageUpdate(UpdateImageDto updateDto)
        {
            ApiInputDetectDto img = new ApiInputDetectDto();
            img.img_url = "";
            img.img_base64 = "data:image/jpeg;base64," + updateDto.FileData;

            var json = JsonConvert.SerializeObject(img);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = _appSettings.ApiDetectFace;
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            var contents = response.Content.ReadAsStringAsync().Result;
            var notifyUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultDetectDto>(contents);
            if (notifyUsers.data != null)
            {
                if (notifyUsers.data.quality >= 0.63 && notifyUsers.data.liveness == 1 && notifyUsers.data.wearmask == 0)
                {
                    byte[] newBytes = Convert.FromBase64String(updateDto.FileData);
                    var _file = newBytes;
                    if (_file.Length > 0)
                    {
                        string contentRootPath = _webHostEnvironment.ContentRootPath;
                        var rootPath = Directory.GetParent(contentRootPath);
                        string uploadFolder = _appSettings.UploadPersonImgFolder + "updateImage" + "\\";
                        string path = Path.Combine(rootPath.FullName, uploadFolder);
                        //check folder tồn tại hay chưa
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        var countPerson = await _repository.CountAsync<PersonQRUpdate>(x => x.PersonId == updateDto.PersonId && x.Status == 1);
                        if (countPerson > 0)
                        {
                            var idUpdate = _repository.Filter<PersonQRUpdate>(x => x.PersonId == updateDto.PersonId && x.Status == 1).Select(x => x.Id).FirstOrDefault();
                            var personQR = await _repository.FindAsync<PersonQRUpdate>(x => x.Id == idUpdate);

                            var fname = personQR.ImageUpdate.Split('\\');
                            var filePath = _appSettings.UploadPersonImgFolder + "updateImage" + "\\" + fname[3];
                            if (Directory.Exists(filePath))
                            {
                                Directory.Delete(filePath);
                            }

                            var filename = Guid.NewGuid() + ".jpg";
                            var fullPath = Path.Combine(path, filename);
                            System.IO.File.WriteAllBytes(fullPath, newBytes);
                            updateDto.ImageUpdate = "img\\faces\\" + "updateImage" + "\\" + filename;
                            updateDto.Id = idUpdate;
                            await _qrCodePerson.UpdateImageUpdate(idUpdate, updateDto);
                        }
                        else
                        {
                            var filename = Guid.NewGuid() + ".jpg";
                            var fullPath = Path.Combine(path, filename);
                            System.IO.File.WriteAllBytes(fullPath, newBytes);
                            updateDto.ImageUpdate = "img\\faces\\" + "updateImage" + "\\" + filename;
                            await _qrCodePerson.CreateImageUpdate(updateDto);
                        }
                        return Ok(0);
                    }
                    else
                    {
                        throw new Exception("QRCodePerson.BackEnd.Controller.ValidImage");
                    }
                }
                else
                {
                    throw new Exception("QRCodePerson.BackEnd.Controller.ValidImage");
                }
            }
            else
            {
                throw new Exception("QRCodePerson.BackEnd.Controller.ValidImage");
            }
        }

        //Danh sách đơn vị
        [AllowAnonymous]
        [HttpGet("detailQR/{id}")]
        public async Task<IActionResult> GetDetailByIdQR(Guid id)
        {
            return Ok(await _qrCodePerson.GetDetailByIdQR(id));
        }

        //Danh sách đơn vị
        [AllowAnonymous]
        [HttpGet("companys")]
        public async Task<IActionResult> GetCompany()
        {
            return Ok(await _qrCodePerson.GetCompanys());
        }

        [AllowAnonymous]
        [HttpGet("department-tree")]
        public async Task<IActionResult> GetDepartmentTree()
        {
            return Ok(await _qrCodePerson.GetDepartmentTree());
        }
    }
}
