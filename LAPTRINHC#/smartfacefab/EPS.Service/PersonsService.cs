using ArcFaceService.Entity;
using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Collection;
using EPS.Service.Dtos.FaceApiDto;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.Persons;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class PersonsService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public PersonsService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        //get all cache person
        public PersonUnit GetPersons(string personId)
        {
            return _baseService.Filter<Person, PersonUnit>().Where(x => x.personId == personId).FirstOrDefault();
        }

        //get all cache person
        public List<PersonUnit> GetAllPersons()
        {
            var lst = _baseService.Filter<Persons, PersonUnit>().ToList();
            return lst;
        }

        //create
        public async Task<FaceApiCreateTransDto> CreatePersons(PersonsCreateDto personsCreate, bool isExploiting = false)
        {
            var count = await _repository.CountAsync<Persons>(x => x.PersonCode == personsCreate.PersonCode);
            if (count > 0)
            {
                throw new EPSException(EPSExceptionCode.ExistCode, "Mã nhân viên");
            }
            else
            {
                await _baseService.CreateAsync<Persons, PersonsCreateDto>(personsCreate);
            }
            FaceApiCreateTransDto result = new FaceApiCreateTransDto();
            //try
            //{
            //result.status = personsCreate.Status;
            //result.message = "Success";
            result.collection_id = personsCreate.GroupId;
            result.person_id = personsCreate.PersonId;
            result.person_code = personsCreate.PersonCode;
            result.person_name = personsCreate.PersonName;

            result.data = new FaceApiCreateTransDataDto();

            result.data.quality = personsCreate.Quality.Value;
            result.data.face_rectangle = personsCreate.Rect.Split(",").Select(Int32.Parse).ToArray();
            //result.data.face_landmark = personsCreate.Landmark;
            result.data.gender = personsCreate.Gender.Value;
            result.data.age = personsCreate.Age.Value;
            result.data.wearmask = personsCreate.Mask.Value;
            result.data.wearglass = personsCreate.WearGlass.Value;
            result.data.liveness = personsCreate.Liveness.Value;
            result.data.lefteye = personsCreate.LeftEyeClose.Value;
            result.data.righteye = personsCreate.RightEyeClose.Value;
            result.data.orient = personsCreate.Orient.Value;
            result.data.angle3D = personsCreate.Angle.Split(",").Select(float.Parse).ToArray();
            //}
            //catch (Exception ex)
            //{
            //    result.message = ex.ToString();
            //}

            return result;
        }

        //update
        public async Task<FaceApiCreateTransDto> UpdatePersons(PersonsUpdateDto personsUpdateDto)
        {
            var count = await _repository.CountAsync<Persons>(x => x.PersonCode == personsUpdateDto.PersonCode && x.PersonId != personsUpdateDto.PersonId);
            if (count > 0)
            {
                throw new EPSException(EPSExceptionCode.ExistCode, "Mã nhân viên");
            }
            else
            {
                await _baseService.UpdateAsync<Persons, PersonsUpdateDto>(personsUpdateDto.PersonId, personsUpdateDto);
            }

            FaceApiCreateTransDto result = new FaceApiCreateTransDto();
            //try
            //{
            //result.status = personsUpdateDto.Status;
            //result.message = "Success";
            result.collection_id = personsUpdateDto.GroupId;
            result.person_id = personsUpdateDto.PersonId;
            result.person_code = personsUpdateDto.PersonCode;
            result.person_name = personsUpdateDto.PersonName;

            result.data = new FaceApiCreateTransDataDto();

            result.data.quality = personsUpdateDto.Quality.Value;
            result.data.face_rectangle = personsUpdateDto.Rect.Split(",").Select(Int32.Parse).ToArray();
            //result.data.face_landmark = personsUpdateDto.Landmark;
            result.data.gender = personsUpdateDto.Gender.Value;
            result.data.age = personsUpdateDto.Age.Value;
            result.data.wearmask = personsUpdateDto.Mask.Value;
            result.data.wearglass = personsUpdateDto.WearGlass.Value;
            result.data.liveness = personsUpdateDto.Liveness.Value;
            result.data.lefteye = personsUpdateDto.LeftEyeClose.Value;
            result.data.righteye = personsUpdateDto.RightEyeClose.Value;
            result.data.orient = personsUpdateDto.Orient.Value;
            result.data.angle3D = personsUpdateDto.Angle.Split(",").Select(float.Parse).ToArray();
            //}
            //catch (Exception ex)
            //{
            //    result.message = ex.ToString();
            //}

            return result;
        }

        //delete
        public async Task<FaceApiDeleteTransDto> DeletePersons(Guid person_id)
        {
            var rs = await _baseService.FindAsync<Persons, PersonsDeleteDto>(x => x.PersonId == person_id);
            rs.DeleteTime = DateTime.Now;
            rs.Status = 0;
            rs.IsDelete = true;
            await _baseService.UpdateAsync<Persons, PersonsDeleteDto>(person_id, rs);

            FaceApiDeleteTransDto result = new FaceApiDeleteTransDto();
            //try
            //{
            //result.message = "Success";
            //result.status = rs.Status;
            result.person_id = rs.PersonId;
            result.person_code = rs.PersonCode;
            result.person_name = rs.PersonName;
            //}
            //catch (Exception ex)
            //{
            //    result.message = ex.ToString();
            //}
            return result;
        }

        /// <summary>
        /// lấy thông tin person và thông tin collections 
        /// </summary>
        /// <param name="collection_id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<int> GetPersons(CollectionSearchResponse personResponse, ResponseMessage resoponse)
        {
            var count = await _repository.CountAsync<Persons>(x => x.GroupId == personResponse.collection_id);

            var personList = _baseService.Filter<Persons, PersonDetailDtos>(x => x.GroupId == personResponse.collection_id).OrderBy(x => x.RegisterTime).Skip(personResponse.start - 1).Take(personResponse.end - personResponse.start + 1).ToList();

            personResponse.count = count;
            resoponse.message = "Success";
            resoponse.status = 200;
            personResponse.face_data = personList;

            personResponse.count = count;
            var index = personResponse.start - 1;
            var countItem = personResponse.end - personResponse.start + 1;

            return personResponse.collection_id;
        }

        /// <summary>
        /// delete person list by collection id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<int> DeleteByCollections(int id)
        {
            try
            {
                var IdsRemovePerson = _repository.Filter<Persons>(x => x.GroupId == id).Select(x => x.PersonId).ToArray();
                return await _baseService.DeleteAsync<Persons, Guid>(IdsRemovePerson);
            }
            catch (Exception ex)
            {
                throw new EPSException(EPSExceptionCode.DeleteConstraintData, "");
            }
        }

        //Lấy thông tin person và và face available của person đó cache lên ram
        public async Task<List<PersonAndFaceDto>> GetAllPersonAndFace()
        {
            using (var context = _repository.GetDbContext<EPSContext>())
            {
                var lstPersons = await context.Persons
                .Join(context.Faces, p => p.PersonId,
                      f => f.PersonId, (p, f) => new
                      {
                          PersonId = p.PersonId,
                          PersonCode = p.PersonCode,
                          FullName = p.FullName,
                          CompId = p.CompId,
                          DeptId = p.DeptId,
                          Position = p.Position,
                          JobDuties = p.JobDuties,
                          PersonType = p.PersonType,
                          CompType = p.CompType,
                          Status = p.Status,
                          IsDelete = p.IsDelete,
                          FaceUrl = f.FaceUrl,
                          FacePath = f.FacePath,
                          FeaturePath = f.FeaturePath,
                          FStatus = f.Status,
                          FaceImageStatus = f.FaceStatus
                      }).Where(x => x.IsDelete == false && x.FStatus == 1 && x.FaceImageStatus == 2)
                        .Select(x => new PersonAndFaceDto()
                        {
                            PersonId = x.PersonId,
                            PersonCode = x.PersonCode,
                            FullName = x.FullName,
                            CompId = x.CompId,
                            DeptId = x.DeptId,
                            Position = x.Position,
                            JobDuties = x.JobDuties,
                            PersonType = x.PersonType,
                            CompType = x.CompType,
                            Status = x.Status,
                            IsDelete = x.IsDelete.Value,
                            FaceUrl = x.FaceUrl,
                            FacePath = x.FacePath,
                            FeaturePath = x.FeaturePath
                        })
                .ToListAsync();
                return lstPersons;
            }
        }

        public List<PersonAndFaceDto> GetAllPerson (EPSContext context)
        {
            var sqlQuery =
                    @"  select p.PersonId as PersonId,
                            p.PersonCode as PersonCode,
                            IIF (p.FullName is null, '', p.FullName) as FullName,
                            IIF (p.CompId is null, 0, p.CompId) as CompId,
                            IIF (p.DeptId is null, 0, p.DeptId) as DeptId,
                            IIF (p.Position is null, '', p.Position) as Position,
                            IIF (p.JobDuties is null, '', p.JobDuties) as JobDuties,
                            IIF (p.PersonType is null, 0, p.PersonType) as PersonType,
                            IIF (p.CompType is null, 0, p.CompType) as CompType,
                            IIF (p.Status is null, 0, p.Status) as Status,
                            p.IsDelete as IsDelete,
                            IIF (f.FaceUrl is null, '', f.FaceUrl) as FaceUrl,
                            IIF (f.FacePath is null, '', f.FacePath) as FacePath,
                            IIF (f.FeaturePath is null, '', f.FeaturePath) as FeaturePath
                         from Persons p, faces f
                        where p.PersonId = f.PersonId
                          and p.IsDelete = 0
                          and f.status = 1
                          and f.FaceStatus = 2
                    ";

            var lsData = new List<PersonAndFaceDto>();
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;
                context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    // Kiểm tra có kết quả trả về
                    if (reader.HasRows)
                    {
                        // Đọc từng dòng tập kết quả
                        while (reader.Read())
                        {
                            PersonAndFaceDto item = new PersonAndFaceDto();
                            item.PersonId = (Guid)reader["PersonId"];
                            item.PersonCode = (string)reader["PersonCode"];
                            item.FullName = (string)reader["FullName"];
                            item.CompId = (int?)reader["CompId"];
                            item.DeptId = (int?)reader["DeptId"];
                            item.Position = (string)reader["Position"];
                            item.JobDuties = (string)reader["JobDuties"];
                            item.PersonType = (int)reader["PersonType"];
                            item.CompType = (int)reader["CompType"];
                            item.Status = (int)reader["Status"];
                            item.IsDelete = (bool)reader["IsDelete"];
                            item.FaceUrl = (string)reader["FaceUrl"];
                            item.FacePath = (string)reader["FacePath"];
                            item.FeaturePath = (string)reader["FeaturePath"];
                            lsData.Add(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không có dữ liệu trả về");
                    }
                }
            }
            return null;
        }

        public List<Int32> getAllCollectionId (EPSContext context)
        {
            var sqlQuery = @"select Id from Companys where IsDelete = 0 and status = 1";

            var lsData = new List<Int32>();
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;
                context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    // Kiểm tra có kết quả trả về
                    if (reader.HasRows)
                    {
                        // Đọc từng dòng tập kết quả
                        while (reader.Read())
                        {
                            int compId = (int) reader["Id"];
                            lsData.Add(compId);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không có dữ liệu trả về");
                    }
                }
            }
            return lsData;
        }

        //Lấy thông tin person và và face available của person đó cache lên ram
        public async Task<List<PersonAndFaceDto>> GetAllPersonAndFace(EPSContext context)
        {
            using (context)
            {
                var lstPersons = await context.Persons
                .Join(context.Faces, p => p.PersonId,
                      f => f.PersonId, (p, f) => new
                      {
                          PersonId = p.PersonId,
                          PersonCode = p.PersonCode,
                          FullName = p.FullName,
                          CompId = p.CompId,
                          DeptId = p.DeptId,
                          Position = p.Position,
                          JobDuties = p.JobDuties,
                          PersonType = p.PersonType,
                          CompType = p.CompType,
                          Status = p.Status,
                          IsDelete = p.IsDelete,
                          FaceUrl = f.FaceUrl,
                          FacePath = f.FacePath,
                          FeaturePath = f.FeaturePath,
                          FStatus = f.Status,
                          FaceImageStatus = f.FaceStatus
                      }).Where(x => x.IsDelete == false && x.FStatus == 1 && x.FaceImageStatus == 2)
                        .Select(x => new PersonAndFaceDto()
                        {
                            PersonId = x.PersonId,
                            PersonCode = x.PersonCode,
                            FullName = x.FullName,
                            CompId = x.CompId,
                            DeptId = x.DeptId,
                            Position = x.Position,
                            JobDuties = x.JobDuties,
                            PersonType = x.PersonType,
                            CompType = x.CompType,
                            Status = x.Status,
                            IsDelete = x.IsDelete.Value,
                            FaceUrl = x.FaceUrl,
                            FacePath = x.FacePath,
                            FeaturePath = x.FeaturePath
                        })
                .ToListAsync();
                return lstPersons;
            }
        }

        //Lấy thông tin person và và face available của person đó cache lên ram
        public async Task<List<PersonAndFaceDto>> GetAllPersonAndFace(EPSContext context, int collectionId)
        {
            try
            {
                var lstPersons = await context.Persons
                .Join(context.Faces, p => p.PersonId,
                      f => f.PersonId, (p, f) => new
                      {
                          PersonId = p.PersonId,
                          PersonCode = p.PersonCode,
                          FullName = p.FullName,
                          CompId = p.CompId,
                          DeptId = p.DeptId,
                          Position = p.Position,
                          JobDuties = p.JobDuties,
                          PersonType = p.PersonType,
                          CompType = p.CompType,
                          Status = p.Status,
                          IsDelete = p.IsDelete,
                          FaceUrl = f.FaceUrl,
                          FacePath = f.FacePath,
                          FeaturePath = f.FeaturePath,
                          FStatus = f.Status,
                          FaceImageStatus = f.FaceStatus,
                          Passport = p.Passport,
                          PhoneNumber = p.PhoneNumber,
                          Birthday = p.Birthday
                      }).Where(x => x.IsDelete == false && x.FStatus == 1 && x.FaceImageStatus == 2 && x.CompId == collectionId)
                        .Select(x => new PersonAndFaceDto()
                        {
                            PersonId = x.PersonId,
                            PersonCode = x.PersonCode,
                            FullName = x.FullName,
                            CompId = x.CompId,
                            DeptId = x.DeptId,
                            Position = x.Position,
                            JobDuties = x.JobDuties,
                            PersonType = x.PersonType,
                            CompType = x.CompType,
                            Status = x.Status,
                            IsDelete = x.IsDelete.Value,
                            FaceUrl = x.FaceUrl,
                            FacePath = x.FacePath,
                            FeaturePath = x.FeaturePath,
                            Passport = x.Passport,
                            PhoneNumber = x.PhoneNumber,
                            Birthday = (DateTime)x.Birthday
                        })
                .ToListAsync();
                return lstPersons;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        //Lấy thông tin person và và face available của person đó cache lên ram
        public async Task<List<PersonAndFaceDto>> getPersonById(EPSContext context, Guid personId)
        {
            try
            {
                var lstPersons = await context.Persons
                .Join(context.Faces, p => p.PersonId,
                      f => f.PersonId, (p, f) => new
                      {
                          PersonId = p.PersonId,
                          PersonCode = p.PersonCode,
                          FullName = p.FullName,
                          CompId = p.CompId,
                          DeptId = p.DeptId,
                          Position = p.Position,
                          JobDuties = p.JobDuties,
                          PersonType = p.PersonType,
                          CompType = p.CompType,
                          Status = p.Status,
                          IsDelete = p.IsDelete,
                          FaceUrl = f.FaceUrl,
                          FacePath = f.FacePath,
                          FeaturePath = f.FeaturePath,
                          FStatus = f.Status,
                          FaceImageStatus = f.FaceStatus
                      }).Where(x => x.IsDelete == false && x.FStatus == 1 && x.FaceImageStatus == 2 && x.PersonId == personId)
                        .Select(x => new PersonAndFaceDto()
                        {
                            PersonId = x.PersonId,
                            PersonCode = x.PersonCode,
                            FullName = x.FullName,
                            CompId = x.CompId,
                            DeptId = x.DeptId,
                            Position = x.Position,
                            JobDuties = x.JobDuties,
                            PersonType = x.PersonType,
                            CompType = x.CompType,
                            Status = x.Status,
                            IsDelete = x.IsDelete.Value,
                            FaceUrl = x.FaceUrl,
                            FacePath = x.FacePath,
                            FeaturePath = x.FeaturePath
                        })
                .ToListAsync();
                return lstPersons;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
