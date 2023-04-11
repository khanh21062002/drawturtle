using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.PersonGroup;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class PersonGroupService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public PersonGroupService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<PersonGroupGridDto>> GetPersonGroups(PersonGroupGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<PersonGroup, PersonGroupGridDto>(pagingModel);
        }

        public async Task<int> CreatePersonGroup(PersonGroupCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<PersonGroup, PersonGroupCreateDto>(categoryCreate);
            return 0;
        }

        public async Task<PersonGroupDetailDto> GetPersonGroupById(int id)
        {
            return await _baseService.FindAsync<PersonGroup, PersonGroupDetailDto>(id);
        }

        public async Task<int> UpdatePersonGroup(int id, PersonGroupUpdateDto editedPersonGroup)
        {
            return await _baseService.UpdateAsync<PersonGroup, PersonGroupUpdateDto>(id, editedPersonGroup);
        }

        public async Task<int> DeletePersonGroup(int id)
        {
            return await _baseService.DeleteAsync<PersonGroup, int>(id);
        }

        public async Task<int> DeletePersonGroup(int[] ids)
        {
            return await _baseService.DeleteAsync<PersonGroup, int>(ids);
        }
    }
}
