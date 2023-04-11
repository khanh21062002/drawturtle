using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.PreOrder;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class PreOrderService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IUserIdentity<int> _userIdentity;
        private IMapper _mapper;

        public PreOrderService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _userIdentity = userIdentity;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<PreOrderGridDto>> GetPreOrders(PreOrderGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            return await _baseService.FilterPagedAsync<PreOrder, PreOrderGridDto>(pagingModel);
        }

        //get detail by id
        public async Task<PreOrderDetailDto> GetPreOrderById(int id)
        {
            return await _baseService.FindAsync<PreOrder, PreOrderDetailDto>(id);
        }

        //create
        public async Task<int?> CreatePreOrder(PreOrderCreateDto createDto)
        {
            createDto.IsDelete = false;
            createDto.DateCreate = DateTime.Now;
            await _baseService.CreateAsync<PreOrder, PreOrderCreateDto>(createDto);
            return createDto.Id;
        }

        //update
        public async Task<int?> UpdatePreOrder(int id, PreOrderUpdateDto updateDto)
        {
            updateDto.IsDelete = false;
            await _baseService.UpdateAsync<PreOrder, PreOrderUpdateDto>(id, updateDto);
            return updateDto.Id;
        }

        //delete: set IsDelete = true
        public async Task<int?> DeletePreOrder(int? id)
        {
            var dto = await _baseService.FindAsync<PreOrder, PreOrderUpdateDto>(x => x.Id == id);
            dto.IsDelete = true;
            await _baseService.UpdateAsync<PreOrder, PreOrderUpdateDto>(id, dto);
            return dto.Id;
        }

        public PersonDetailDto GetInfoByPhone(string phoneNumber)
        {
            PersonDetailDto rs = new PersonDetailDto();
            var check = _baseService.Filter<Person, PersonDetailDto>(x => x.PhoneNumber == phoneNumber && x.CompId == _userIdentity.CompId).Any();
            if (check)
            {
                rs = _baseService.Filter<Person, PersonDetailDto>(x => x.PhoneNumber == phoneNumber && x.CompId == _userIdentity.CompId).OrderByDescending(x => x.RegisterTime).FirstOrDefault();
            }
            return rs;
        }
    }
}
