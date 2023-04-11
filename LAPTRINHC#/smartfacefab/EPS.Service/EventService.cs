using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos;
using EPS.Service.Dtos.Company;
using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.PreOrder;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class EventService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IOptions<AppSettings> _settings;
        private readonly ILogger<EventService> _logger;
        private IUserIdentity<int> _userIdentity;

        public EventService(EPSRepository repository, IMapper mapper, IOptions<AppSettings> settings, ILogger<EventService> logger, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _settings = settings;
            _logger = logger;
            _userIdentity = userIdentity;
        }

        public async Task<PagingResult<EventGridDto>> GetEvents(EventGridPagingDto pagingModel)
        {
            //int? curComId = _userIdentity.CompId;
            //if (curComId.HasValue)
            //{
            //    var curCompany = await _baseService.FindAsync<Company, CompanyDetailDto>(company => company.Id == curComId.Value);
            //    if (curCompany != null)
            //    {
            //        string hiddenParentField = curCompany.HiddenParentField;
            //        pagingModel.HiddenParentField = hiddenParentField;
            //    }
            //}
            var result = await _baseService.FilterPagedAsync<View_ListEvent, EventGridDto>(pagingModel);
            foreach (var item in result.Data)
            {
                item.getFaceUrl = _settings.Value.ApiUrl + "img/faces/" + item.CompId + "/" + item.EventId + ".jpg";
            }
            return result;
        }

        public async Task<EventGridDto> GetRealTime(Guid id)
        {
            var dto = await _baseService.FindAsync<View_ListEvent, EventGridDto>(x => x.EventId == id);
            dto.getFaceUrl = _settings.Value.ApiUrl + "img/faces/" + dto.CompId + "/" + dto.EventId + ".jpg";
            if (dto.CompId == _userIdentity.CompId)
            {
                return dto;
            }
            else
            {
                return null;
            }
        }

        public async Task<EventGridDto> GetRealTime(Guid id, string username)
        {
            var dto = await _baseService.FindAsync<View_ListEvent, EventGridDto>(x => x.EventId == id);
            var dtoUser = _repository.Filter<User>(x => x.UserName == username).FirstOrDefault();
            dto.getFaceUrl = _settings.Value.ApiUrl + "img/faces/" + dto.CompId + "/" + dto.EventId + ".jpg";
            if (dtoUser == null)
            {
                return null;
            }

            if (dto.CompId == dtoUser.CompId)
            {
                return dto;
            }
            else
            {
                return null;
            }
        }

        public async Task<PagingResult<EventGridDto>> Get5EventNewest(EventGridPagingDto pagingModel)
        {
            pagingModel.SortBy = "AccessTime";
            pagingModel.SortDesc = true;
            pagingModel.ItemsPerPage = 10;
            pagingModel.personType = 1;

            var result = await _baseService.FilterPagedAsync<View_ListEvent_RealTime, EventGridDto>(pagingModel);

            foreach (var item in result.Data)
            {
                item.getFaceUrl = _settings.Value.ApiUrl + "img/faces/" + item.CompId + "/" + item.EventId + ".jpg";
            }
            return result;
        }

        public async Task<PagingResult<EventGridDto>> ListGuessRealTime(EventGridPagingDto pagingModel)
        {
            pagingModel.SortBy = "AccessTime";
            pagingModel.SortDesc = true;
            pagingModel.ItemsPerPage = 10;
            pagingModel.personType = 2;

            var result = await _baseService.FilterPagedAsync<View_ListEvent_RealTime, EventGridDto>(pagingModel);

            foreach (var item in result.Data)
            {
                item.getFaceUrl = _settings.Value.ApiUrl + "img/faces/" + item.CompId + "/" + item.EventId + ".jpg";
            }
            return result;
        }

        public async Task<PagingResult<EventGridDto>> GetEventBlackListNewest(EventGridPagingDto pagingModel)
        {
            pagingModel.SortBy = "AccessTime";
            pagingModel.SortDesc = true;
            pagingModel.ItemsPerPage = 10;
            pagingModel.personType = 4;

            var result = await _baseService.FilterPagedAsync<View_ListEvent_RealTime, EventGridDto>(pagingModel);

            foreach (var item in result.Data)
            {
                item.getFaceUrl = _settings.Value.ApiUrl + "img/faces/" + item.CompId + "/" + item.EventId + ".jpg";
            }
            return result;
        }

        public async Task<PagingResult<EventGridDto>> GetEventPreOrderNewest(EventGridPagingDto pagingModel)
        {
            pagingModel.SortBy = "AccessTime";
            pagingModel.SortDesc = true;
            pagingModel.ItemsPerPage = 10;
            pagingModel.filterDateOrderTo = DateTime.Now;

            var result = await _baseService.FilterPagedAsync<View_ListEvent_RealTime, EventGridDto>(pagingModel);
            foreach (var item in result.Data)
            {
                item.getFaceUrl = _settings.Value.ApiUrl + "img/faces/" + item.CompId + "/" + item.EventId + ".jpg";
            }
            return result;
        }

        public async Task<int> CountOfPerson(Guid perId)
        {
            var countNumber = await _repository.FindAsync<View_Person_NumberOfTimes>(x => x.PersonId == perId);
            return countNumber.NumberOfTimes.Value;
        }

        public List<DateTime> ListTimeDetailPerson(Guid perId)
        {
            var listTimeFormat = new List<DateTime>();
            var listTime = _repository.Filter<CustomerEvent>(x => x.PersonId == perId).OrderByDescending(x => x.AccessTime).Select(x => x.AccessTime).Take(10).ToList();
            for (var i = 0; i < listTime.Count; i++)
            {
                var t = listTime[i].Value;
                listTimeFormat.Add(t);
            }
            return listTimeFormat;
        }

        //create event
        public async Task<int> CreateEvent(EventCreateDto eventCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<Event, EventCreateDto>(eventCreate);
            return 0;
        }

        public async Task<int> CreateCustomerEvent(CustomerEventCreateDto eventCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<CustomerEvent, CustomerEventCreateDto>(eventCreate);
            return 0;
        }

        public async Task<int> UpdateCustomerEvent(int id, CustomerEventUpdateDto eventUpdate)
        {
            return await _baseService.UpdateAsync<CustomerEvent, CustomerEventUpdateDto>(id, eventUpdate);
        }

        public async Task<EventDetailDto> GetEventById(Guid id)
        {
            var result = await _baseService.FindAsync<View_ListEvent, EventDetailDto>(id);
            return result;
        }

        public async Task<int> DeleteEvent(Guid id)
        {
            return await _baseService.DeleteAsync<Event, Guid>(id);
        }

        public async Task<int> DeleteEvent(int[] ids)
        {
            return await _baseService.DeleteAsync<Event, int>(ids);
        }
    }
}
