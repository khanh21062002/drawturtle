using ArcFaceService.Entity;
using ArcFaceService.Service;
using ArcFaceService.Utils;
using AutoMapper;
using EPS.API.Helpers;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Collection;
using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.FaceApiDto;
using EPS.Service.Dtos.FaceApiDto.Detect;
using EPS.Service.Dtos.FaceApiDto.RemovePerson;
using EPS.Service.Dtos.FaceMatch;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.Persons;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("service/v1")]

    public class FaceAppVer1Controller : BaseController
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private PersonsService _personsService;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;
        private FaceMatchService _faceMatchService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FaceAppVer1Controller> _logger;
        private PersonController _personController;
        private EventService _eventService;
        private EventController _eventController;

        public FaceAppVer1Controller(PersonsService personsService, PersonService personService, EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity,
            FaceMatchService faceMatchService, IWebHostEnvironment webHostEnvironment, ILogger<FaceAppVer1Controller> logger, GroupService groupService,
            DepartmentService departmentService, IOptions<AppSettings> appSettings, CheckDataService checkDataService, EventService eventService, IHubContext<EventHub> hubContext,
            ReportWorkingTimeService reportWorkingTimeService)
        {
            _mapper = mapper;
            _eventService = eventService;
            _webHostEnvironment = webHostEnvironment;
            _baseService = new EPSBaseService(repository, mapper);
            _personsService = personsService;
            _userIdentity = userIdentity;
            _faceMatchService = faceMatchService;
            _logger = logger;
            _eventController = new EventController(appSettings, repository, webHostEnvironment, eventService, reportWorkingTimeService, checkDataService, userIdentity, hubContext, null);
            _personController = new PersonController(personService, userIdentity, groupService, departmentService, webHostEnvironment, appSettings, checkDataService);
        }

        #region statusCollection
        //[HttpGet("status")]
        public ResponseMessage GetStatus()
        {
            ResponseMessage res = new ResponseMessage();
            res.status = 200;
            res.message = "Success";
            res.data = "Hello world";
            return res;
        }
        #endregion

        #region faceCollection
        //[HttpPost("searchFaceFab")]
        public async Task<ResponseMessage> SearchFaceFab(FaceApiSearchDto faceSearchDto)
        {
            ResponseMessage res = new ResponseMessage();
            //try
            //{
            //    try
            //    {
            //        _logger.LogDebug("Request: " + JsonConvert.SerializeObject(faceSearchDto));
            //    }
            //    catch { }

            //    //Kiểm tra engine status
            //    var initFinish = FaceServer.Instance.getCacheFeatureStatus();
            //    if (!initFinish)
            //    {
            //        res.status = 500;
            //        res.message = "FACE ENGINE IS LOADING...";
            //        res.data = null;
            //        return res;
            //    }

            //    //Tìm kiếm face
            //    res = SearchFace(faceSearchDto);
            //    if (res.status != 200)
            //    {
            //        return res;
            //    }

            //    FaceApiSearchTransDto resultSearch = (FaceApiSearchTransDto)res.data;
            //    List<FaceApiSearchResultDto> lsPerson = resultSearch.result;
            //    FaceApiSearchTransDataDto faceBiometric = resultSearch.input_data;
            //    if (resultSearch != null)
            //    {
            //        _logger.LogDebug("Response: " + JsonConvert.SerializeObject(resultSearch));
            //    }

            //    var faceImageBase64 = (faceBiometric != null && faceBiometric.imageBase64 != null) ? faceBiometric.imageBase64 : faceSearchDto.img_base64;
            //    try
            //    {
            //        _logger.LogDebug("Avatar image");
            //        _logger.LogDebug(faceImageBase64);
            //    }
            //    catch { }

            //    var personId = Guid.NewGuid();
            //    float maxScore = 0;
            //    if (lsPerson == null || lsPerson.Count == 0)
            //    {
            //        _logger.LogDebug("Person not exits -> create new person " + personId);

            //        var feature = faceBiometric.feature;
            //        if (feature == null || feature.Length == 0)
            //        {
            //            res.status = 400;
            //            _logger.LogDebug("Face not exits in image");
            //            res.message = "Face not exits in image";
            //            res.data = null;
            //            return res;
            //        }

            //        try
            //        {
            //            //Tạo mới person
            //            PersonCreateDto personCreate = new PersonCreateDto();
            //            personCreate.CompId = faceSearchDto.collection_id.Value;
            //            personCreate.DeptId = faceSearchDto.dept_id.Value;
            //            personCreate.PersonId = personId;
            //            personCreate.Gender = faceBiometric.Gender;
            //            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            //            personCreate.FullName = "USER " + timeStamp;
            //            personCreate.FileData = faceImageBase64;
            //            personCreate.Status = 1;
            //            personCreate.PersonType = 2;
            //            personCreate.IsDelete = false;

            //            var face = new FaceCreateDto();
            //            face.FeaturePath = Convert.ToBase64String(feature);
            //            face.FileData = faceImageBase64;
            //            face.Status = 1;
            //            var faces = new List<FaceCreateDto>();
            //            faces.Add(face);
            //            personCreate.ListFace = faces;

            //            _logger.LogDebug("Start create person " + personCreate.PersonId);
            //            await _personController.Create(personCreate, faceSearchDto.quality, faceBiometric.Quality.Value);
            //            _logger.LogDebug("Create person success " + personCreate.PersonId);

            //            //add face feature to engine cache
            //            _logger.LogDebug("Start create face " + personCreate.PersonId);
            //            var personUnit = new PersonUnit();
            //            personUnit.groupId = personCreate.CompId.Value;
            //            personUnit.personId = personCreate.PersonId.ToString();
            //            personUnit.personCode = personCreate.PersonCode;
            //            personUnit.personName = personCreate.FullName;
            //            personUnit.facePath = "";
            //            personUnit.faceFeature = feature;
            //            FaceServer.Instance.addFeature(personUnit);
            //            _logger.LogDebug("Create face success " + personCreate.PersonId);
            //            //end

            //            //Person result
            //            var lsResult = new List<FaceApiSearchResultDto>();
            //            var personResult = new FaceApiSearchResultDto();
            //            personResult.person_id = personCreate.PersonId;
            //            personResult.person_code = personCreate.PersonCode;
            //            personResult.person_name = personCreate.FullName;
            //            personResult.similary = 1f;
            //            personResult.face_data = "";
            //            lsResult.Add(personResult);

            //            resultSearch.result = lsResult;
            //            //end

            //            _logger.LogDebug("Success");
            //        }
            //        catch (Exception ex)
            //        {
            //            _logger.LogError(ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        foreach (FaceApiSearchResultDto item in lsPerson)
            //        {
            //            if (item.similary.Value > maxScore)
            //            {
            //                maxScore = item.similary.Value;
            //                personId = item.person_id;
            //            }
            //        }
            //    }

            //    //Insert bảng event
            //    _logger.LogDebug("Start save event");
            //    EventCreateDto eventCreate = new EventCreateDto();
            //    eventCreate.PersonId = personId;
            //    eventCreate.FileData = faceImageBase64;
            //    eventCreate.AccessDate = DateTime.Now;
            //    eventCreate.AccessTime = DateTime.Now;
            //    eventCreate.AccessType = "1";
            //    eventCreate.CompId = faceSearchDto.collection_id.Value;
            //    eventCreate.AreaId = 0;
            //    eventCreate.MachineId = 0;
            //    eventCreate.Temperature = 0;

            //    if (faceBiometric != null)
            //    {
            //        if (faceBiometric.Quality.Value < faceSearchDto.quality)
            //        {
            //            eventCreate.PersonId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            //            //res.status = 400;
            //            //res.message = "Quality is out of range";
            //            //res.data = null;
            //            //return res;
            //        }

            //        eventCreate.Gender = faceBiometric.Gender;
            //        eventCreate.Age = faceBiometric.Age;
            //        eventCreate.WearMask = faceBiometric.WearMask;
            //        eventCreate.Quality = faceBiometric.Quality.Value;
            //    }
            //    else
            //    {
            //        FaceApiSearchTransDto rsss = (FaceApiSearchTransDto)res.data;
            //        eventCreate.Age = rsss.input_data.Age;
            //        eventCreate.Gender = rsss.input_data.Gender;
            //        eventCreate.WearMask = rsss.input_data.WearMask;
            //        eventCreate.Quality = rsss.input_data.Quality.Value;

            //        if (rsss.input_data.Quality.Value < faceSearchDto.quality)
            //        {
            //            eventCreate.PersonId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            //            //res.status = 400;
            //            //res.message = "Quality is out of range";
            //            //res.data = null;
            //            //return res;
            //        }
            //    }
            //    eventCreate.ScoreMatch = maxScore;
            //    eventCreate.ErrorCode = "0003";
            //    eventCreate.Status = 1;

            //    await _eventController.Create(eventCreate);

            //    if (faceBiometric.imageBase64 != null)
            //    {
            //        faceBiometric.imageBase64 = null;
            //    }

            //    if (faceBiometric.feature != null)
            //    {
            //        faceBiometric.feature = null;
            //    }
            //    _logger.LogDebug("Save event success\n");
            //}
            //catch (Exception ex)
            //{
            //    _logger.Log(LogLevel.Error, ex, ex.Message);

            //    res.status = 500;
            //    res.message = ex.Message;
            //    res.data = null;

            //    if (ex.Message.Contains("Privilege"))
            //    {
            //        res.message = ex.Message;
            //    }
            //    return res;
            //}

            return res;
        }
        #endregion

        #region faceCollection
        [HttpPost("searchFace")]
        public ResponseMessage SearchFace(FaceApiSearchDto faceSearchDto)
        {
            ResponseMessage res = new ResponseMessage();

            try
            {
                // thông báo khi bỏ trống cả 2 trường img_url và img_base64
                if (string.IsNullOrEmpty(faceSearchDto.img_url) && string.IsNullOrEmpty(faceSearchDto.img_base64))
                {
                    res.data = null;
                    res.status = 400;
                    res.message = "img_url or img_base64 is required";

                    return res;
                }

                if (faceSearchDto.collection_id == null)
                {
                    res.data = null;
                    res.status = 400;
                    res.message = "Collection id can not null";
                    return res;
                }

                if (faceSearchDto.dept_id == null)
                {
                    res.data = null;
                    res.status = 400;
                    res.message = "Department id can not null";
                    return res;
                }

                float threshold = 0.75f;
                if (faceSearchDto.threshold != null)
                {
                    threshold = faceSearchDto.threshold.Value;
                }

                var compId = faceSearchDto.collection_id.Value;
                var lsCollection = new List<int>();
                lsCollection.Add(compId);

                FaceResult<FaceFeatureUnit, List<PersonResult>> faceResult = new FaceResult<FaceFeatureUnit, List<PersonResult>>();
                if (!string.IsNullOrEmpty(faceSearchDto.img_url))
                {
                    faceResult = FaceServer.Instance.searchFace(faceSearchDto.img_url, FaceType.URL, lsCollection, threshold, true);
                }
                else
                {
                    faceResult = FaceServer.Instance.searchFace(faceSearchDto.img_base64, FaceType.BASE64, lsCollection, threshold, true);
                }

                //Trả lỗi từ Engine
                if (faceResult.status != 200)
                {
                    res.data = null;
                    res.status = faceResult.status;
                    res.message = faceResult.error;

                    return res;
                }

                FaceApiSearchTransDto resultSearch = new FaceApiSearchTransDto();
                resultSearch.input_data = new FaceApiSearchTransDataDto();
                resultSearch.input_data.Quality = faceResult.input_data.quality;

                resultSearch.input_data.face_rectangle = new int[4];
                resultSearch.input_data.face_rectangle[0] = faceResult.input_data.faceRect.left;
                resultSearch.input_data.face_rectangle[1] = faceResult.input_data.faceRect.top;
                resultSearch.input_data.face_rectangle[2] = faceResult.input_data.faceRect.right;
                resultSearch.input_data.face_rectangle[3] = faceResult.input_data.faceRect.bottom;

                resultSearch.input_data.Gender = faceResult.input_data.gender;
                resultSearch.input_data.Age = faceResult.input_data.age;
                resultSearch.input_data.WearMask = faceResult.input_data.mask;
                resultSearch.input_data.WearGlass = faceResult.input_data.wearGlass;
                resultSearch.input_data.Liveness = faceResult.input_data.liveness;
                resultSearch.input_data.LeftEye = faceResult.input_data.leftEyeClose;
                resultSearch.input_data.RightEye = faceResult.input_data.rightEyeClose;
                resultSearch.input_data.Orient = faceResult.input_data.orient;
                resultSearch.input_data.Angle3D = faceResult.input_data.angle3D;

                if (faceResult.input_data.feature != null && faceResult.input_data.feature.Length > 0)
                {
                    resultSearch.input_data.feature = faceResult.input_data.feature;
                }

                if (faceResult.input_data.featureBase64 != null && faceResult.input_data.featureBase64.Length > 0)
                {
                    resultSearch.input_data.feature = System.Convert.FromBase64String(faceResult.input_data.featureBase64);
                }

                if (faceResult.input_data.imageBase64 != null && faceResult.input_data.imageBase64.Length > 0)
                {
                    resultSearch.input_data.imageBase64 = faceResult.input_data.imageBase64;
                }

                var lsResult = new List<FaceApiSearchResultDto>();
                var resultResponse = faceResult.result.OrderBy(x => x.scoreMatching).ToList();

                //Fix số bản ghi trả ra tối đa chỉ 10
                if (!faceSearchDto.num_result.HasValue || faceSearchDto.num_result > 10)
                {
                    faceSearchDto.num_result = 10;
                }

                int countRes = resultResponse.Count();

                if (countRes >= faceSearchDto.num_result)
                    countRes = faceSearchDto.num_result.Value;

                for (int i = 0; i < countRes; i++)
                {
                    var result = new FaceApiSearchResultDto();
                    result.person_id = Guid.Parse(resultResponse[i].personId);
                    result.person_code = resultResponse[i].personCode;
                    result.person_name = resultResponse[i].personName;
                    result.similary = resultResponse[i].scoreMatching;
                    result.face_data = resultResponse[i].facePath;
                    result.passpord = resultResponse[i].passpord;
                    result.phonenumber = resultResponse[i].phonenumber;
                    result.birthday = resultResponse[i].birthday;
                    lsResult.Add(result);
                }
                resultSearch.result = lsResult;

                res.status = 200;
                res.message = "Success";
                res.data = resultSearch;

                return res;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                res.status = 500;
                res.message = "INTERNAL_ERROR";
                res.data = null;

                return res;
            }
        }
        #endregion        

        #region Detect
        [HttpPost("detect")]
        public ResponseMessage DetectFace(DetectFaceDto detectDto)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                FaceResult<String, FaceFeatureUnit> analyze;
                if (string.IsNullOrEmpty(detectDto.img_url) && string.IsNullOrEmpty(detectDto.img_base64))
                {
                    res.status = 500;
                    res.message = "Input image must have value";
                    res.data = null;
                    return res;
                }

                if (!string.IsNullOrEmpty(detectDto.img_url))
                {
                    analyze = FaceServer.Instance.analyzeFace(detectDto.img_url, 1);
                }
                else
                {
                    analyze = FaceServer.Instance.analyzeFace(detectDto.img_base64, 3);
                }

                if (analyze.error != null)
                {
                    res.status = analyze.status;
                    res.message = analyze.error;
                    res.data = null;
                    return res;
                }
                else
                {
                    DetectFaceTransDataDto result = new DetectFaceTransDataDto();

                    result.quality = analyze.result.quality;

                    result.face_rectangle = new int[4];
                    result.face_rectangle[0] = analyze.result.faceRect.left;
                    result.face_rectangle[1] = analyze.result.faceRect.top;
                    result.face_rectangle[2] = analyze.result.faceRect.right;
                    result.face_rectangle[3] = analyze.result.faceRect.bottom;

                    result.age = analyze.result.age;
                    result.liveness = analyze.result.liveness;
                    result.gender = analyze.result.gender;
                    result.wearmask = analyze.result.mask;
                    result.wearglass = analyze.result.wearGlass;
                    result.lefteye = analyze.result.leftEyeClose;
                    result.righteye = analyze.result.rightEyeClose;
                    result.orient = analyze.result.orient;

                    result.angle3D = new float[3];
                    result.angle3D[0] = analyze.result.angle3D[0];
                    result.angle3D[1] = analyze.result.angle3D[1];
                    result.angle3D[2] = analyze.result.angle3D[2];

                    result.feature = analyze.result.feature;
                    result.imageBase64 = analyze.result.imageBase64;

                    switch (analyze.status)
                    {
                        case 500:
                            res.status = analyze.status;
                            res.message = analyze.error;
                            break;
                        case 406:
                            res.status = analyze.status;
                            res.message = analyze.error;
                            break;
                        case 200:
                            res.status = analyze.status;
                            res.message = "Success";
                            res.data = result;
                            break;
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                res.status = 500;
                res.message = "INTERNAL_ERROR";
                res.data = null;

                return res;
            }
        }
        #endregion        

        #region Matching
        [HttpPost("match")]
        public async Task<ResponseMessage> GetFaceAnalyze(FaceMatchCreateDto faceCreateDto)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                DateTime dtStart = DateTime.Now;
                Console.WriteLine("Process " + dtStart.ToString("dd/MM/yyyy hh:mm:ss.fff"));

                FaceResult<Object, float> compare;
                if (!string.IsNullOrEmpty(faceCreateDto.img1_url) && !string.IsNullOrEmpty(faceCreateDto.img2_url))
                {
                    compare = FaceServer.Instance.compareFace(faceCreateDto.img1_url, faceCreateDto.img2_url, FaceType.URL, FaceType.URL);
                }
                else
                {
                    if (!string.IsNullOrEmpty(faceCreateDto.img1_base64) && !string.IsNullOrEmpty(faceCreateDto.img2_url))
                    {
                        compare = FaceServer.Instance.compareFace(faceCreateDto.img1_base64, faceCreateDto.img2_url, FaceType.BASE64, FaceType.URL);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(faceCreateDto.img1_url) && !string.IsNullOrEmpty(faceCreateDto.img2_base64))
                        {
                            compare = FaceServer.Instance.compareFace(faceCreateDto.img1_url, faceCreateDto.img2_base64, FaceType.URL, FaceType.BASE64);
                        }
                        else
                        {
                            compare = FaceServer.Instance.compareFace(faceCreateDto.img1_base64, faceCreateDto.img2_base64, FaceType.BASE64, FaceType.BASE64);
                        }
                    }
                }

                if (compare.error != null)
                {
                    Console.WriteLine("Error " + compare.error);
                    res.status = compare.status;
                    res.message = compare.error;
                    res.data = null;
                }
                else
                {
                    MatchCreateDto matchCreateDto = new MatchCreateDto();
                    matchCreateDto.Similary = compare.result;
                    //await _faceMatchService.CreateMatchs(matchCreateDto);
                    MatchDetailDto result = new MatchDetailDto();

                    res.status = 200;
                    res.message = "Success";
                    result.similary = compare.result;
                    res.data = compare.result;
                }

                DateTime dtEnd = DateTime.Now;
                Console.WriteLine("Response " + dtEnd.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                TimeSpan span = dtEnd - dtStart;
                int ms = (int)span.TotalMilliseconds;
                Console.WriteLine("Total " + Thread.CurrentThread.Name + " in " + ms);

                return res;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                res.status = 500;
                res.message = "INTERNAL_ERROR";
                res.data = null;

                return res;
            }
        }
        #endregion

        #region Remove
        [HttpPost("remove")]
        public ResponseMessage RemovePerson(RemovePersonDto removePersonDto)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                if (removePersonDto.person_id == null || removePersonDto.person_id == "" && removePersonDto.collection_id == 0)
                {
                    res.status = 400;
                    res.message = "Parameter input is missing";
                    return res;
                }

                if (removePersonDto.person_id != null && removePersonDto.person_id != "" && removePersonDto.collection_id != 0)
                {
                    PersonUnit personUnit = _personsService.GetPersons(removePersonDto.person_id);
                    if (personUnit == null)
                    {
                        res.status = 400;
                        res.message = "Person with id is not found";
                        return res;
                    }

                    if (removePersonDto.collection_id != 0 && personUnit.groupId != 0 && removePersonDto.collection_id != personUnit.groupId)
                    {
                        res.status = 400;
                        res.message = "Person is not belong collection id group";
                        return res;
                    }

                    FaceServer.Instance.removeFeature(personUnit);
                    res.status = 200;
                    res.message = "Success";
                    return res;
                }

                if (removePersonDto.person_id != null && removePersonDto.person_id != "" && removePersonDto.collection_id == 0)
                {
                    PersonUnit personUnit = _personsService.GetPersons(removePersonDto.person_id);
                    if (personUnit == null)
                    {
                        res.status = 400;
                        res.message = "Person with id is not found";
                        return res;
                    }

                    FaceServer.Instance.removeFeature(personUnit);
                    res.status = 200;
                    res.message = "Success";
                    return res;
                }

                if ((removePersonDto.person_id == null || removePersonDto.person_id == "") && removePersonDto.collection_id != 0)
                {
                    FaceServer.Instance.removeGroupFeature(removePersonDto.collection_id);
                    res.status = 200;
                    res.message = "Success";
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                res.status = 500;
                res.message = "INTERNAL_ERROR";
            }
            return res;
        }
        #endregion  
    }
}

