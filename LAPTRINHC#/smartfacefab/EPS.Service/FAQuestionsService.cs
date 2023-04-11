using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.FAQuestions;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static EPS.Service.Dtos.FAQuestions.FAQuestionsGirdPagingDto;

namespace EPS.Service
{
    public class FAQuestionsService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public FAQuestionsService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }


        public async Task<PagingResult<FAQuestionsGridDto>> GetFAQuestions(FAQuestionsGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<FAQuestions, FAQuestionsGridDto>(pagingModel);
        }
        public async Task<FAQuestionsGridDto> GetFAQuestionsById(int id)
        {
            return await _baseService.FindAsync<FAQuestions, FAQuestionsGridDto>(x => x.Id == id);
        }
        public async Task<int> CreateFAQuestions(FAQuestionsCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<FAQuestions, FAQuestionsCreateDto>(categoryCreate);
            return 0;
        }

        public async Task<int> UpdateFAQuestions(int id, FAQuestionsUpdateDto editedFaquestions)
        {
            return await _baseService.UpdateAsync<FAQuestions, FAQuestionsUpdateDto>(id, editedFaquestions);
        }
        public async Task<int> DeleteFAQuestions(int id)
        {
            return await _baseService.DeleteAsync<FAQuestions, int>(id);
        }

        public async Task<int> DeleteFAQuestions(int[] ids)
        {
            return await _baseService.DeleteAsync<FAQuestions, int>(ids);
        }

    }

}

