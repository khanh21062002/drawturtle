using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.ShiftTime;
using EPS.Service.Dtos.WorkingShiftTimes;
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
    public class WorkingShiftTimeService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public WorkingShiftTimeService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        //Get All
        public List<WorkingShiftTimesGridDto> GetAllWorkingShiftTime()
        {
            return _baseService.Filter<WorkingShiftTimes, WorkingShiftTimesGridDto>(x => x.CompId == _userIdentity.CompId && !x.IsDelete.HasValue || !x.IsDelete.Value).ToList();
        }

        public async Task<PagingResult<WorkingShiftTimesGridDto>> GetListWorkingShiftTimes(WorkingShiftTimesGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<WorkingShiftTimes, WorkingShiftTimesGridDto>(pagingModel);
        }

        public async Task<List<WorkingShiftTimesDetailDto>> GetAllNoPaging()
        {
            int compId = _userIdentity.CompId.Value;
            return await _baseService.Filter<WorkingShiftTimes, WorkingShiftTimesDetailDto>(x => x.CompId == compId && x.IsDelete == false).ToListAsync();
        }

        public async Task<int> CreateWorkingShiftTimes(WorkingShiftTimesCreateDto shiftTimeCreate)
        {
            var count = await _repository.CountAsync<WorkingShiftTimes>(x => x.Code.Equals(shiftTimeCreate.Code.Trim()) && x.IsDelete == false && x.CompId == shiftTimeCreate.CompId);
            if (count > 0)
            {
                var errors1 = "TimeKeepService.Message.ShiftTimeCode";
                throw new EPSException(errors1);
            }
            else
            {
                shiftTimeCreate.IsDelete = false;
                await _baseService.CreateAsync<WorkingShiftTimes, WorkingShiftTimesCreateDto>(shiftTimeCreate);
                return shiftTimeCreate.ID;
            }
        }

        public async Task<WorkingShiftTimesDetailDto> GetWorkingShiftTimesById(int id)
        {
            return await _baseService.FindAsync<WorkingShiftTimes, WorkingShiftTimesDetailDto>(id);
        }

        public async Task<WorkingShiftTimesDetailDto> GetWorkingShiftTimesByCompId(int compId)
        {
            return await _baseService.FindAsync<WorkingShiftTimes, WorkingShiftTimesDetailDto>(x => x.IsDelete == false && x.CompId == compId);
        }

        public async Task<int> UpdateWorkingShiftTimes(int id, WorkingShiftTimesUpdateDto editedShiftTime)
        {
            var count = await _repository.CountAsync<WorkingShiftTimes>(x => x.Code.Equals(editedShiftTime.Code.Trim()) && x.IsDelete == false && x.CompId == editedShiftTime.CompId && x.ID != id);
            if (count > 0)
            {
                var errors1 = "TimeKeepService.Message.ShiftTimeCode";
                throw new EPSException(errors1);
            }
            else
            {
                return await _baseService.UpdateAsync<WorkingShiftTimes, WorkingShiftTimesUpdateDto>(id, editedShiftTime);
            }
        }

        public async Task<int> UpdateDelete(int id)
        {
            var workingShiftTimes = await _baseService.FindAsync<WorkingShiftTimes, WorkingShiftTimesDetailDto>(id);
            var count = await _repository.CountAsync<RegisterWorkingShiftDetail>(x => x.WorkingShiftId == id && x.IsDelete == false);
            var countGroupAccess = await _repository.CountAsync<TimeKeeping>(x => x.SHIFT_ID == id);
            var countGroup = await _repository.CountAsync<OverTime>(x => x.SHIFT_ID == id);

            if (count > 0 || countGroupAccess > 0 || countGroup > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                throw new EPSException(errors1);
            }
            else
            {
                var shiftTime = await _baseService.FindAsync<WorkingShiftTimes, WorkingShiftTimesUpdateDto>(x => x.ID == id);
                shiftTime.IsDelete = true;
                await _baseService.UpdateAsync<WorkingShiftTimes, WorkingShiftTimesUpdateDto>(id, shiftTime);
            }
            return 0;
        }

        public async Task<int> UpdateDeletes(int[] ids)
        {
            foreach (var item in ids)
            {
                var shiftTime = await _baseService.FindAsync<WorkingShiftTimes, WorkingShiftTimesUpdateDto>(x => x.ID == item);
                if (shiftTime != null)
                {
                    shiftTime.IsDelete = true;
                    await _baseService.UpdateAsync<WorkingShiftTimes, WorkingShiftTimesUpdateDto>(shiftTime.ID, shiftTime);
                }
            }
            return 0;
        }
    }
}
