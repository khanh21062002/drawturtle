using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.AccessTimeSeg;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class AccessTimeSegservice
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public AccessTimeSegservice(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<AccessTimeSegGridDto>> GetAccessTimeSegs(AccessTimeSegGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<AccessTimeSeg, AccessTimeSegGridDto>(pagingModel);
        }

        public async Task<int> CreateAccessTimeSeg(AccessTimeSegCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<AccessTimeSeg, AccessTimeSegCreateDto>(categoryCreate);
            return categoryCreate.Id;
        }

        public async Task<AccessTimeSegDetailDto> GetAccessTimeSegById(int id)
        {
            return await _baseService.FindAsync<AccessTimeSeg, AccessTimeSegDetailDto>(id);
        }

        public async Task<int> UpdateAccessTimeSeg(int id, AccessTimeSegUpdateDto editedAccessTimeSeg)
        {
            return await _baseService.UpdateAsync<AccessTimeSeg, AccessTimeSegUpdateDto>(id, editedAccessTimeSeg);
        }

        public async Task<int> DeleteAccessTimeSeg(int id)
        {
            return await _baseService.DeleteAsync<AccessTimeSeg, int>(id);
        }
        public async Task<int> UpdateDelete(int id)
        {
            var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.TimeSegId == id && x.IsDelete == false);
            if (countGroupAccess > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                throw new EPSException(errors1);
            }
            else
            {
                var face = await _baseService.FindAsync<AccessTimeSeg, AccessTimeSegUpdateDto>(x => x.Id == id);
                face.IsDelete = true;
                 await _baseService.UpdateAsync<AccessTimeSeg, AccessTimeSegUpdateDto>(id, face);
            }
            return 0;
        }
        public async Task<int> UpdateDeletes(int[] ids)
        {
            foreach (var item in ids)
            {

                var company = await _baseService.FindAsync<AccessTimeSeg, AccessTimeSegUpdateDto>(x => x.Id == item);
                var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.TimeSegId == company.Id);
                if (countGroupAccess > 0)
                {
                    var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                    throw new EPSException(errors1);
                }
                else
                {
                    if (company != null)
                    {
                        company.IsDelete = true;
                        await _baseService.UpdateAsync<AccessTimeSeg, AccessTimeSegUpdateDto>(company.Id, company);
                    }
                }
            }
            return 0;
        }

        public async Task<int> DeleteAccessTimeSeg(int[] ids)
        {
            return await _baseService.DeleteAsync<AccessTimeSeg, int>(ids);
        }
    }
}
