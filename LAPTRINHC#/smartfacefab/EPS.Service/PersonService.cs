using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Data.Helpers;
using EPS.Service.Dtos.Card;
using EPS.Service.Dtos.Company;
using EPS.Service.Dtos.Department;
using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.Group;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.PersonGroup;
using EPS.Service.Dtos.PersonsAccess;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.WireProtocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class PersonService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public PersonService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        public async Task<PagingResult<PersonGridDto>> GetPersons(PersonGridPagingDto pagingModel)
        {
            pagingModel.SortBy = "registerTime desc, updateTime";
            pagingModel.SortDesc = true;
            var result = await _baseService.FilterPagedAsync<View_ListPerson, PersonGridDto>(pagingModel);
            return result;
        }

        public async Task<PagingResult<PersonGridDto>> GetEmployee(PersonGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Person, PersonGridDto>(pagingModel);
        }

        public async Task<PagingResult<CustomerEventGridDto>> GetPersonsMachine(PersonMachineGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<v_CustomerEvent, CustomerEventGridDto>(pagingModel);
        }

        //Lấy all nhân viên
        public List<PersonGridDto> GetAllPerson()
        {
            return _baseService.Filter<Person, PersonGridDto>(x => x.CompId == _userIdentity.CompId && !x.IsDelete && !string.IsNullOrEmpty(x.PersonCode)).ToList();
        }

        //Lấy all nhân viên
        public List<string> GetAllPersonCode()
        {
            return _baseService.Filter<Person, PersonGridDto>(x => x.CompId == _userIdentity.CompId && !x.IsDelete && !string.IsNullOrEmpty(x.PersonCode)).ToList().Select(x => x.PersonCode).ToList();
        }

        public async Task<int> CreatePerson(PersonCreateDto categoryCreate, bool checkPerCode = true, bool isExploiting = false)
        {
            categoryCreate.IsDelete = false;
            var count = 0;
            if (checkPerCode)
                count = await _repository.CountAsync<Person>(x => (x.CompType != AppConstants.PersonCompType.SCHOOL_PARENT && x.CompType != AppConstants.PersonCompType.SCHOOL_STU) && x.PersonCode == categoryCreate.PersonCode.Trim() && x.IsDelete == false && x.CompId == categoryCreate.CompId);
            if (count > 0)
            {
                var errors1 = "GroupService.Message.PersonCode";
                throw new EPSException(errors1);
            }
            else
            {
                categoryCreate.IsDelete = false;
                categoryCreate.RegisterTime = DateTime.Now;
                await _baseService.CreateAsync<Person, PersonCreateDto>(categoryCreate);
                return 0;
            }
        }

        public async Task<int> CreateListPerson(List<PersonCreateDto> lstPerson, List<PersonsAccessCreateDto> lstPersonAccess, bool checkDuplicatePersonCode)
        {
            if (checkDuplicatePersonCode)
            {
                foreach (var item in lstPerson)
                {
                    item.IsDelete = false;
                    var count = 0;

                    count = await _repository.CountAsync<Person>(x => (x.CompType != AppConstants.PersonCompType.SCHOOL_PARENT && x.CompType != AppConstants.PersonCompType.SCHOOL_STU) && x.PersonCode == item.PersonCode.Trim() && x.IsDelete == false && x.CompId == item.CompId);
                    if (count > 0)
                    {
                        throw new EPSException("GroupService.Message.PersonCode");
                    }
                }
            }
            //call function to insert all person to person
            this.CreateAsyncPersonUsingBulkSql(lstPerson.ToArray());
            //this.CreateAsyncPersonGroupUsingBulkSql(lstPersonGroup.ToArray());
            this.CreateAsyncPersonAccessUsingBulkSql(lstPersonAccess.ToArray());
            return 0;
        }

        //using bulkSql to faster
        private void CreateAsyncPersonUsingBulkSql(params PersonCreateDto[] dtos)
        {
            BulkSql _bulkSql = new BulkSql("EPS");
            var entities = _mapper.Map<PersonCreateDto[], Person[]>(dtos);
            string tableName = "Persons";
            _bulkSql.InsertData<Person>(entities.ToList(), tableName);

            if (_mapper.ConfigurationProvider.FindTypeMapFor<Person, PersonCreateDto>() != null)
            {
                for (var i = 0; i < entities.Length; i++)
                {
                    _mapper.Map(entities[i], dtos[i]);
                }
            }
        }

        private void CreateAsyncPersonGroupUsingBulkSql(params PersonGroupCreateDto[] dtos)
        {
            BulkSql _bulkSql = new BulkSql("EPS");
            var entities = _mapper.Map<PersonGroupCreateDto[], PersonGroup[]>(dtos);
            string tableName = "PersonGroup";
            _bulkSql.InsertData<PersonGroup>(entities.ToList(), tableName);

            if (_mapper.ConfigurationProvider.FindTypeMapFor<PersonGroup, PersonGroupCreateDto>() != null)
            {
                for (var i = 0; i < entities.Length; i++)
                {
                    _mapper.Map(entities[i], dtos[i]);
                }
            }
        }

        private void CreateAsyncPersonAccessUsingBulkSql(params PersonsAccessCreateDto[] dtos)
        {
            BulkSql _bulkSql = new BulkSql("EPS");
            var entities = _mapper.Map<PersonsAccessCreateDto[], PersonsAccess[]>(dtos);
            string tableName = "PersonsAccess";
            _bulkSql.InsertData<PersonsAccess>(entities.ToList(), tableName);

            if (_mapper.ConfigurationProvider.FindTypeMapFor<PersonsAccess, PersonsAccessCreateDto>() != null)
            {
                for (var i = 0; i < entities.Length; i++)
                {
                    _mapper.Map(entities[i], dtos[i]);
                }
            }
        }

        public async Task<int> UpdatePersonDetail(Guid id, FaceUpdateDto editedPerson)
        {
            var face = await _baseService.FindAsync<Face, FaceUpdateDto>(x => x.PersonId == id);
            face.Status = 0;
            face.UpdateTime = DateTime.UtcNow.Date;
            face.UpdateBy = _userIdentity.Username; ;

            return await _baseService.UpdateAsync<Face, FaceUpdateDto>(face.FaceId, face);
        }

        public async Task<int> UpdateFace(Guid faceId, FaceUpdateDto editedPerson)
        {
            return await _baseService.UpdateAsync<Face, FaceUpdateDto>(faceId, editedPerson);
        }

        public async Task<int> CreatePersonDetail(FaceCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<Face, FaceCreateDto>(categoryCreate);
            return 0;
        }

        //Tạo thời gian làm việc
        public async Task<int> CreateDateTimeToPersonAccess(PersonsAccessCreateDto createDto, bool isExploiting = false)
        {
            if (createDto.FromDate != null)
            {
                if (createDto.ToDate == null)
                {
                    createDto.ToDate = DateTime.MaxValue;
                }
            }
            if (createDto.ToDate != null)
            {
                if (createDto.FromDate == null)
                {
                    createDto.FromDate = DateTime.Parse("1900-01-01");
                }
            }
            await _baseService.CreateAsync<PersonsAccess, PersonsAccessCreateDto>(createDto);
            return 0;
        }

        //Update thời gian làm việc
        public async Task<bool> UpdateDateTimeToPersonAccess(Guid personId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var accessTime = _repository.Filter<PersonsAccess>(x => x.PersonId == personId).ToList();

                foreach (var item in accessTime)
                {
                    item.FromDate = fromDate;
                    item.ToDate = toDate;
                }

                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Xóa thời gian làm việc
        public async Task<bool> DeleteDateTimeToPersonAccess(Guid personId)
        {
            try
            {
                var accessTime = _repository.Filter<PersonsAccess>(x => x.PersonId == personId).ToList();

                foreach (var item in accessTime)
                {
                    item.IsDelete = true;
                }

                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CreatePersonGroupDetail(PersonGroupCreateDto categoryCreate, bool isExploiting = false)
        {
            await _baseService.CreateAsync<PersonGroup, PersonGroupCreateDto>(categoryCreate);
            return 0;
        }

        public async Task<PersonDetailDto> GetPersonById(Guid id)
        {
            //get thông tin
            var person = await _baseService.FindAsync<Person, PersonDetailDto>(x => x.PersonId == id && x.IsDelete == false);

            //get thời gian làm việc
            var accessTime = await _baseService.FindAsync<PersonsAccess, PersonsAccessDetailDto>(x => x.PersonId == id && x.IsDelete == false);

            if (accessTime != null)
            {
                //gán lại thời gian truy cập
                person.FromDate = accessTime.FromDate;
                person.ToDate = accessTime.ToDate;
                person.FromDateStr = String.Format("{0:dd/MM/yyyy}", accessTime.FromDate);
                person.ToDateStr = String.Format("{0:dd/MM/yyyy}", accessTime.ToDate);
            }

            return person;
        }

        public async Task<FaceUpdateDto> GetFaceByPersonId(Guid id)
        {
            var person = await _baseService.FindAsync<Face, FaceUpdateDto>(x => x.PersonId == id);
            return person;
        }

        public async Task<List<FaceUpdateDto>> GetListActiveFaceByPersonId(Guid id)
        {
            var person = await _baseService.Filter<Face, FaceUpdateDto>(x => x.PersonId == id && x.Status == 1).ToListAsync();
            return person;
        }

        public async Task<CompanyDetailDto> GetCompany(int? compId)
        {
            var person = await _baseService.FindAsync<Company, CompanyDetailDto>(x => x.Id == compId);
            return person;
        }

        public async Task UpdateAvatar(Guid perId, string path)
        {
            var person = await _repository.FindAsync<Person>(x => x.PersonId == perId);
            person.File1 = path;
            await _repository.SaveChangesAsync();
        }

        public async Task<int> UpdatePersonImage(Guid id, PersonUpdateDto editedPerson)
        {
            await _baseService.UpdateAsync<Person, PersonUpdateDto>(id, editedPerson);
            return 0;
        }

        public async Task<int> UpdatePerson(Guid id, PersonUpdateDto editedPerson)
        {
            var count = await _repository.CountAsync<Person>(x => (x.CompType != AppConstants.PersonCompType.SCHOOL_PARENT && x.CompType != AppConstants.PersonCompType.SCHOOL_STU) && x.PersonCode == editedPerson.PersonCode.Trim() && x.PersonId != id && x.IsDelete == false && x.CompId == editedPerson.CompId);
            if (count > 0)
            {
                var errors1 = "GroupService.Message.PersonCode";
                throw new EPSException(errors1);
            }
            else
            {
                editedPerson.UpdateTime = DateTime.UtcNow.Date;
                editedPerson.UpdateBy = _userIdentity.Username;
                await _baseService.UpdateAsync<Person, PersonUpdateDto>(id, editedPerson);
            }
            return 0;
        }

        public async Task UpdatePersonForGuess(Guid id, PersonUpdateDto editedPerson)
        {
            await _baseService.UpdateAsync<Person, PersonUpdateDto>(id, editedPerson);
        }

        public async Task<int> UpdateDelete(Guid id)
        {
            var countPer = await _repository.CountAsync<View_ListEvent>(x => x.PersonId == id);

            if (countPer > 0)
            {
                throw new Exception("Constant.Error1");
            }
            else
            {
                var facesImg = await _baseService.FindAsync<Face, FaceUpdateDto>(x => x.PersonId == id);
                if (facesImg != null)
                {
                    facesImg.Status = 0;
                    facesImg.DeleteBy = _userIdentity.Username;
                    facesImg.DeleteTime = DateTime.UtcNow.Date;
                    await _baseService.UpdateAsync<Face, FaceUpdateDto>(facesImg.FaceId, facesImg);
                }
                var face1 = await _baseService.FindAsync<PersonGroup, PersonGroupUpdateDto>(x => x.PersonId == id);
                if (face1 != null)
                {
                    face1.IsDelete = true;
                    await _baseService.UpdateAsync<PersonGroup, PersonGroupUpdateDto>(face1.Id, face1);
                }
                var face = await _baseService.FindAsync<Person, PersonUpdateDto>(x => x.PersonId == id);
                face.IsDelete = true;
                face.DeleteTime = DateTime.UtcNow.Date;
                face.DeleteBy = _userIdentity.Username;
                await _baseService.UpdateAsync<Person, PersonUpdateDto>(id, face);
                return 0;
            }
        }

        public async Task<int> DeletePerson(Guid personid)
        {
            return await _baseService.DeleteAsync<Person, Guid>(personid);
        }

        public async Task<int> UpdateDeletes(string[] ids)
        {
            foreach (var item in ids)
            {
                var company = await _baseService.FindAsync<Person, PersonUpdateDto>(x => x.PersonId == new Guid(item));
                if (company != null)
                {
                    company.IsDelete = true;
                    company.DeleteTime = DateTime.UtcNow.Date;
                    company.DeleteBy = _userIdentity.Username;
                    await _baseService.UpdateAsync<Person, PersonUpdateDto>(company.PersonId, company);
                }
            }
            return 0;
        }
    }
}
