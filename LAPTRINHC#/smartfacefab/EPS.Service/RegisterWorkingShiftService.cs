using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.RegisterWorkingShift;
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
    public class RegisterWorkingShiftService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public RegisterWorkingShiftService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        public async Task<PagingResult<RegisterWorkingShiftGridDto>> GetAll(RegisterWorkingShiftGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<RegisterWorkingShift, RegisterWorkingShiftGridDto>(pagingModel);
        }

        public async Task<RegisterWorkingShiftDetailDto> GetPersonById(Guid id)
        {
            var person = await _baseService.FindAsync<RegisterWorkingShift, RegisterWorkingShiftDetailDto>(id);

            return person;
        }

        public async Task<int> Create(RegisterWorkingShiftCreateDto createDto, bool isExploiting = false)
        {
            if (createDto.Type == 2)
            {
                var count = await _repository.CountAsync<RegisterWorkingShift>(x => x.PersonId == createDto.PersonId && x.Type == 2 && x.IsDelete == false && ((x.DateFrom <= createDto.DateFrom && x.DateTo >= createDto.DateFrom) || (x.DateFrom >= createDto.DateFrom && x.DateFrom <= createDto.DateTo)));
                if (count > 0)
                {
                    var errors = Constant.ErrorMessage.ERROR_EMPLOYEE;
                    throw new EPSException(errors);
                }
            }
            if (createDto.Type == 0)
            {
                var count = await _repository.CountAsync<RegisterWorkingShift>(x => x.CompId == createDto.CompId && x.Type == 0 && x.IsDelete == false && ((x.DateFrom <= createDto.DateFrom && x.DateTo >= createDto.DateFrom) || (x.DateFrom >= createDto.DateFrom && x.DateFrom <= createDto.DateTo)));
                if (count > 0)
                {
                    var errors = Constant.ErrorMessage.ERROR_EMPLOYEE;
                    throw new EPSException(errors);
                }
            }
            if (createDto.Type == 1)
            {
                var count = await _repository.CountAsync<RegisterWorkingShift>(x => x.DeptId == createDto.DeptId && x.Type == 1 && x.IsDelete == false && ((x.DateFrom <= createDto.DateFrom && x.DateTo >= createDto.DateFrom) || (x.DateFrom >= createDto.DateFrom && x.DateFrom <= createDto.DateTo)));
                if (count > 0)
                {
                    var errors = Constant.ErrorMessage.ERROR_EMPLOYEE;
                    throw new EPSException(errors);
                }
            }
            createDto.IsDelete = false;
            await _baseService.CreateAsync<RegisterWorkingShift, RegisterWorkingShiftCreateDto>(createDto);
            return createDto.ID;
        }

        public async Task<RegisterWorkingShiftDetailDto> GetDetails(int id)
        {
            return await _baseService.FindAsync<RegisterWorkingShift, RegisterWorkingShiftDetailDto>(id);
        }

        //List danh sách chi tiết 
        public async Task<List<RegisterWorkingShiftDetailCreateAndUpdateDto>> GetListNotiDetails(int workcalendarId)
        {
            List<Expression<Func<RegisterWorkingShiftDetailCreateAndUpdateDto, bool>>> predicates = new List<Expression<Func<RegisterWorkingShiftDetailCreateAndUpdateDto, bool>>>();
            predicates.Add(x => x.RegisterWorkingShiftId == workcalendarId && x.IsDelete == false);
            return await _baseService.Filter<RegisterWorkingShiftDetail, RegisterWorkingShiftDetailCreateAndUpdateDto>(predicates.ToArray()).ToListAsync();
        }

        public async Task<int> UpdateById(int id, RegisterWorkingShiftUpdateDto editDto)
        {
            if (editDto.Type == 2)
            {
                var count = await _repository.CountAsync<RegisterWorkingShift>(x => x.ID != id && x.PersonId == editDto.PersonId && x.Type == 2 && x.IsDelete == false && ((x.DateFrom <= editDto.DateFrom && x.DateTo >= editDto.DateFrom) || (x.DateFrom <= editDto.DateTo && x.DateTo >= editDto.DateTo)));
                if (count > 0)
                {
                    var errors = Constant.ErrorMessage.ERROR_EMPLOYEE;
                    throw new EPSException(errors);
                }
            }
            if (editDto.Type == 0)
            {
                var count = await _repository.CountAsync<RegisterWorkingShift>(x => x.ID != id && x.CompId == editDto.CompId && x.Type == 0 && x.IsDelete == false && ((x.DateFrom <= editDto.DateFrom && x.DateTo >= editDto.DateFrom) || (x.DateFrom <= editDto.DateTo && x.DateTo >= editDto.DateTo)));
                if (count > 0)
                {
                    var errors = Constant.ErrorMessage.ERROR_EMPLOYEE;
                    throw new EPSException(errors);
                }
            }
            if (editDto.Type == 1)
            {
                var count = await _repository.CountAsync<RegisterWorkingShift>(x => x.ID != id && x.DeptId == editDto.DeptId && x.Type == 1 && x.IsDelete == false && ((x.DateFrom <= editDto.DateFrom && x.DateTo >= editDto.DateFrom) || (x.DateFrom <= editDto.DateTo && x.DateTo >= editDto.DateTo)));
                if (count > 0)
                {
                    var errors = Constant.ErrorMessage.ERROR_EMPLOYEE;
                    throw new EPSException(errors);
                }
            }
            var listDetailsOld = _repository.Filter<RegisterWorkingShiftDetail>(x => x.RegisterWorkingShiftId == editDto.ID && x.IsDelete == false).Select(i => i.ID).ToList();
            foreach (var item in listDetailsOld)
            {
                var detailsItem = await _repository.FindAsync<RegisterWorkingShiftDetail>(x => x.ID == item);
                detailsItem.IsDelete = true;
                await _repository.UpdateAsync<RegisterWorkingShiftDetail>(detailsItem);
            }
            //then reinsert
            //update details
            foreach (var item in editDto.listDetails)
            {
                item.ID = 0;
                item.RegisterWorkingShiftId = id;
                await this.CreateDetails(item);
            }
            return await _baseService.UpdateAsync<RegisterWorkingShift, RegisterWorkingShiftUpdateDto>(id, editDto);
        }

        public async Task<int> UpdateDelete(int id)
        {
            //first delete all old data in list details:
            var listDetailsOld = _repository.Filter<RegisterWorkingShiftDetail>(x => x.RegisterWorkingShiftId == id && x.IsDelete == false).Select(i => i.ID).ToList();
            foreach (var item in listDetailsOld)
            {
                var detailsItem = await _repository.FindAsync<RegisterWorkingShiftDetail>(x => x.ID == item);
                detailsItem.IsDelete = true;
                await _repository.UpdateAsync<RegisterWorkingShiftDetail>(detailsItem);
            }
            var dayoffs = await _baseService.FindAsync<RegisterWorkingShift, RegisterWorkingShiftUpdateDto>(x => x.ID == id);
            dayoffs.IsDelete = true;
            return await _baseService.UpdateAsync<RegisterWorkingShift, RegisterWorkingShiftUpdateDto>(id, dayoffs);
        }

        public async Task<int> UpdateDeletes(int[] ids)
        {
            foreach (var item in ids)
            {
                var dayOff = await _baseService.FindAsync<RegisterWorkingShift, RegisterWorkingShiftUpdateDto>(x => x.ID == item);
                if (dayOff != null)
                {
                    dayOff.IsDelete = true;
                    await _baseService.UpdateAsync<RegisterWorkingShift, RegisterWorkingShiftUpdateDto>(dayOff.ID, dayOff);
                }
            }
            return 0;
        }

        public async Task<int> CreateDetails(RegisterWorkingShiftDetailCreateAndUpdateDto createDto, bool isExploiting = false)
        {
            createDto.IsDelete = false;
            await _baseService.CreateAsync<RegisterWorkingShiftDetail, RegisterWorkingShiftDetailCreateAndUpdateDto>(createDto);
            return createDto.ID;
        }

        public async Task<int> UpdateDetailsById(int id, RegisterWorkingShiftDetailCreateAndUpdateDto editDto)
        {
            return await _baseService.UpdateAsync<RegisterWorkingShiftDetail, RegisterWorkingShiftDetailCreateAndUpdateDto>(id, editDto);
        }
    }
}
