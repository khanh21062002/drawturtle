using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Notification;
using EPS.Service.Helpers;
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
    public class NotificationService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        public NotificationService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<NotificationGridDto>> GetAll(NotificationGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Notification, NotificationGridDto>(pagingModel);
        }
        public async Task<int> Create(NotificationCreateDto createDto, bool isExploiting = false)
        {
            createDto.IsDelete = false;
            await _baseService.CreateAsync<Notification, NotificationCreateDto>(createDto);
            return createDto.ID;
        }

        public async Task<NotificationDetailDto> GetDetails(int id)
        {
            return await _baseService.FindAsync<Notification, NotificationDetailDto>(id);
        }

        public async Task<int> UpdateById(int id, NotificationUpdateDto editDto)
        {
            //first delete all
            var lstOld = _repository.Filter<NotificationDetail>(x => x.NotiId == id && x.IsDelete == false).Select(i => i.ID).ToArray();
            await _baseService.DeleteAsync<NotificationDetail, int>(lstOld);
            // then reinsert
            foreach (var item in editDto.NotificationDetails)
            {
                item.ID = 0;
                item.NotiId = id;
                await this.CreateNotiDetail(item);
            }
            return await _baseService.UpdateAsync<Notification, NotificationUpdateDto>(id, editDto);
        }

        public async Task<int> UpdateDelete(int id)
        {
            var dayoffs = await _baseService.FindAsync<Notification, NotificationUpdateDto>(x => x.ID == id);
            dayoffs.IsDelete = true;
            return await _baseService.UpdateAsync<Notification, NotificationUpdateDto>(id, dayoffs);
        }

        //Thêm chi tiết noti
        public async Task<int> CreateNotiDetail(NotificationDetailCreateDto createDto, bool isExploiting = false)
        {
            createDto.IsDelete = false;
            await _baseService.CreateAsync<NotificationDetail, NotificationDetailCreateDto>(createDto);
            return createDto.ID;
        }

        //Update chi tiết noti
        public async Task<int> UpdateNotiDetail(NotificationDetailCreateDto edit)
        {
            return await _baseService.UpdateAsync<NotificationDetail, NotificationDetailCreateDto>(edit.ID, edit);
        }

        //List danh sách chi tiết noti
        public async Task<List<NotificationDetailGridDto>> GetListNotiDetails(int notiID)
        {
            List<Expression<Func<NotificationDetailGridDto, bool>>> predicates = new List<Expression<Func<NotificationDetailGridDto, bool>>>();

            predicates.Add(x => x.NotiId == notiID && x.IsDelete == false);
            return await _baseService.Filter<NotificationDetail, NotificationDetailGridDto>(predicates.ToArray()).ToListAsync();
        }



    }
}
