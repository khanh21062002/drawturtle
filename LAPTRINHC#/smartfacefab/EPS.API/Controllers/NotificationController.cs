using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Notification;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/notification")]
    [Authorize]
    public class NotificationController : BaseController
    {
        private NotificationService _notificationService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;

        public NotificationController(NotificationService notificationService, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _notificationService = notificationService;
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewNotification, PrivilegeList.ManageNotification)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] NotificationGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            return Ok(await _notificationService.GetAll(pagingModel));
        }

        //create - thêm cảnh báo
        [CustomAuthorize(PrivilegeList.ManageNotification)]
        [HttpPost]
        public async Task<IActionResult> Create(NotificationCreateDto warehouseReceiptCreateDto)
        {
            if (warehouseReceiptCreateDto.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(warehouseReceiptCreateDto.CompId.Value, (int)_userIdentity.CompId);
            }
            var id = await _notificationService.Create(warehouseReceiptCreateDto);
            foreach (var item in warehouseReceiptCreateDto.NotificationDetails)
            {
                item.NotiId = id;
                await _notificationService.CreateNotiDetail(item);
            }
            return Ok(id);
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageNotification)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _checkDataService.CheckNotification(id, (int)_userIdentity.CompId);
            return Ok(await _notificationService.UpdateDelete(id));
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewNotification, PrivilegeList.ManageNotification)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            //await _checkDataService.CheckNotification(id, (int)_userIdentity.CompId);
            return Ok(await _notificationService.GetDetails(id));
        }

        //danh sách chi tiết cảnh báo 
        [HttpGet("getListNotiDetails/{notiId}")]
        public async Task<IActionResult> GetListNotiDetailsByNotiID(int notiId)
        {
            await _checkDataService.CheckNotification(notiId, (int)_userIdentity.CompId);
            return Ok(await _notificationService.GetListNotiDetails(notiId));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageNotification)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, NotificationUpdateDto warehouseReceiptUpdateDto)
        {
            await _checkDataService.CheckNotification(id, (int)_userIdentity.CompId);
            await _checkDataService.CheckNotification(id, (int)warehouseReceiptUpdateDto.CompId);
            return Ok(await _notificationService.UpdateById(id, warehouseReceiptUpdateDto));
        }

    }
}
