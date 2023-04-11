using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Machine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using EPS.Service.Models;
using Microsoft.AspNetCore.Hosting;
using EPS.Utils.Repository.Audit;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/machine")]
    [Authorize]

    public class MachineController : BaseController
    {
        private MachineService _machineService;
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public MachineController(MachineService machineService, IOptions<AppSettings> appSettings, IWebHostEnvironment webHostEnvironment, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _machineService = machineService;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpGet]
        public async Task<IActionResult> GetListMachines([FromQuery] MachineGridPagingDto pagingModel)
        {
            
            return Ok(await _machineService.GetMachines(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageMachine)]
        [HttpPost]
        public async Task<IActionResult> Create(MachineCreateDto machineCreateDto)
        {
            machineCreateDto.Volume = 100;

            if (machineCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(machineCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }

            if (machineCreateDto.FileData == null)
            {
                await _machineService.CreateMachine(machineCreateDto);
            }
            else
            {
                byte[] newBytes = Convert.FromBase64String(machineCreateDto.FileData);


                var _file = newBytes;
                if (_file.Length > 0)
                {
                    var filename = machineCreateDto.File1.Trim('"');

                    string contentRootPath = _webHostEnvironment.ContentRootPath;

                    var rootPath = System.IO.Directory.GetParent(contentRootPath);

                    string uploadFolder = _appSettings.UploadPersonImgFolder + machineCreateDto.CompId + "\\";                   

                    string path = Path.Combine(rootPath.FullName, uploadFolder);

                    //check folder tồn tại hay chưa
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fullPath = Path.Combine(path, filename);

                    System.IO.File.WriteAllBytes(fullPath, newBytes);

                    machineCreateDto.Logo = "img\\faces\\" + machineCreateDto.CompId + "\\"+  filename;
                    machineCreateDto.FileData = "";
                    return Ok(await _machineService.CreateMachine(machineCreateDto));
                }          
            }

            return Ok();
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMachineById(int id)
        {
            await _checkDataService.CheckMachine(id, (int)_userIdentity.CompId);
            return Ok(await _machineService.GetMachineById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageMachine)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, MachineUpdateDto machineUpdateDto)
        {
            machineUpdateDto.Volume = 100;
            await _checkDataService.CheckMachine(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckMachine(id, (int)machineUpdateDto.CompId);
            if (machineUpdateDto.FileData == null)
            {
                await _machineService.UpdateMachine(id, machineUpdateDto);
            }
            else
            {
                byte[] newBytes = Convert.FromBase64String(machineUpdateDto.FileData);


                var _file = newBytes;
                if (_file.Length > 0)
                {
                    var filename = machineUpdateDto.File1.Trim('"');

                    string contentRootPath = _webHostEnvironment.ContentRootPath;

                    var rootPath = System.IO.Directory.GetParent(contentRootPath);

                    string uploadFolder = _appSettings.UploadPersonImgFolder + machineUpdateDto.CompId + "\\";

                    string path = Path.Combine(rootPath.FullName, uploadFolder);

                    //check folder tồn tại hay chưa
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fullPath = Path.Combine(path, filename);

                    System.IO.File.WriteAllBytes(fullPath, newBytes);

                    machineUpdateDto.Logo = "img\\faces\\" + machineUpdateDto.CompId + "\\" + filename;
                    machineUpdateDto.FileData = "";
                    await _machineService.UpdateMachine(id, machineUpdateDto);
                }
            }
            return Ok();
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageMachine)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckMachine(id, (int)_userIdentity.CompId);
            return Ok(await _machineService.UpdateDelete1(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageMachine)]
        [HttpDelete]
        public async Task<IActionResult> DeleteMachines(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var Machines = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _machineService.UpdateDeletes1(Machines));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [CustomAuthorize(PrivilegeList.ManageMachine)]
        [HttpGet("{checkcode}/{id}/{compid}/{code}")]
        public async Task<IActionResult> CheckExistCode(int id, int compid, string code)
        {
            return Ok(await _machineService.CheckExistCode(id, compid, code));
        }

        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpGet("synchronized/{id}")]
        public async Task<IActionResult> CreateSynchronizeDataToMachine(int id)
        {
            await _checkDataService.CheckMachine(id, (int)_userIdentity.CompId);
            return Ok(await _machineService.CreateSynchronizeDataToMachine(id));
        }
        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpGet("request/{id}")]
        public async Task<IActionResult> RequestRebootToMachine(int id)
        {
            await _checkDataService.CheckMachine(id, (int)_userIdentity.CompId);
            return Ok(await _machineService.RequestRebootToMachine(id));
        }
    }
}
