using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Common;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.PersonQRUpdate;
using EPS.Service.Dtos.PersonQRUpdate.QRCodePerson;
using EPS.Service.Dtos.User;
using EPS.Service.Helpers;
using EPS.Service.Models;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class QRCodePersonService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private IUserIdentity<int> _userIdentity;

        public QRCodePersonService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
        }

        //Lấy all thông tin cập nhật phê duyệt
        public async Task<PagingResult<PersonQRUpdateGridDto>> GetAll(PersonQRUpdateGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<PersonQRUpdate, PersonQRUpdateGridDto>(pagingModel);
        }

        //get qr code current use for update person
        public async Task<QRCodePersonDetailDto> GetQRCodePersonDetail()
        {
            var currentComp = _repository.Filter<Company>(x => x.Id == _userIdentity.CompId).FirstOrDefault();
            var currentDate = DateTime.Now;
            var qrCode = _repository.Filter<PersonQRCode>(x => x.CompId == currentComp.Id && currentDate >= x.DateFrom && currentDate <= x.DateTo).FirstOrDefault();
            if (qrCode != null)
            {
                return await _baseService.Filter<PersonQRCode, QRCodePersonDetailDto>(x => x.CompId == currentComp.Id && currentDate >= x.DateFrom && currentDate <= x.DateTo).FirstAsync();
            }
            else
            {
                QRCodePersonDetailDto newvalue = new QRCodePersonDetailDto();
                return newvalue;
            }
        }

        //create qr code
        public async Task<int> CreateQRCodePerson(QRCodePersonUpdateDto create, bool isExploiting = false)
        {
            create.Id = Guid.NewGuid();
            var currentComp = _repository.Filter<Company>(x => x.Id == _userIdentity.CompId).FirstOrDefault();
            create.CompId = currentComp.Id;
            create.Status = true;
            var currentDate = DateTime.Now;
            var qrCode = _repository.Filter<PersonQRCode>(x => x.CompId == currentComp.Id && currentDate >= x.DateFrom && currentDate <= x.DateTo).FirstOrDefault();
            if (qrCode != null)
            {
                var count = await _repository.CountAsync<PersonQRCode>(x => x.CompId == currentComp.Id && ((x.DateFrom <= create.DateFrom && x.DateTo >= create.DateFrom) || (x.DateFrom <= create.DateTo && x.DateTo >= create.DateTo)));
                if (count > 0)
                {
                    throw new EPSException("QRCodePerson.BackEnd.Service.ExistTime");
                }
                else
                {
                    await _baseService.CreateAsync<PersonQRCode, QRCodePersonUpdateDto>(create);
                }
            }
            else
            {
                if (currentDate >= create.DateFrom && currentDate <= create.DateTo)
                {
                    var count = await _repository.CountAsync<PersonQRCode>(x => x.CompId == currentComp.Id && ((x.DateFrom <= create.DateFrom && x.DateTo >= create.DateFrom) || (x.DateFrom <= create.DateTo && x.DateTo >= create.DateTo)));
                    if (count > 0)
                    {
                        throw new EPSException("QRCodePerson.BackEnd.Service.ExistTime");
                    }
                    else
                    {
                        await _baseService.CreateAsync<PersonQRCode, QRCodePersonUpdateDto>(create);
                    }
                }
                else
                {
                    throw new EPSException("QRCodePerson.BackEnd.Service.CurrentTime");
                }
            }
            return 0;
        }

        //update qr code
        public async Task<int> UpdateQRCodePerson(Guid id, QRCodePersonUpdateDto editDto)
        {
            var currentComp = _repository.Filter<Company>(x => x.Id == _userIdentity.CompId).FirstOrDefault();
            var currentDate = DateTime.Now;
            var checkTime = _baseService.Filter<PersonQRCode, QRCodePersonDetailDto>(x => x.CompId == currentComp.Id && currentDate >= x.DateFrom && currentDate <= x.DateTo).FirstOrDefault();
            if (checkTime != null)
            {
                var idQR = await _baseService.Filter<PersonQRCode, QRCodePersonDetailDto>(x => x.CompId == currentComp.Id && currentDate >= x.DateFrom && currentDate <= x.DateTo).FirstAsync();
                var count = await _repository.CountAsync<PersonQRCode>(x => x.Id != idQR.Id && x.CompId == currentComp.Id && ((x.DateFrom <= editDto.DateFrom && x.DateTo >= editDto.DateFrom) || (x.DateFrom <= editDto.DateTo && x.DateTo >= editDto.DateTo)));
                if (count > 0)
                {
                    throw new EPSException("QRCodePerson.BackEnd.Service.ExistTime");
                }
                else
                {
                    id = idQR.Id;
                    await _baseService.UpdateAsync<PersonQRCode, QRCodePersonUpdateDto>(id, editDto);
                }
            }
            else
            {
                throw new EPSException("QRCodePerson.BackEnd.Service.ExistTime");
            }
            return 0;
        }

        public async Task<int> UpdateFaces(Guid id, FaceUpdateDto dto)
        {
            await _baseService.UpdateAsync<Face, FaceUpdateDto>(id, dto);
            return 0;
        }

        //Get detail person by code
        public async Task<PersonDetailDto> GetPersonDetailByCode(string code, int compId)
        {
            var personId = _repository.Filter<Person>(x => x.CompId == compId && x.PersonCode == code && x.IsDelete == false).Select(x => x.PersonId).FirstOrDefault();
            var detail = await _baseService.FindAsync<Person, PersonDetailDto>(personId);
            return detail;
        }

        //Lấy thông tin nhân viên theo id
        public async Task<PersonDetailDto> GetPersonById(Guid id)
        {
            var person = await _baseService.FindAsync<Person, PersonDetailDto>(id);
            var listPersonGroup = _repository.Filter<PersonGroup>(i => i.PersonId == id && i.IsDelete == false).Select(i => i.GroupId).FirstOrDefault();
            var listPersonGroup1 = _repository.Filter<Cards>(i => i.PersonId == id).Select(i => i.CardNo).ToList();
            person.GroupId = listPersonGroup;
            if (listPersonGroup1 == null || listPersonGroup1.Count() == 0)
            {
                person.Card = "";
            }
            else
            {
                person.Card = listPersonGroup1[0];
            }
            return person;
        }

        //Lấy thông tin qr code update theo id
        public async Task<PersonQRUpdateGridDto> GetQRUpdateById(int id)
        {
            var person = await _baseService.FindAsync<PersonQRUpdate, PersonQRUpdateGridDto>(id);
            return person;
        }

        //Đồng ý phê duyệt
        public async Task<int> ApproveRequest(int[] ids)
        {
            foreach (var item in ids)
            {
                var req = await _baseService.FindAsync<PersonQRUpdate, UpdateImageDto>(x => x.Id == item && x.Status == 1);
                if (req != null)
                {
                    req.Status = 2;
                    await _baseService.UpdateAsync<PersonQRUpdate, UpdateImageDto>(req.Id, req);
                }
                else
                {
                    throw new EPSException("QRCodePerson.BackEnd.Service.InvalidRequest");
                }
            }
            return 0;
        }
        //Từ chối phê duyệt
        public async Task<int> RejectRequest(int[] ids)
        {
            foreach (var item in ids)
            {
                var req = await _baseService.FindAsync<PersonQRUpdate, UpdateImageDto>(x => x.Id == item && x.Status == 1);
                if (req != null)
                {
                    req.Status = 0;
                    await _baseService.UpdateAsync<PersonQRUpdate, UpdateImageDto>(req.Id, req);
                }
                else
                {
                    throw new EPSException("QRCodePerson.BackEnd.Service.InvalidRequest");
                }
            }
            return 0;
        }

        //Tạo dữ liệu mới vào bảng PersonQRUpdate
        public async Task<int> CreateImageUpdate(UpdateImageDto updateDto, bool isExploiting = false)
        {
            await _baseService.CreateAsync<PersonQRUpdate, UpdateImageDto>(updateDto);
            return 0;
        }

        //Cập nhật dữ liệu vào bảng PersonQRUpdate
        public async Task<int> UpdateImageUpdate(int id, UpdateImageDto dto)
        {
            await _baseService.UpdateAsync<PersonQRUpdate, UpdateImageDto>(id, dto);
            return 0;
        }

        //get detail qr by id
        public async Task<QRCodePersonUpdateDto> GetDetailByIdQR(Guid id)
        {
            return await _baseService.FindAsync<PersonQRCode, QRCodePersonUpdateDto>(x => x.Id == id);
        }

        //Danh sách công ty
        public async Task<List<SelectItem>> GetCompanys()
        {
            return await _baseService.Filter<Company, SelectItem>(comp => comp.IsDelete == false).ToListAsync();
        }

        //Danh sách phòng ban
        public async Task<List<DepartmentTreeDto>> GetDepartmentTree()
        {
            var department = await _baseService.Filter<Department, DepartmentTreeDto>(x => x.IsDelete == false && x.Type != AppConstants.DepartmentType.SCHOOL && x.Type != AppConstants.DepartmentType.DEFAULT_PARENT_DEPT).ToListAsync();
            return BuildTree(department, null);
        }
        private List<DepartmentTreeDto> BuildTree(IEnumerable<DepartmentTreeDto> allDepartments, int? rootDepartment = null, bool? showOnlyChild = false)
        {
            var tree = new List<DepartmentTreeDto>();
            List<DepartmentTreeDto> roots = new List<DepartmentTreeDto>();
            if (showOnlyChild == false)
            {
                if (rootDepartment == null)
                {
                    roots = allDepartments.Where(x => x.ParentId == null).ToList();
                }
                else
                {
                    var rootNode = allDepartments.FirstOrDefault(x => x.Id == rootDepartment);
                    if (rootNode != null)
                    {
                        roots.Add(rootNode);
                    }
                }
            }
            else
            {
                if (rootDepartment == null)
                {
                    roots = allDepartments.Where(x => x.ParentId == null).ToList();
                }
                else
                {
                    roots = allDepartments.Where(x => x.ParentId == rootDepartment).ToList();
                }
            }
            foreach (var r in roots)
            {
                tree.Add(r);
                DepartmentTreeDto nextNode = r;
                DepartmentTreeDto currentNode;
                DepartmentTreeDto parentNode;
                int index;
                int level = 1;
                while (nextNode != null)
                {
                    currentNode = nextNode;
                    nextNode = null;
                    foreach (var child in allDepartments.Where(x => x.ParentId == currentNode.Id))
                    {
                        child.Parent = currentNode;
                        currentNode.Children.Add(child);
                        currentNode._children.Add(child);
                    }
                    if (currentNode.Children.Any())
                    {
                        nextNode = currentNode.Children[0];
                        level++;
                    }
                    else
                    {
                        while (currentNode.Parent != null)
                        {
                            parentNode = currentNode.Parent;
                            index = parentNode.Children.IndexOf(currentNode);
                            if (index < parentNode.Children.Count - 1)
                            {
                                nextNode = parentNode.Children[index + 1];
                                break;
                            }
                            else
                            {
                                currentNode = parentNode;
                                level--;
                            }
                        }
                    }
                }
            }
            return tree;
        }
    }
}
