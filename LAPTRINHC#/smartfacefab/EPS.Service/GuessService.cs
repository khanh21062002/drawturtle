using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Guess;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class GuessService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public GuessService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<int> CreateOTP(OTPCreateDto oTPCreateDto, bool isExploiting = false)
        {
            Random random = new Random();
            oTPCreateDto.RequestTime = DateTime.Now;
            oTPCreateDto.ExpirationTime = DateTime.Now.AddMinutes(3);
            oTPCreateDto.OTPCode = random.Next(100000, 999999).ToString();

            await _baseService.CreateAsync<OTP, OTPCreateDto>(oTPCreateDto);
            return oTPCreateDto.ID;
        }

        public async Task<bool> CheckOTP(string phoneNumber, string otpCode)
        {
            var exTime = DateTime.Now;
            var rs = await _repository.FindAsync<OTP>(x => x.PhoneNumber == phoneNumber && x.OTPCode == otpCode && x.ExpirationTime >= exTime);

            if (rs != null)
                return true;
            else
                return false;
        }

        public async Task<int> CreateGuess(GuessRegisterCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<GuessRegister, GuessRegisterCreateDto>(categoryCreate);
            return categoryCreate.ID;
        }

        public async Task<bool> ApproveGuess(int id, int approveValue, Guid? perid)
        {
            var guess = await _repository.FindAsync<GuessRegister>(x => x.ID == id);
            guess.Approve = approveValue;
            if (perid.HasValue)
                guess.PersonId = perid.Value;

            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<GuessDetailDto> GetGuessId(int id)
        {
            var rs = await _baseService.FindAsync<GuessRegister, GuessDetailDto>(id);
            return rs;
        }

        public async Task<GuessDetailDto> GetGuessByPhoneNumber(string phoneNumber)
        {
            GuessDetailDto rs = new GuessDetailDto();
            var check = _baseService.Filter<GuessRegister, GuessDetailDto>(x => x.PhoneNumber == phoneNumber).Any();
            if (check)
            {
                rs = _baseService.Filter<GuessRegister, GuessDetailDto>(x => x.PhoneNumber == phoneNumber).OrderByDescending(x => x.ID).FirstOrDefault();
                rs.StartTime = null;
                rs.EndTime = null;
                rs.FeverDec = null;
                rs.TravelDec = null;
                rs.ImageUrl = null;
            }
            return rs;
        }

        public async Task<PagingResult<GuessGridDto>> GetListGuess(GuessGridPagingDto pagingModel)
        {
            //return await _baseService.FilterPagedAsync<View_ListGuess, GuessGridDto>(pagingModel);
            return await _baseService.FilterPagedAsync<GuessRegister, GuessGridDto>(pagingModel);
        }

        //Lấy perid theo số điện thoại
        public Guid? GetPersonId(string phoneNumber)
        {
            var checkExists = _repository.Filter<GuessRegister>(x => x.PhoneNumber == phoneNumber.Trim() && x.PersonId.HasValue).Any();
            if (checkExists)
            {
                var per = _repository.Filter<GuessRegister>(x => x.PhoneNumber == phoneNumber && x.PersonId.HasValue).OrderByDescending(x => x.ID).FirstOrDefault();
                return per.PersonId;
            }
            else
                return null;
        }

        //update time guess
        public async Task<int> UpdateTimeGuess(int id, GuessUpdateDto editDto)
        {
            //var sql = "UPDATE GuessRegister SET StartTime = " + "'" + editDto.StartTime.ToString("yyyy/MM/dd HH:mm:ss") + "'" + ", EndTime = " + "'" + editDto.EndTime.ToString("yyyy/MM/dd HH:mm:ss") + "'" + " WHERE ID = " + id;
            //var rs = _repository.ExecuteNonQuery(sql);
            //return rs;
            return await _baseService.UpdateAsync<GuessRegister, GuessUpdateDto>(id, editDto);
        }
    }
}
