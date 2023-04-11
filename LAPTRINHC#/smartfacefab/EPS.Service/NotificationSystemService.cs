using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.NotificationSystem;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class NotificationSystemService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public NotificationSystemService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        //Get by userId
        public async Task<PagingResult<NotificationSystemGridDto>> GetByUserId(NotificationSystemPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<NotificationSystem, NotificationSystemGridDto>(pagingModel);
        }

        public async Task<int> NotificationCount(int userId)
        {
            int rs = 0;

            rs = await _repository.CountAsync<NotificationSystem>(x => x.UserId == userId && x.Readed == false);

            return rs;
        }

        public async Task<NotificationSystemDetailDto> GetNotificationById(int id)
        {
            return await _baseService.FindAsync<NotificationSystem, NotificationSystemDetailDto>(id);
        }

        public async Task<bool> UpdateNotificationStatus(int id)
        {
            var item = _repository.Find<NotificationSystem>(x => x.ID == id);

            //Đã đọc return false, chưa đọc return true
            if (item.Readed.HasValue && item.Readed.Value)
                return false;
            else
            {
                item.Readed = true;
                await _repository.SaveChangesAsync();
                return true;
            }    
        }
    }
}
