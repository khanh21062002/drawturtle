using EPS.API.Helpers;
using EPS.Service;
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
    [Route("api/home")]
    [Authorize]

    public class HomeController : BaseController
    {
        private HomeService _homeService;
        private IUserIdentity<int> _userIdentity;

        public HomeController(HomeService homeService, IUserIdentity<int> userIdentity)
        {
            _homeService = homeService;
            _userIdentity = userIdentity;
        }

        [HttpGet("personInfor")]
        public IActionResult GetPersonInfor()
        {
            return Ok(_homeService.GetPersonInfor());
        }

        [HttpGet("topEmpInOut/{options}")]
        public IActionResult GetTopEmpInOut(int options)
        {
            return Ok(_homeService.GetTopEmpInOut(options));
        }

        [HttpGet("statisticPersonInOut/{val}")]
        public IActionResult GetStatisticPersonInOut(int val)
        {
            return Ok(_homeService.GetStatisticPersonInOut(val));
        }

        [HttpGet("statisticUnregistered/{val}")]
        public IActionResult GetStatisticUnregistered(int val)
        {
            return Ok(_homeService.GetStatisticUnregistered(val));
        }

        [HttpGet("chartGuestByHour/{compId}/{date}")]
        public IActionResult GetCustomerComeStore(int compId, DateTime date)
        {
            return Ok(_homeService.GetCustomerComeStore(compId, date));
        }

        [HttpGet("chartGuestByDay/{dateFrom}/{dateTo}")]
        public IActionResult GetChartCustomerByDay(DateTime dateFrom, DateTime dateTo)
        {
            return Ok(_homeService.GetChartCustomerByDay(dateFrom, dateTo));
        }

        [HttpGet("pieGuestByDay/{dateFrom}/{dateTo}")]
        public IActionResult GetPieCustomerByDay(DateTime dateFrom, DateTime dateTo)
        {
            return Ok(_homeService.GetPieCustomerByDay(dateFrom, dateTo));
        }
        [HttpGet("home2/{date}")]
        public async Task<IActionResult> GetHome2(int date)
        {
            return Ok(await _homeService.GetHome2(date));
        }
        [HttpGet("home3/{date}")]
        public async Task<IActionResult> GetHome3(int date)
        {
            return Ok(await _homeService.GetHome3(date));
        }
    }
}
