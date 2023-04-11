using EPS.API.Helpers;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.Event.ImageSearch;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.PreOrder;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/event")]
    [Authorize]

    public class EventController : BaseController
    {
        private EventService _eventService;

        private ReportWorkingTimeService _reportWorkingTimeService;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;
        private readonly IHubContext<EventHub> _hubContext;
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private EPSRepository _repository;
        private readonly ILogger<EventController> _logger;

        public EventController(IOptions<AppSettings> appSettings, EPSRepository repository, IWebHostEnvironment webHostEnvironment, EventService eventService,
            ReportWorkingTimeService reportWorkingTimeService, CheckDataService checkDataService, IUserIdentity<int> userIdentity,
            IHubContext<EventHub> hubContext, ILogger<EventController> logger)
        {
            _eventService = eventService;
            _reportWorkingTimeService = reportWorkingTimeService;
            _userIdentity = userIdentity;
            _appSettings = appSettings.Value;
            _repository = repository;
            _checkDataService = checkDataService;
            _hubContext = hubContext;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [HttpPost("listEvent")]
        public async Task<IActionResult> ListEvents(EventGridPagingDto pagingModel)
        {
            //pagingModel.compId = _userIdentity.CompId;
            //ImageSearchInputDataDto b = new ImageSearchInputDataDto();
            //b.collection_id = _userIdentity.CompId;
            //b.threshold = pagingModel.ValueThreshold;
            //b.num_result = 10;
            //b.img_url = "";
            //b.img_base64 = pagingModel.FileData;
            //b.fromDate = pagingModel.filterDateRequestFrom.Value;
            //b.toDate = pagingModel.filterDateRequestTo.Value;
            //var json = JsonConvert.SerializeObject(b);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");

            //var url = _appSettings.ApiSearchEvent;
            //using var client = new HttpClient();
            //var response = await client.PostAsync(url, data);
            //var contents = response.Content.ReadAsStringAsync().Result;
            //var notifyUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageSearchEventDto<Object>>(contents);

            //if (notifyUsers.status == "200" && notifyUsers.data != null)
            //{
            //    var imageSearchEventDto = (List<ImageSearchResultDto>)notifyUsers.data;
            //    if (imageSearchEventDto.Count > 0)
            //    {
            //        int check = 0;
            //        foreach (var item in imageSearchEventDto)
            //        {
            //            if (check == 0)
            //            {
            //                pagingModel.EventId += item.eventId;
            //            }
            //            else
            //            {
            //                pagingModel.EventId += ";" + item.eventId;
            //            }
            //            check++;
            //        }
            //    }
            //}
            return Ok(await _eventService.GetEvents(pagingModel));
        }

        [HttpPost("realTime")]
        public async Task<IActionResult> EventRealTime(object eventId)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(eventId);
            var container = JToken.Parse(json);
            Guid guid;
            if (Guid.TryParse(container["eventId"]?.ToString(), out guid))
            {
                var detailDto = await _eventService.GetRealTime(guid);
                if (detailDto != null)
                {
                    await _hubContext.Clients.All.SendAsync("EventRealTime", detailDto);
                    return Ok(detailDto);
                }
                else
                {
                    throw new Exception("Invalid company");
                }
            }
            else
            {
                throw new Exception("Invalid eventId");
            }
        }

        [HttpGet("list5RealTime")]
        public async Task<IActionResult> List5EventRealTime([FromQuery] EventGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            var detailDto = await _eventService.Get5EventNewest(pagingModel);
            return Ok(detailDto);
        }
        // blacklist
        [HttpGet("listGuessRealTime")]
        public async Task<IActionResult> ListGuessRealTime([FromQuery] EventGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            var detailDto = await _eventService.ListGuessRealTime(pagingModel);
            return Ok(detailDto);
        }

        [HttpGet("listBlackListRealTime")]
        public async Task<IActionResult> ListEventBlackListRealTime([FromQuery] EventGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            var detailDto = await _eventService.GetEventBlackListNewest(pagingModel);
            return Ok(detailDto);
        }

        [HttpGet("listPreOrderRealTime")]
        public async Task<IActionResult> ListEventPreOrderRealTime([FromQuery] EventGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            var detailDto = await _eventService.GetEventPreOrderNewest(pagingModel);
            return Ok(detailDto);
        }

        [HttpGet("countOfPerson/{perId}")]
        public async Task<IActionResult> CountOfPerson(Guid perId)
        {
            return Ok(await _eventService.CountOfPerson(perId));
        }

        [HttpGet("timeDetailPerson/{perId}")]
        public IActionResult ListTimeDetailPerson(Guid perId)
        {
            var timeDto = _eventService.ListTimeDetailPerson(perId);
            return Ok(timeDto);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseMessage> Create(EventCreateDto eventCreateDto)
        {
            ResponseMessage res = new ResponseMessage();

            try
            {
                res = await DoCreateEvent(eventCreateDto);
                return res;
            }
            catch (Exception ex)
            {
                res.status = 500;
                res.message = ex.Message;
                res.data = null;

                if (ex.Message.Contains("Privilege"))
                {
                    res.message = ex.Message;
                }

                if (ex.Message.Contains("Invalid company"))
                {
                    res.message = "Token invalid or expired";
                }
                return res;
            }
        }

        //create
        [CustomAuthorize(PrivilegeList.ManageEvent)]
        public async Task<ResponseMessage> DoCreateEvent(EventCreateDto eventCreateDto)
        {
            ResponseMessage res = new ResponseMessage();

            var eventId = Guid.NewGuid();
            eventCreateDto.EventId = eventId;
            if (eventCreateDto.FileData == null)
            {
                throw new Exception("Image not null");
            }

            try
            {
                byte[] newBytes = Convert.FromBase64String(eventCreateDto.FileData);
                var _file = newBytes;
                if (_file.Length > 0)
                {
                    string contentRootPath = _webHostEnvironment.ContentRootPath;
                    var rootPath = System.IO.Directory.GetParent(contentRootPath);
                    var compId = _userIdentity.CompId;
                    if (eventCreateDto.CompId != null && eventCreateDto.CompId != 0)
                    {
                        compId = eventCreateDto.CompId;
                    }
                    string uploadFolder = _appSettings.UploadImgFolder + compId + "\\";
                    string path = Path.Combine(rootPath.FullName, uploadFolder);

                    //check folder tồn tại hay chưa
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var vehicleIdImg = eventCreateDto.EventId;
                    var filename = vehicleIdImg + ".jpg";
                    var fullPath = Path.Combine(path, filename);
                    System.IO.File.WriteAllBytes(fullPath, newBytes);
                    eventCreateDto.FacePath = "img\\faces\\" + compId + "\\" + filename;
                }
                var personType = await _repository.FindAsync<Person>(x => x.PersonId == eventCreateDto.PersonId);


                if (personType.PersonType != null)
                {
                    eventCreateDto.PersonType = personType.PersonType;
                }
                //Require FeaturePath
                //Tạm bỏ
                //if(eventCreateDto.FeaturePath == null || eventCreateDto.FeaturePath == "")
                //{
                //    throw new Exception("Feature of face image can not be null");
                //}

                await _eventService.CreateEvent(eventCreateDto);

                //call socket
                var detailDto = await _eventService.GetRealTime(eventCreateDto.EventId);
                switch (detailDto.PersonType)
                {
                    case 1:
                        await _hubContext.Clients.All.SendAsync("eventRealTime", detailDto);
                        break;
                    case 2:
                        await _hubContext.Clients.All.SendAsync("eventGuessRealTime", detailDto);
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        await _hubContext.Clients.All.SendAsync("eventBlackListRealTime", detailDto);
                        break;
                    default:
                        break;
                }

                res.status = 200;
                res.message = "Create event success";
                res.data = detailDto.PersonType;
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error save event " + ex.Message);
                throw ex;
            }

            //try
            //{
            //    var listevent = _repository.Find<Event>(x => x.EventId == eventCreateDto.EventId);
            //    var listCustormerevent = _repository.Filter<CustomerEvent>(x => x.PersonId == listevent.PersonId).OrderByDescending(x => x.Id).ToList();
            //    if (listCustormerevent != null && listCustormerevent.Count > 0)
            //    {
            //        var date = DateTime.Today;
            //        if (listCustormerevent[0].AccessDate == date)
            //        {
            //            CustomerEventUpdateDto cusenvent = new CustomerEventUpdateDto();
            //            cusenvent.EventId = listevent.EventId;
            //            cusenvent.PersonId = listevent.PersonId;
            //            cusenvent.Id = listCustormerevent[0].Id;
            //            cusenvent.AccessDate = listevent.AccessDate;
            //            cusenvent.AccessTime = listevent.AccessTime;
            //            cusenvent.CompId = listevent.CompId;
            //            await _eventService.UpdateCustomerEvent(listCustormerevent[0].Id, cusenvent);
            //        }
            //        else
            //        {
            //            CustomerEventCreateDto customerevent = new CustomerEventCreateDto();
            //            customerevent.EventId = eventCreateDto.EventId;
            //            customerevent.PersonId = listevent.PersonId;
            //            customerevent.AccessDate = listevent.AccessDate;
            //            customerevent.AccessTime = listevent.AccessTime;
            //            customerevent.CompId = listevent.CompId;
            //            await _eventService.CreateCustomerEvent(customerevent);
            //        }
            //    }
            //    else
            //    {
            //        CustomerEventCreateDto customerevent = new CustomerEventCreateDto();
            //        customerevent.EventId = eventCreateDto.EventId;
            //        customerevent.PersonId = listevent.PersonId;
            //        customerevent.AccessDate = listevent.AccessDate;
            //        customerevent.AccessTime = listevent.AccessTime;
            //        customerevent.CompId = listevent.CompId;
            //        await _eventService.CreateCustomerEvent(customerevent);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Error customer event " + ex.Message);
            //    throw ex;
            //}

            //call socket
            //try
            //{
            //    EventGridDto detailDto = null;
            //    if (eventCreateDto.Username == null || eventCreateDto.Username == "")
            //    {
            //        detailDto = await _eventService.GetRealTime(eventCreateDto.EventId);
            //    }
            //    else
            //    {
            //        detailDto = await _eventService.GetRealTime(eventCreateDto.EventId, eventCreateDto.Username);
            //    }

            //    if (detailDto != null)
            //    {
            //        switch (detailDto.PersonType)
            //        {
            //            case 1:
            //                await _hubContext.Clients.All.SendAsync("eventRealTime", detailDto);
            //                break;
            //            case 2:
            //                await _hubContext.Clients.All.SendAsync("eventGuessRealTime", detailDto);
            //                break;
            //            case 4:
            //            case 5:
            //            case 6:
            //            case 7:
            //                await _hubContext.Clients.All.SendAsync("eventBlackListRealTime", detailDto);
            //                break;
            //            default:
            //                break;
            //        }

            //        res.status = 200;
            //        res.message = "Create event success";
            //        res.data = eventId;
            //        return res;
            //    }
            //    else
            //    {
            //        throw new Exception("Invalid company");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Error call event realtime " + ex.Message);
            //    throw ex;
            //}

            return res;
        }

        //list all
        [CustomAuthorize(PrivilegeList.ViewEvent, PrivilegeList.ManageEvent)]
        [HttpGet]
        public async Task<IActionResult> GetListEvents([FromQuery] EventGridPagingDto pagingModel)
        {
            if (pagingModel.compId == null)
            {
                pagingModel.compId = _userIdentity.CompId;
            }
            return Ok(await _eventService.GetEvents(pagingModel));
        }

        //detail
        [CustomAuthorize(PrivilegeList.ViewEvent, PrivilegeList.ManageEvent, PrivilegeList.ViewStudentReport, PrivilegeList.ManageStudentReport, PrivilegeList.ViewParentReport, PrivilegeList.ManageParentReport)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            //await _checkDataService.CheckEvent(id, (int)_userIdentity.CompId);
            return Ok(await _eventService.GetEventById(id));
        }

        //delete
        [CustomAuthorize(PrivilegeList.ManageEvent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _checkDataService.CheckEvent(id, (int)_userIdentity.CompId);
            return Ok(await _eventService.DeleteEvent(id));
        }

        //multiple delete 
        [CustomAuthorize(PrivilegeList.ManageEvent)]
        [HttpDelete]
        public async Task<IActionResult> DeleteEvents(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var Events = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                return Ok(await _eventService.DeleteEvent(Events));
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
