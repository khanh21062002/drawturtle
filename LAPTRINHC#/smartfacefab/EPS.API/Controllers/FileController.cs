using EPS.API.Helpers;
using EPS.Data;
using EPS.Service;
using EPS.Service.Dtos.Face;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/file")]
    [Authorize]
    public class FileController : BaseController
    {
        private PersonService _personService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings;
        private FaceService _faceService;
        private IUserIdentity<int> _userIdentity;

        public FileController(PersonService personService, FaceService faceService, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings, IUserIdentity<int> userIdentity)
        {
            _personService = personService;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value;
            _faceService = faceService;
            _userIdentity = userIdentity;
        }

        [HttpPost]
        [RequestFormSizeLimit(valueCountLimit: int.MaxValue)]//Set số file được đẩy lên
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            try
            {
                //Get All person
                var lstPerson = _personService.GetAllPerson();

                List<UploadFaceDto> lstResult = new List<UploadFaceDto>();

                foreach (var item in files)
                {
                    var fileName = item.FileName;
                    string perCode = fileName.Split('.')[0].ToString();

                    //Check exits percode in the system
                    var isExting = lstPerson.Any(x => x.PersonCode == perCode);

                    //if exits then upload avatar
                    if (isExting)
                    {
                        UploadFaceDto uploadFaceDto = new UploadFaceDto();
                        if (_userIdentity == null || _userIdentity.CompId == null)
                        {
                            throw new EPSException("CompanyService.Message.UserPrivileges");
                        }
                        //get per
                        var per = lstPerson.Where(x => x.PersonCode == perCode && x.CompId == _userIdentity.CompId).FirstOrDefault();

                        if (per == null)
                        {
                            throw new EPSException("AuthorizationService.Message.UserPrivileges2   ");
                        }

                        if (_userIdentity.CompId != per.CompId)
                        {
                            throw new EPSException("CompanyService.Message.UserPrivileges");
                        }


                        //get path
                        var perId = per.PersonId;

                        string contentRootPath = _webHostEnvironment.ContentRootPath;

                        var rootPath = System.IO.Directory.GetParent(contentRootPath);

                        string uploadFolder = _appSettings.UploadPersonImgFolder + per.CompId + "\\";

                        string path = Path.Combine(rootPath.FullName, uploadFolder);

                        //check exits folder
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        var faceId = Guid.NewGuid();
                        var filename = faceId + "." + fileName.Split('.')[1].ToString();

                        var fullPath = Path.Combine(path, filename);

                        using (FileStream output = System.IO.File.Create(fullPath))
                            await item.CopyToAsync(output);

                        var AvatarPath = "img\\faces\\" + per.CompId + "\\" + filename;

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

                        //Add to list result
                        uploadFaceDto.FilePath = AvatarPath;
                        uploadFaceDto.OriginalName = filename;
                        uploadFaceDto.IsSuccess = true;
                        lstResult.Add(uploadFaceDto);
                    }
                }

                return Ok(lstResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
