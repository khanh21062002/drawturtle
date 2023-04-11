using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.Machine;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



namespace EPS.Service
{
    public class ApiService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;
        private PersonService _personService;

        private readonly AppSettings _appSettings;
        public ApiService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity, PersonService personService)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
            _personService = personService;
        }


        public async Task<ApiResponseDto> GetEvents(EventGridPagingDto filterDto)
        {
            ApiResponseDto response = new ApiResponseDto();
            try
            {
                List<Expression<Func<EventGridDto, bool>>> predicates = filterDto.GetPredicates();

                var data = await _baseService.Filter<Event, EventGridDto>(predicates.ToArray()).ToListAsync();
                data.OrderBy(x => x.SyncDatetime);

                response.HttpStatus = "200";
                response.ApiStatus = "1";
                response.ResponseMessage = "Success";
                response.Data = data;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatus = "500";
                response.ApiStatus = "0";
                response.ResponseMessage = "Internal Server Error: " + ex.Message;
                response.Data = "";
            }
            return response;
        }


    }
}
