using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.AppParam;
using EPS.Service.Dtos.Event;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using EPS.Service.Dtos.Face;
using System.IO;
using EPS.Service.Dtos.Person;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/publicApi")]
    [Authorize]

    public class ApiController : BaseController
    {
        private ApiService _apiService;

        private IUserIdentity<int> _userIdentity;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings;

        private PersonService _personService;
        private FaceService _faceService;

        public ApiController(ApiService apiService, PersonService personService, IUserIdentity<int> userIdentity, IOptions<AppSettings> appSettings, IWebHostEnvironment webHostEnvironment, FaceService faceService)
        {
            _apiService = apiService;
            _userIdentity = userIdentity;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value;
            _personService = personService;
            _faceService = faceService;
        }

        //get event by sync date time
        [HttpGet("getAllEvents")]
        public async Task<IActionResult> GetListAllDevices([FromQuery] EventGridPagingDto filterDto)
        {
            var compId = _userIdentity.CompId;
            filterDto.compId = compId;
            return Ok(await _apiService.GetEvents(filterDto));
        }


        //list all
        [HttpGet("getAllStudentEvents")]
        public async Task<IActionResult> GetListEventsStudent([FromQuery] EventGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _apiService.GetEvents(pagingModel));
        }


        //list all
        [HttpPut("updateAvatar")]
        public async Task<IActionResult> UpdateAvatar(AvatarUpdateDto avatarUpdateDto)
        {
            const long MAX_FILE_SIZE = 1048576 * 5;
            ApiResponseDto response = new ApiResponseDto();
            try
            {
                string personCode = avatarUpdateDto.personCode;
                string avatar = avatarUpdateDto.avatar;

                //Get All person
                var lstPerson = _personService.GetAllPerson();

                List<UploadFaceDto> lstResult = new List<UploadFaceDto>();

                //Check exits percode in the system
                var isExting = lstPerson.Any(x => x.PersonCode == personCode);

                //if exits then upload avatar
                if (isExting)
                {

                    if (_userIdentity == null || _userIdentity.CompId == null)
                    {
                        return Ok(ReturnErrorApi("403", "Vui lòng đăng nhập lại"));
                    }
                    //get per
                    var per = lstPerson.Where(x => x.PersonCode == personCode && x.CompId == _userIdentity.CompId).FirstOrDefault();

                    if (per == null)
                    {
                        return Ok(ReturnErrorApi("500", "Không tìm thấy mã nhân sự/ học sinh"));
                    }

                    if (_userIdentity.CompId != per.CompId)
                    {
                        return Ok(ReturnErrorApi("500", "Không tìm thấy mã nhân sự/ học sinh"));
                    }


                    byte[] newBytes = Convert.FromBase64String(avatar);
                    var _file = newBytes;
                    if (_file.Length > 0)
                    {
                        if (_file.Length >= MAX_FILE_SIZE)
                        {
                            return Ok(ReturnErrorApi("500", "Vui lòng upload ảnh dưới 5MB"));
                        }
                        string contentRootPath = _webHostEnvironment.ContentRootPath;

                        var rootPath = System.IO.Directory.GetParent(contentRootPath);

                        string uploadFolder = _appSettings.UploadPersonImgFolder + _userIdentity.CompId + "\\";

                        string path = Path.Combine(rootPath.FullName, uploadFolder);

                        //delete old image when update
                        string path1 = Directory.GetParent(path).ToString();
                        string path2 = Directory.GetParent(path1).ToString();
                        string path3 = Directory.GetParent(path2).ToString();
                        string path4 = Directory.GetParent(path3).ToString();
                        var oldPerson = per;
                        string deleteFilePath = "";
                        if (oldPerson != null)
                        {
                            deleteFilePath = oldPerson.File1;
                        }
                        string pathDelete = path4 + "\\" + deleteFilePath;

                        if (System.IO.File.Exists(pathDelete))
                        {
                            try
                            {
                                System.IO.File.Delete(pathDelete);
                            }
                            catch (Exception)
                            {
                                //Do something
                            }
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
                        var AvatarPath = "img\\faces\\" + _userIdentity.CompId + "\\" + filename;
                        var perId = per.PersonId;
                        var face = await _personService.GetFaceByPersonId(perId);
                        if (face == null)
                        {
                            FaceCreateDto crFace = new FaceCreateDto();

                            crFace.PersonId = perId;
                            crFace.FaceId = new Guid();
                            crFace.FacePath = AvatarPath;
                            crFace.Status = 1;
                            await _personService.CreatePersonDetail(crFace);
                            await _personService.UpdateAvatar(perId, AvatarPath);
                        }
                        else
                        {
                            face.FacePath = AvatarPath;
                            await _faceService.UpdateFace(face.FaceId, face);
                            await _personService.UpdateAvatar(perId, AvatarPath);
                        }

                        response.HttpStatus = "200";
                        response.ApiStatus = "1";
                        response.ResponseMessage = "Update thành công";
                        response.Data = "";
                        return Ok(response);
                    }
                    else
                    {
                        return Ok(ReturnErrorApi("500", "Vui lòng upload ảnh đúng định dạng base64"));
                    }
                }
                else
                {
                    return Ok(ReturnErrorApi("500", "Không tìm thấy mã nhân sự/ học sinh"));
                }
            }



            catch (Exception ex)
            {
                return Ok(ReturnErrorApi("500", "Lỗi Exception: " + ex.Message));
            }


            return Ok(response);
        }


        private ApiResponseDto ReturnErrorApi(string httpCode, string errorMsg)
        {
            ApiResponseDto response = new ApiResponseDto();
            response.HttpStatus = httpCode;
            response.ApiStatus = "0";
            response.ResponseMessage = errorMsg;
            response.Data = "";

            return response;
        }
    }
}
