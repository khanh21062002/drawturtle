using EPS.API.Helpers;
using EPS.Service;
using EPS.Service.Dtos.NotificationSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/notificationSystem")]
    [Authorize]
    public class NotificationSystemController : BaseController
    {
        private NotificationSystemService _notificationSystemService;

        public NotificationSystemController(NotificationSystemService notificationSystemService)
        {
            _notificationSystemService = notificationSystemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId([FromQuery] NotificationSystemPagingDto pagingModel)
        {
            return Ok(await _notificationSystemService.GetByUserId(pagingModel));
        }

        //danh sách sản phẩm đã xuất kho
        [HttpGet("count")]
        public async Task<IActionResult> NotificationCount()
        {
            return Ok(await _notificationSystemService.NotificationCount(UserIdentity.UserId));
        }

        //detail
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            return Ok(await _notificationSystemService.GetNotificationById(id));
        }

        //update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationStatus(int id)
        {
            return Ok(await _notificationSystemService.UpdateNotificationStatus(id));
        }
    }
}
