using EPS.API.Helpers;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.ApiFace;
using EPS.Service.Dtos.ApiFace.ApiDetectFace;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.Guess;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.PersonsAccess;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/guess")]
    [Authorize]
    public class GuessController : BaseController
    {
        private GuessService _guessService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings;
        private PersonService _personService;
        private FaceService _faceService;
        private PersonsAccessService _personsAccessService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public GuessController(GuessService guessService, PersonService personService, PersonsAccessService personsAccessService, FaceService faceService, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _guessService = guessService;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value;
            _personService = personService;
            _faceService = faceService;
            _personsAccessService = personsAccessService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        [AllowAnonymous]
        [HttpGet("requestOTP/{phoneNumber}")]
        public async Task<IActionResult> RequestOTP(string phoneNumber)
        {
            OTPCreateDto oTPCreateDto = new OTPCreateDto();
            oTPCreateDto.PhoneNumber = phoneNumber;

            return Ok(await _guessService.CreateOTP(oTPCreateDto));
        }

        [AllowAnonymous]
        [HttpGet("checkOTP/{phoneNumber}/{otpCode}")]
        public async Task<IActionResult> CheckOTP(string phoneNumber, string otpCode)
        {
            return Ok(await _guessService.CheckOTP(phoneNumber, otpCode));
        }

        //Lấy thông tin đăng ký gần nhất theo số điện thoại
        [AllowAnonymous]
        [HttpGet("getInfo/{phoneNumber}")]
        [RequireReferrer("https://smf-test.atin.vn/", "https://smartface.atin.vn/", "http://localhost:8080/")]
        public async Task<IActionResult> GetInfo(string phoneNumber)
        {
            if (phoneNumber != null)
            {
                // await _checkDataService.CheckGuessByPhoneNumber(phoneNumber, (int)_userIdentity.CompId);
                return Ok(await _guessService.GetGuessByPhoneNumber(phoneNumber));
            }
            else
            {
                var errors = "Vui lòng nhập số điện thoại!";
                throw new EPSException(errors);
            }
        }

        //create với khách
        [HttpPost("createGuess")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateGuess(GuessRegisterCreateDto guessRegisterCreateDto)
        {
            if (!string.IsNullOrEmpty(guessRegisterCreateDto.base64Image))
            {
                byte[] newBytes = Convert.FromBase64String(guessRegisterCreateDto.base64Image.Split(',')[1]);
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                var rootPath = System.IO.Directory.GetParent(contentRootPath);
                string uploadFolder = _appSettings.UploadPersonImgFolder + guessRegisterCreateDto.ComId + "\\ImgGuess\\";
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
                var AvatarPath = "img\\faces\\" + guessRegisterCreateDto.ComId + "\\ImgGuess\\" + filename;
                guessRegisterCreateDto.ImageUrl = AvatarPath;
            }
            guessRegisterCreateDto.ID = 0;
            var guessId = await _guessService.CreateGuess(guessRegisterCreateDto);

            await _guessService.ApproveGuess(guessId, guessRegisterCreateDto.Approve.Value, null);
            return Ok();
        }

        //create với lễ tân
        [HttpPost]
        [CustomAuthorize(PrivilegeList.ManageGuess)]
        public async Task<IActionResult> Create(GuessRegisterCreateDto guessRegisterCreateDto)
        {
            if (guessRegisterCreateDto.ComId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize((int)_userIdentity.CompId, (int)guessRegisterCreateDto.ComId);
            }
            if (!string.IsNullOrEmpty(guessRegisterCreateDto.base64Image))
            {
                var imgBase64 = guessRegisterCreateDto.base64Image.Split(',')[1];

                ApiInputDetectDto img = new ApiInputDetectDto();
                img.img_url = "";
                img.img_base64 = imgBase64;

                var json = JsonConvert.SerializeObject(img);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = _appSettings.ApiDetectFace;
                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var contents = response.Content.ReadAsStringAsync().Result;
                var notifyUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultDetectDto>(contents);
                if (notifyUsers.data != null)
                {
                    if (notifyUsers.data.quality >= 0.63 && notifyUsers.data.liveness == 1)
                    {
                        byte[] newBytes = Convert.FromBase64String(imgBase64);
                        string contentRootPath = _webHostEnvironment.ContentRootPath;
                        var rootPath = System.IO.Directory.GetParent(contentRootPath);
                        string uploadFolder = _appSettings.UploadPersonImgFolder + guessRegisterCreateDto.ComId + "\\ImgGuess\\";
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
                        var AvatarPath = "img\\faces\\" + guessRegisterCreateDto.ComId + "\\ImgGuess\\" + filename;
                        guessRegisterCreateDto.ImageUrl = AvatarPath;
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
            }

            //lấy perId
            var perId = _guessService.GetPersonId(guessRegisterCreateDto.PhoneNumber.Trim());

            //ins dữ liệu nếu chưa có
            if (!perId.HasValue)
            {
                //Nếu chưa có thông tin thì chuyển dữ liệu vào bảng person
                PersonCreateDto perCreate = new PersonCreateDto();
                perCreate.PersonId = Guid.NewGuid();
                perCreate.CompId = guessRegisterCreateDto.ComId;
                perCreate.FullName = guessRegisterCreateDto.FullName;
                perCreate.Birthday = guessRegisterCreateDto.Birthday;
                perCreate.Gender = guessRegisterCreateDto.Gender;
                perCreate.PhoneNumber = guessRegisterCreateDto.PhoneNumber;
                perCreate.Email = guessRegisterCreateDto.Email;
                perCreate.Status = 1;
                perCreate.File1 = guessRegisterCreateDto.ImageUrl;
                perCreate.IsDelete = false;
                perCreate.PersonType = 2;
                perCreate.Passport = guessRegisterCreateDto.Passport;
                await _personService.CreatePerson(perCreate, false);

                perId = perCreate.PersonId;
                //Chuyển dữ liệu vào bảng face
                FaceCreateDto faceDto = new FaceCreateDto();
                faceDto.FaceId = Guid.NewGuid();
                faceDto.PersonId = perCreate.PersonId;
                faceDto.FacePath = guessRegisterCreateDto.ImageUrl;
                faceDto.Status = 1;
                await _faceService.CreateFace(faceDto);
            }
            else
            {
                //update person
                PersonUpdateDto perUpdate = new PersonUpdateDto();
                perUpdate.PersonId = perId.Value;
                perUpdate.CompId = guessRegisterCreateDto.ComId;
                perUpdate.FullName = guessRegisterCreateDto.FullName;
                perUpdate.Birthday = guessRegisterCreateDto.Birthday;
                perUpdate.Gender = guessRegisterCreateDto.Gender;
                perUpdate.PhoneNumber = guessRegisterCreateDto.PhoneNumber;
                perUpdate.Email = guessRegisterCreateDto.Email;
                perUpdate.Status = 1;
                perUpdate.File1 = guessRegisterCreateDto.ImageUrl;
                perUpdate.IsDelete = false;
                perUpdate.PersonType = 2;
                perUpdate.Passport = guessRegisterCreateDto.Passport;
                await _personService.UpdatePersonForGuess(perId.Value, perUpdate);

                var faceId = await _faceService.GetFaceByPersonId(perId.Value);
                //update face
                FaceUpdateDto faceUpdate = new FaceUpdateDto();
                faceUpdate.FaceId = faceId;
                faceUpdate.PersonId = perId.Value;
                faceUpdate.FacePath = guessRegisterCreateDto.ImageUrl;
                faceUpdate.Status = 1;
                await _faceService.UpdateFace(faceId, faceUpdate);
            }

            //Thêm danh sách thiết bị truy cập
            if (guessRegisterCreateDto.LstMachineId != null)
            {
                foreach (var item in guessRegisterCreateDto.LstMachineId)
                {
                    PersonsAccessCreateDto personsAccess = new PersonsAccessCreateDto();
                    personsAccess.PersonId = perId.Value;
                    personsAccess.MachineId = item;
                    personsAccess.FromDate = guessRegisterCreateDto.StartTime;
                    personsAccess.ToDate = guessRegisterCreateDto.EndTime;
                    personsAccess.IsDelete = false;
                    await _personsAccessService.CreatePersonsAccess(personsAccess);
                }
            }

            guessRegisterCreateDto.ID = 0;
            if (guessRegisterCreateDto.ComId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(guessRegisterCreateDto.ComId.Value, (int)_userIdentity.CompId);
            }
            var guessId = await _guessService.CreateGuess(guessRegisterCreateDto);

            await _guessService.ApproveGuess(guessId, guessRegisterCreateDto.Approve.Value, perId.Value);
            return Ok();
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewGuess, PrivilegeList.ManageGuess)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuessById(int id)
        {
            await _checkDataService.CheckGuess(id, (int)_userIdentity.CompId);
            var rs = await _guessService.GetGuessId(id);

            //Nếu có thông tin person thì lấy các thiết bị hiện được truy cập
            if (rs.PersonId.HasValue)
            {
                rs.LstMachineId = _personsAccessService.GetListAccessMachines(rs.PersonId.Value, rs.StartTime.Value, rs.EndTime.Value);
                rs.StrMachine = _personsAccessService.GetStrAccessMachines(rs.LstMachineId);
            }
            return Ok(rs);
        }

        [HttpPut("{id}/approve{approveValue}")]
        [CustomAuthorize(PrivilegeList.ManageGuess)]
        public async Task<IActionResult> Approve(int id, int approveValue)
        {
            await _checkDataService.CheckGuess(id, (int)_userIdentity.CompId);
            //Nếu duyệt thông tin
            if (approveValue == 1)
            {
                //lấy thông tin khách
                var guess = await _guessService.GetGuessId(id);

                //lấy perId
                var perId = _guessService.GetPersonId(guess.PhoneNumber.Trim());

                //ins dữ liệu nếu chưa có
                if (!perId.HasValue)
                {
                    //Nếu chưa có thông tin thì chuyển dữ liệu vào bảng person
                    PersonCreateDto perCreate = new PersonCreateDto();
                    perCreate.PersonId = Guid.NewGuid();
                    perCreate.CompId = guess.ComId;
                    perCreate.FullName = guess.FullName;
                    perCreate.Birthday = guess.Birthday;
                    perCreate.Gender = guess.Gender;
                    perCreate.PhoneNumber = guess.PhoneNumber;
                    perCreate.Email = guess.Email;
                    perCreate.Status = 1;
                    perCreate.File1 = guess.ImageUrl;
                    perCreate.IsDelete = false;
                    perCreate.PersonType = 2;
                    await _personService.CreatePerson(perCreate, false);

                    perId = perCreate.PersonId;
                    //Chuyển dữ liệu vào bảng face
                    FaceCreateDto faceDto = new FaceCreateDto();
                    faceDto.FaceId = Guid.NewGuid();
                    faceDto.PersonId = perCreate.PersonId;
                    faceDto.FacePath = guess.ImageUrl;
                    faceDto.Status = 1;
                    await _faceService.CreateFace(faceDto);
                }
                //update dữ liệu nếu đã tồn tại
                else
                {
                    //update person
                    PersonUpdateDto perUpdate = new PersonUpdateDto();
                    perUpdate.PersonId = perId.Value;
                    perUpdate.CompId = guess.ComId;
                    perUpdate.FullName = guess.FullName;
                    perUpdate.Birthday = guess.Birthday;
                    perUpdate.Gender = guess.Gender;
                    perUpdate.PhoneNumber = guess.PhoneNumber;
                    perUpdate.Email = guess.Email;
                    perUpdate.Status = 1;
                    perUpdate.File1 = guess.ImageUrl;
                    perUpdate.IsDelete = false;
                    perUpdate.PersonType = 2;
                    await _personService.UpdatePersonForGuess(perId.Value, perUpdate);

                    var faceId = await _faceService.GetFaceByPersonId(perId.Value);
                    //update face
                    FaceUpdateDto faceUpdate = new FaceUpdateDto();
                    faceUpdate.FaceId = faceId;
                    faceUpdate.PersonId = perId.Value;
                    faceUpdate.FacePath = guess.ImageUrl;
                    faceUpdate.Status = 1;
                    await _faceService.UpdateFace(faceId, faceUpdate);
                }
                await _guessService.ApproveGuess(id, approveValue, perId.Value);
            }
            else
            {
                //Không duyệt chỉ cập nhật lại trạng thái
                await _guessService.ApproveGuess(id, approveValue, null);
            }
            return Ok();
        }

        //Update phần quyền truy cập thiết bị
        [HttpPut("{id}")]
        [CustomAuthorize(PrivilegeList.ManageGuess)]
        public async Task<IActionResult> Update(int id, GuessUpdateDto guess)
        {
            await _checkDataService.CheckGuess(id, (int)_userIdentity.CompId);

            //lấy thông tin khách
            var gInfo = await _guessService.GetGuessId(id);

            //lấy perId
            var perId = _guessService.GetPersonId(gInfo.PhoneNumber.Trim());

            //ins dữ liệu nếu chưa có
            if (!perId.HasValue)
            {
                //Nếu chưa có thông tin thì chuyển dữ liệu vào bảng person
                PersonCreateDto perCreate = new PersonCreateDto();
                perCreate.PersonId = Guid.NewGuid();
                perCreate.CompId = gInfo.ComId;
                perCreate.FullName = gInfo.FullName;
                perCreate.Birthday = gInfo.Birthday;
                perCreate.Gender = gInfo.Gender;
                perCreate.PhoneNumber = gInfo.PhoneNumber;
                perCreate.Email = gInfo.Email;
                perCreate.Status = 1;
                perCreate.File1 = gInfo.ImageUrl;
                perCreate.IsDelete = false;
                perCreate.PersonType = 2;
                perCreate.Passport = gInfo.Passport;
                await _personService.CreatePerson(perCreate, false);

                perId = perCreate.PersonId;
                //Chuyển dữ liệu vào bảng face
                FaceCreateDto faceDto = new FaceCreateDto();
                faceDto.FaceId = Guid.NewGuid();
                faceDto.PersonId = perCreate.PersonId;
                faceDto.FacePath = gInfo.ImageUrl;
                faceDto.Status = 1;
                await _faceService.CreateFace(faceDto);
            }
            //update dữ liệu nếu đã tồn tại
            else
            {
                //update person
                PersonUpdateDto perUpdate = new PersonUpdateDto();
                perUpdate.PersonId = perId.Value;
                perUpdate.CompId = gInfo.ComId;
                perUpdate.FullName = gInfo.FullName;
                perUpdate.Birthday = gInfo.Birthday;
                perUpdate.Gender = gInfo.Gender;
                perUpdate.PhoneNumber = gInfo.PhoneNumber;
                perUpdate.Email = gInfo.Email;
                perUpdate.Status = 1;
                perUpdate.File1 = gInfo.ImageUrl;
                perUpdate.IsDelete = false;
                perUpdate.PersonType = 2;
                perUpdate.Passport = gInfo.Passport;
                await _personService.UpdatePersonForGuess(perId.Value, perUpdate);

                var faceId = await _faceService.GetFaceByPersonId(perId.Value);
                //update face
                FaceUpdateDto faceUpdate = new FaceUpdateDto();
                faceUpdate.FaceId = faceId;
                faceUpdate.PersonId = perId.Value;
                faceUpdate.FacePath = gInfo.ImageUrl;
                faceUpdate.Status = 1;
                await _faceService.UpdateFace(faceId, faceUpdate);
            }

            if (gInfo.PersonId.HasValue)
            {
                List<int> lstDeleteId = _personsAccessService.GetListPersonsAccessId(gInfo.PersonId.Value, gInfo.StartTime.Value, gInfo.EndTime.Value);
                //Xóa các thiết bị cũ
                await _personsAccessService.DeletePersonsAccess(lstDeleteId.ToArray());
            }

            //add thiết bị
            foreach (var item in guess.LstMachineId)
            {
                PersonsAccessCreateDto personsAccess = new PersonsAccessCreateDto();
                personsAccess.PersonId = perId.Value;
                personsAccess.MachineId = item;
                personsAccess.FromDate = gInfo.StartTime;
                personsAccess.ToDate = gInfo.EndTime;

                //Từ chối mặc định IsDelete = true
                if (guess.Approve.HasValue && guess.Approve == 2)
                    personsAccess.IsDelete = true;
                else
                    personsAccess.IsDelete = false;

                await _personsAccessService.CreatePersonsAccess(personsAccess);
            }

            if (!guess.Approve.HasValue)
                guess.Approve = 0;

            await _guessService.ApproveGuess(id, guess.Approve.Value, perId.Value);
            await _guessService.UpdateTimeGuess(id, guess);

            return Ok();
        }

        [HttpGet]
        [CustomAuthorize(PrivilegeList.ViewGuess, PrivilegeList.ManageGuess)]
        public async Task<IActionResult> GetListGuess([FromQuery] GuessGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _guessService.GetListGuess(pagingModel));
        }
    }
}
