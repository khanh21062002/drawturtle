using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.AppParam;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class AppParamService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public AppParamService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<AppParamGridDto>> GetAppParams(AppParamGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<AppParam, AppParamGridDto>(pagingModel);
        }

        public async Task<int> CreateAppParam(AppParamCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<AppParam, AppParamCreateDto>(categoryCreate);
            return 0;
        }

        public async Task<AppParamDetailDto> GetAppParamById(int id)
        {
            return await _baseService.FindAsync<AppParam, AppParamDetailDto>(id);
        }

        public async Task<int> UpdateAppParam(int id, AppParamUpdateDto editedAppParam)
        {
            return await _baseService.UpdateAsync<AppParam, AppParamUpdateDto>(id, editedAppParam);
        }

        public async Task<int> DeleteAppParam(int id)
        {
            return await _baseService.DeleteAsync<AppParam, int>(id);
        }

        public async Task<int> DeleteAppParam(int[] ids)
        {
            return await _baseService.DeleteAsync<AppParam, int>(ids);
        }
    }
}
