using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Face;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/face")]
    [Authorize]

    public class FaceController : BaseController
    {
        private FaceService _faceService;

        public FaceController(FaceService faceService)
        {
            _faceService = faceService;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewFace, PrivilegeList.ManageFace)]
        [HttpGet]
        public async Task<IActionResult> GetListFaces([FromQuery] FaceGridPagingDto pagingModel)
        {
            return Ok(await _faceService.GetFaces(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageFace)]
        [HttpPost]
        public async Task<IActionResult> Create(FaceCreateDto faceCreateDto)
        {
            return Ok(await _faceService.CreateFace(faceCreateDto));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewFace, PrivilegeList.ManageFace)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFaceById(int id)
        {
            return Ok(await _faceService.GetFaceById(id));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageFace)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFace(Guid id, FaceUpdateDto faceUpdateDto)
        {
            return Ok(await _faceService.UpdateFace(id, faceUpdateDto));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageFace)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _faceService.DeleteFace(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageFace)]
        [HttpDelete]
        public async Task<IActionResult> DeleteFaces(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var Faces = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _faceService.DeleteFace(Faces));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
