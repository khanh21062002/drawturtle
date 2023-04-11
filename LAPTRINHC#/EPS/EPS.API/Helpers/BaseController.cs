using AutoMapper;
using EPS.Data;
using EPS.Service;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Helpers
{
    [ApiController]
    public class BaseController : Controller
    {
        private Lazier<IUserIdentity<int>> _userIdentity;
        protected IUserIdentity<int> UserIdentity
        {
            get
            {
                if (_userIdentity == null) _userIdentity = new Lazier<IUserIdentity<int>>(HttpContext.RequestServices);

                return _userIdentity.Value;
            }
        }

        public BaseController()
        {
        }
    }
}
