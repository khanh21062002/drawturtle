using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Face;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class FaceService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public FaceService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<FaceGridDto>> GetFaces(FaceGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Face, FaceGridDto>(pagingModel);
        }

        public async Task<int> CreateFace(FaceCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<Face, FaceCreateDto>(categoryCreate);
            return 0;
        }

        public async Task<FaceDetailDto> GetFaceById(int id)
        {
            return await _baseService.FindAsync<Face, FaceDetailDto>(x => x.Id == id);
        }

        public async Task<Guid> GetFaceByPersonId(Guid id)
        {
            var face = await _repository.FindAsync<Face>(x => x.PersonId == id);

            return face.FaceId;
        }

        public async Task<int> UpdateFace(Guid id, FaceUpdateDto editedFace)
        {
            return await _baseService.UpdateAsync<Face, FaceUpdateDto>(id, editedFace);
        }

        public async Task<int> DeleteFace(int id)
        {
            return await _baseService.DeleteAsync<Face, int>(id);
        }

        public async Task<int> DeleteFace(int[] ids)
        {
            return await _baseService.DeleteAsync<Face, int>(ids);
        }
    }
}
