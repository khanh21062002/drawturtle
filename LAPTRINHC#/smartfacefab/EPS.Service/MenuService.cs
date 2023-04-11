using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Menu;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class MenuService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IUserIdentity<int> _userIdentity;
        private IMapper _mapper;

        public MenuService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _userIdentity = userIdentity;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<MenuGridDto>> GetMenus(MenuGridPagingDto pagingModel)
        {
            pagingModel.CompId = _userIdentity.CompId;
            return await _baseService.FilterPagedAsync<Menu, MenuGridDto>(pagingModel);
        }

        //get detail by id
        public async Task<MenuDetailDto> GetMenuById(int id)
        {
            return await _baseService.FindAsync<Menu, MenuDetailDto>(id);
        }

        //create
        public async Task<int?> CreateMenu(MenuCreateDto createDto)
        {
            createDto.IsDelete = false;
            await _baseService.CreateAsync<Menu, MenuCreateDto>(createDto);
            return createDto.Id;
        }

        //update
        public async Task<int?> UpdateMenu(int id, MenuUpdateDto updateDto)
        {
            updateDto.IsDelete = false;
            await _baseService.UpdateAsync<Menu, MenuUpdateDto>(id, updateDto);
            return updateDto.Id;
        }

        //delete: set IsDelete = true
        public async Task<int?> DeleteMenu(int? id)
        {
            var dto = await _baseService.FindAsync<Menu, MenuUpdateDto>(x => x.Id == id);
            dto.IsDelete = true;
            await _baseService.UpdateAsync<Menu, MenuUpdateDto>(id, dto);
            return dto.Id;
        }
    }
}
