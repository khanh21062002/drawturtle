using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.RegisterWorkingShift;
using EPS.Service.Dtos.WorkingHours;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class WorkingHoursService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public WorkingHoursService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;

        }
        public async Task<PagingResult<WorkingHoursGridDto>> GetAll(WorkingHoursGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<WorkingHours, WorkingHoursGridDto>(pagingModel);
        }
        public async Task<int> Create(WorkingHoursCreateDto createDto, bool isExploiting = false)
        {
            await _baseService.CreateAsync<WorkingHours, WorkingHoursCreateDto>(createDto);
            return createDto.ID;
        }

        public async Task<WorkingHoursDetailsDto> GetDetails(int id)
        {
            return await _baseService.FindAsync<WorkingHours, WorkingHoursDetailsDto>(id);
        }
       
        public async Task<int> UpdateById(int id, WorkingHoursUpdateDto editDto)
        {
            return await _baseService.UpdateAsync<WorkingHours, WorkingHoursUpdateDto>(id, editDto);
        }

    }
}
