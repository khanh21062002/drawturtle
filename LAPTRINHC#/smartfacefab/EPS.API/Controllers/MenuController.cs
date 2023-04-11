using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Menu;
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
    [Route("api/menu")]

    public class MenuController : BaseController
    {
        private MenuService _menuService;
        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [CustomAuthorize(PrivilegeList.ViewMenu, PrivilegeList.ManageMenu)]
        [HttpGet]
        public async Task<IActionResult> GetMenus([FromQuery] MenuGridPagingDto pagingModel)
        {
            return Ok(await _menuService.GetMenus(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.ManageMenu)]
        [HttpPost]
        public async Task<IActionResult> Create(MenuCreateDto menuCreateDto)
        {
            return Ok(await _menuService.CreateMenu(menuCreateDto));
        }

        [CustomAuthorize(PrivilegeList.ViewMenu, PrivilegeList.ManageMenu)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _menuService.GetMenuById(id));
        }

        [CustomAuthorize(PrivilegeList.ManageMenu)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuUpdateDto menuUpdateDto)
        {
            return Ok(await _menuService.UpdateMenu(id, menuUpdateDto));
        }

        [CustomAuthorize(PrivilegeList.ManageMenu)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _menuService.DeleteMenu(id));
        }
    }
}
