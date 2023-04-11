using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EPS.Utils.Service;

namespace EPS.Service
{
    public class HomeService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IOptions<AppSettings> _settings;
        private readonly ILogger<EventService> _logger;
        private IUserIdentity<int> _userIdentity;

        public HomeService(EPSRepository repository, IMapper mapper, IOptions<AppSettings> settings, ILogger<EventService> logger, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _settings = settings;
            _logger = logger;
            _userIdentity = userIdentity;
        }

        public List<HomePersonInfor> GetPersonInfor()
        {
            var compId = _userIdentity.CompId ?? 0;
            string sqlQuery = " EXEC [dbo].[HomePersonInfor] "
            + " @CompId = " + compId;

            IQueryable<HomePersonInfor> query = _repository.FromSqlRaw<HomePersonInfor>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.ToList<HomePersonInfor>();
            return result;
        }

        public PagingResult<HomeTopEmployeeInOut> GetTopEmpInOut(int options)
        {
            int compId = _userIdentity.CompId ?? 0;

            string sqlQuery = " EXEC HomeTopEmployeeInOut "
                + " @CompId = " + compId + ", "
                + " @NumberMinus = " + options;

            IQueryable<HomeTopEmployeeInOut> query = _repository.FromSqlRaw<HomeTopEmployeeInOut>(sqlQuery);
            var exData = query.AsEnumerable();
            PagingParams<HomeTopEmployeeInOut> pagingModel = new PagingParams<HomeTopEmployeeInOut>();
            var result = new PagingResult<HomeTopEmployeeInOut>() { PageSize = pagingModel.ItemsPerPage, CurrentPage = pagingModel.Page };

            result.TotalRows = exData.Count();

            if (pagingModel.StartingIndex > 0)
            {
                exData = exData.Skip(pagingModel.StartingIndex);
            }
            if (pagingModel.ItemsPerPage != -1 && pagingModel.ItemsPerPage <= 0)
            {
                pagingModel.ItemsPerPage = 100;
            }
            if (pagingModel.ItemsPerPage > 0)
            {
                exData = exData.Take(pagingModel.ItemsPerPage);
            }
            result.Data = exData.ToList();
            return result;
        }

        public List<HomeStatisticPersonInOut> GetStatisticPersonInOut(int val)
        {
            var compId = _userIdentity.CompId ?? 0;
            string sqlQuery = " EXEC [dbo].[HomeStatisticPersonInOut] "
            + " @CompId = " + compId + ","
            + " @Numb = " + val;

            IQueryable<HomeStatisticPersonInOut> query = _repository.FromSqlRaw<HomeStatisticPersonInOut>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.ToList<HomeStatisticPersonInOut>();
            return result;
        }

        public List<HomeStatisticUnregistered> GetStatisticUnregistered(int val)
        {
            var compId = _userIdentity.CompId ?? 0;
            string sqlQuery = " EXEC [dbo].[HomeStatisticUnregistered] "
            + " @CompId = " + compId + ","
            + " @Numb = " + val;

            IQueryable<HomeStatisticUnregistered> query = _repository.FromSqlRaw<HomeStatisticUnregistered>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.ToList<HomeStatisticUnregistered>();
            return result;
        }

        public List<HomeCustomerComeStore> GetCustomerComeStore(int compId, DateTime date)
        {
            string sqlQuery = " EXEC [dbo].[HomeCustomerComeStore] "
                                + " @DateCount = '" + date.ToString("yyyy-MM-dd") + "',"
                                + " @CompId = " + compId;
            IQueryable<HomeCustomerComeStore> query = _repository.FromSqlRaw<HomeCustomerComeStore>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.ToList<HomeCustomerComeStore>();
            return result;
        }

        public List<HomeChartCustomerByDay> GetChartCustomerByDay(DateTime dateFrom, DateTime dateTo)
        {
            var compId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            string sqlQuery = " EXEC [dbo].[HomeChartCustomerByDay] "
                                + " @DateFrom = '" + dateFrom.ToString("yyyy-MM-dd") + "',"
                                + " @DateTo = '" + dateTo.ToString("yyyy-MM-dd") + "',"
                                + " @CompId = " + compId;
            IQueryable<HomeChartCustomerByDay> query = _repository.FromSqlRaw<HomeChartCustomerByDay>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.ToList<HomeChartCustomerByDay>();
            return result;
        }

        public HomePieCustomerByDay GetPieCustomerByDay(DateTime dateFrom, DateTime dateTo)
        {
            var compId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            string sqlQuery = " EXEC [dbo].[HomePieCustomerByDay] "
                                + " @DateFrom = '" + dateFrom.ToString("yyyy-MM-dd") + "',"
                                + " @DateTo = '" + dateTo.ToString("yyyy-MM-dd") + "',"
                                + " @CompId = " + compId;
            IQueryable<HomePieCustomerByDay> query = _repository.FromSqlRaw<HomePieCustomerByDay>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.FirstOrDefault();
            return result;
        }
        public async Task<List<HomeChartDay>> GetHome2(int date)
        {
            var compId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            string sqlQuery = " EXEC [dbo].[HomeChartDay] "
            + " @CompId = " + compId + ","
            + " @DateSearch = " + date;

            IQueryable<HomeChartDay> query = _repository.FromSqlRaw<HomeChartDay>(sqlQuery);
            var exData = query.AsEnumerable();
            var result = exData.ToList<HomeChartDay>();
            return result;
        }
        public async Task<PagingResult<HomePieByDay>> GetHome3(int date)
        {
            var compId = _userIdentity.CompId.HasValue ? _userIdentity.CompId.Value : 0;
            string sqlQuery = " EXEC [dbo].[HomePieByDay] "
            + " @CompId = " + compId + ","
            + " @DateSearch = " + date;

            IQueryable<HomePieByDay> query = _repository.FromSqlRaw<HomePieByDay>(sqlQuery);
            var exData = query.AsEnumerable();
            PagingParams<HomePieByDay> pagingModel = new PagingParams<HomePieByDay>();
            var result = new PagingResult<HomePieByDay>() { PageSize = pagingModel.ItemsPerPage, CurrentPage = pagingModel.Page };
            result.TotalRows = exData.Count();

            if (pagingModel.StartingIndex > 0)
            {
                exData = exData.Skip(pagingModel.StartingIndex);
            }
            if (pagingModel.ItemsPerPage != -1 && pagingModel.ItemsPerPage <= 0)
            {
                pagingModel.ItemsPerPage = 100;
            }
            if (pagingModel.ItemsPerPage > 0)
            {
                exData = exData.Take(pagingModel.ItemsPerPage);
            }
            result.Data = exData.ToList();
            return result;
        }
    }
}
