using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.FAQuestions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EPS.Service.Dtos.FAQuestions.FAQuestionsGirdPagingDto;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/faquestions")]
    [Authorize]
    public class FAQuestionsController : BaseController
    {
        private FAQuestionsService _fAQuestionsService;

        public FAQuestionsController(FAQuestionsService fAQuestionsService)
        {
            _fAQuestionsService = fAQuestionsService;
        }
        //list all
        [CustomAuthorize(PrivilegeList.ViewFAQuestions, PrivilegeList.ManageFAQuestions)]
        [HttpGet]
        public async Task<IActionResult> GetListFAQuestions([FromQuery] FAQuestionsGridPagingDto pagingModel)
        {
            return Ok(await _fAQuestionsService.GetFAQuestions(pagingModel));
        }
        //detail
        [CustomAuthorize(PrivilegeList.ViewFAQuestions, PrivilegeList.ManageFAQuestions)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFAQuestionsById(int id)
        {
            return Ok(await _fAQuestionsService.GetFAQuestionsById(id));
        }
        //create
        [CustomAuthorize( PrivilegeList.ManageFAQuestions)]
        [HttpPost]
        public async Task<IActionResult> Create(FAQuestionsCreateDto faquestionsCreateDto)
        {
            return Ok(await _fAQuestionsService.CreateFAQuestions(faquestionsCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.ManageFAQuestions)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFAQuestions(int id, FAQuestionsUpdateDto faquestionsUpdateDto)
        {
            return Ok(await _fAQuestionsService.UpdateFAQuestions(id, faquestionsUpdateDto));
        }

        //delete
        [CustomAuthorize( PrivilegeList.ManageFAQuestions)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _fAQuestionsService.DeleteFAQuestions(id));
        }

        //multiple delete 
        [CustomAuthorize( PrivilegeList.ManageFAQuestions)]
        [HttpDelete]
        public async Task<IActionResult> DeleteFAQuestions(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var FAQuestions = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _fAQuestionsService.DeleteFAQuestions(FAQuestions));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
