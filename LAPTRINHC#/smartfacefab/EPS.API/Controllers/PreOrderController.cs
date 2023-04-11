using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.PreOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/preOrder")]

    public class PreOrderController : BaseController
    {
        private PreOrderService _preOrderService;
        public PreOrderController(PreOrderService preOrderService)
        {
            _preOrderService = preOrderService;
        }

        [CustomAuthorize(PrivilegeList.ViewPreOrder, PrivilegeList.ManagePreOrder)]
        [HttpGet]
        public async Task<IActionResult> GetPreOrders([FromQuery] PreOrderGridPagingDto pagingModel)
        {
            return Ok(await _preOrderService.GetPreOrders(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.ManagePreOrder)]
        [HttpPost]
        public async Task<IActionResult> Create(PreOrderCreateDto preOrderCreateDto)
        {
            return Ok(await _preOrderService.CreatePreOrder(preOrderCreateDto));
        }

        [CustomAuthorize(PrivilegeList.ViewPreOrder, PrivilegeList.ManagePreOrder)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _preOrderService.GetPreOrderById(id));
        }

        [CustomAuthorize(PrivilegeList.ManagePreOrder)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PreOrderUpdateDto preOrderUpdateDto)
        {
            return Ok(await _preOrderService.UpdatePreOrder(id, preOrderUpdateDto));
        }

        [CustomAuthorize(PrivilegeList.ManagePreOrder)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _preOrderService.DeletePreOrder(id));
        }

        [HttpGet("getInfoByPhone/{phoneNumber}")]
        public IActionResult GetInfoByPhone(string phoneNumber)
        {
            return Ok(_preOrderService.GetInfoByPhone(phoneNumber));
        }
    }
}
