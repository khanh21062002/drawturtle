using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.FaceMatch;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class FaceMatchService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public FaceMatchService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        public async Task<int> CreateMatchs(MatchCreateDto personsCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<Match, MatchCreateDto>(personsCreate);
            return personsCreate.Id;
        }

        public async Task<MatchDetailDto> CreateMatch(MatchDetailDto editMatch, int id)
        {
            MatchCreateDto cre = new MatchCreateDto();
            var count = await _repository.CountAsync<Match>(x => x.Id == id);
            if (count > 0)
            {
                //editMatch.status = 1;
                //editMatch.message = "Error";
            }
            else
            {
                //editMatch.status = 0;
                //editMatch.message = "Success";
                editMatch.similary = cre.Similary;
            }
            return editMatch;
        }
    }

}
