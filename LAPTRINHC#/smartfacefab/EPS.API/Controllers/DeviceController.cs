
using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.CamViewConfig;
using EPS.Service.Dtos.Device;
//using EPS.Service.Dtos.StreamVideo;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Device")]
    [Authorize]
    public class DeviceController : BaseController
    {
        private DeviceService _DeviceSevice;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings;
        private IUserIdentity<int> _userIdentity;
        private readonly IHubContext<EventHub> _hubContext;
        private IMemoryCache _cache;

        public DeviceController(DeviceService DeviceService, IUserIdentity<int> userIdentity, IMemoryCache cache, IHubContext<EventHub> hubContext, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings)
        {
            _DeviceSevice = DeviceService;
            _userIdentity = userIdentity;
            _cache = cache;
            _hubContext = hubContext;
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value;
        }

        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpGet]
        public async Task<IActionResult> GetListDevice([FromQuery] DeviceGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return Ok(await _DeviceSevice.GetListDevice(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpPost]
        public async Task<IActionResult> Create(DeviceCreateDto createDto)
        {
            return Ok(await _DeviceSevice.AddDevice(createDto));
        }

        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceById(int Id)
        {
            return Ok(await _DeviceSevice.GetById(Id));
        }

        [CustomAuthorize(PrivilegeList.ViewMachine, PrivilegeList.ManageMachine)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, DeviceUpdateDto updateDto)
        {
            return Ok(await _DeviceSevice.UpdateDevice(id, updateDto));
        }

        [HttpPost("polygon")]
        public async Task<IActionResult> CreatePolygon(PolygonCreateDto createDto)
        {
            return Ok(await _DeviceSevice.CreatePolygon(createDto));
        }

        [HttpDelete("polygon/{id}")]
        public async Task<IActionResult> Deletepolygon(int id)
        {
            return Ok(await _DeviceSevice.UpdateDeletePolygon(id));
        }

        [HttpGet("polygon/{camPolygonId}/{type}")]
        public async Task<IActionResult> GetPolygonByCamId(int camPolygonId, int type)
        {
            return Ok(await _DeviceSevice.GetPolygonByCamId(camPolygonId, type));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageMachine)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _DeviceSevice.UpdateDelete(id));
        }

        [HttpGet("camViewConfigByUser")]
        public IActionResult GetDeviceByCamViewConfig()
        {
            return Ok(_DeviceSevice.GetDeviceByCamViewConfig());
        }

        [HttpGet("getIdViewConfigByUser/{position}")]
        public IActionResult GetIdCamViewConfig(int position)
        {
            return Ok(_DeviceSevice.GetIdCamViewConfig(position));
        }

        [HttpPost("createCamViewConfigByUser")]
        public async Task<IActionResult> CreateCamViewConfig(CamViewConfigUpdateDto updateDto)
        {
            return Ok(await _DeviceSevice.CreateCamViewConfig(updateDto));
        }

        [HttpPut("updateCamViewConfigByUser/{id}")]
        public async Task<IActionResult> UpdateCamViewConfig(int id, CamViewConfigUpdateDto updateDto)
        {
            return Ok(await _DeviceSevice.UpdateCamViewConfig(id, updateDto));
        }

        [HttpGet("getDeviceOnCam/{id}")]
        public async Task<IActionResult> GetByDeviceIdOnCamViewConfig(int Id)
        {
            return Ok(await _DeviceSevice.GetById(Id));
        }
    }
}
