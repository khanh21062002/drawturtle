using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Grades;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Service;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class GradesService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IOptions<AppSettings> _settings;

        public GradesService(EPSRepository repository, IMapper mapper, IOptions<AppSettings> settings)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _settings = settings;
        }

        public async Task<PagingResult<GradesGridDto>> GetAll(GradesGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Grades, GradesGridDto>(pagingModel);
        }
        public async Task<int> Create(GradesCreateDto createDto, bool isExploiting = false)
        {
            createDto.IsDelete = false;
            await _baseService.CreateAsync<Grades, GradesCreateDto>(createDto);
            return createDto.Id;
        }

        public async Task<GradesDetailDto> GetById(int id)
        {
            return await _baseService.FindAsync<Grades, GradesDetailDto>(id);
        }

        public async Task<int> Update(int id, GradesUpdateDto editDto)
        {

            return await _baseService.UpdateAsync<Grades, GradesUpdateDto>(id, editDto);
        }

        public async Task<int> UpdateDelete(int id)
        {
           

            var countClass = await _repository.CountAsync<Department>(x => x.GradesId == id && x.IsDelete == false);

            if (countClass > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                throw new EPSException(errors1);
            }
            else
            {
                var dayoffs = await _baseService.FindAsync<Grades, GradesUpdateDto>(x => x.Id == id);
                dayoffs.IsDelete = true;
                return await _baseService.UpdateAsync<Grades, GradesUpdateDto>(id, dayoffs);

            }
            return 0;
        }


    }

}