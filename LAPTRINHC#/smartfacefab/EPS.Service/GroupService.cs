using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Group;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class GroupService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IUserIdentity<int> _userIdentity;
        private IMapper _mapper;

        public GroupService(EPSRepository repository, IMapper mapper, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _userIdentity = userIdentity;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<GroupGridDto>> GetGroups(GroupGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Group, GroupGridDto>(pagingModel);
        }

        public async Task<int> CreateGroup(GroupCreateDto categoryCreate, bool isExploiting = false)
        {

            var count = await _repository.CountAsync<Group>(x => x.GroupCode == categoryCreate.GroupCode.Trim() && x.IsDelete == false && x.CompId == categoryCreate.CompId );
            if (count > 0)
            {
                var errors1 = "GroupService.Message.GroupCode";
                throw new EPSException(errors1);
            }
            else
            {

                categoryCreate.IsDelete = false;

                await _baseService.CreateAsync<Group, GroupCreateDto>(categoryCreate);
                return categoryCreate.GroupId;
            }
        }

        //Xóa Nhóm người
        public async Task<int> UpdateIsDelete(int id)
        {

            var count = await _repository.CountAsync<PersonGroup>(x => x.GroupId == id && x.IsDelete == false && x.Person.IsDelete == false && x.Group.IsDelete == false);

            var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.GroupId == id && x.IsDelete == false);
            var countAcessTime = await _repository.CountAsync<AccessTimeSeg>(x => x.GroupId == id && x.IsDelete == false);
            if (count > 0 || countGroupAccess > 0 || countAcessTime > 0)
            {
                var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                throw new EPSException(errors1);
            }
            else
            {
                var face = await _baseService.FindAsync<Group, GroupUpdateDto>(x => x.GroupId == id);
                face.IsDelete = true;
                await _baseService.UpdateAsync<Group, GroupUpdateDto>(id, face);

            }
            return 0;

        }

        public async Task<int> UpdateDeletes1(int[] ids)
        {
            foreach (var item in ids)
            {

                var company = await _baseService.FindAsync<Group, GroupUpdateDto>(x => x.GroupId == item);

                var count = await _repository.CountAsync<PersonGroup>(x => x.GroupId == company.GroupId);

                var countGroupAccess = await _repository.CountAsync<GroupAccess>(x => x.GroupId == company.GroupId && x.IsDelete == false);
                var countAcessTime = await _repository.CountAsync<AccessTimeSeg>(x => x.GroupId == company.GroupId && x.IsDelete == false);
                if (count > 0 || countGroupAccess > 0 || countAcessTime > 0)
                {
                    var errors1 = Constant.ErrorMessage.ERROR_DELETE;
                    throw new EPSException(errors1);
                }
                else
                {
                    if (company != null)
                    {
                        company.IsDelete = true;
                        await _baseService.UpdateAsync<Group, GroupUpdateDto>(company.GroupId, company);
                    }
                }
            }
            return 0;
        }
        public async Task<GroupDetailDto> GetGroupById(int id)
        {
            return await _baseService.FindAsync<Group, GroupDetailDto>(id);
        }

        public async Task<int> UpdateGroup(int id, GroupUpdateDto editedGroup)
        {
            var count = await _repository.CountAsync<Group>(x => x.GroupCode == editedGroup.GroupCode.Trim() && x.GroupId != id && x.IsDelete == false && x.CompId == editedGroup.CompId);

            if (count > 0)
            {
                var errors1 = "GroupService.Message.GroupCode";
                throw new EPSException(errors1);
            }
            else
            {
                await _baseService.UpdateAsync<Group, GroupUpdateDto>(id, editedGroup);

            }
            return 0;

        }

        public async Task<int> DeleteGroup(int groupid)
        {
            return await _baseService.DeleteAsync<Group, int>(groupid);
        }

        public async Task<bool> CheckExistCode(int id, int CompId, int deptId, string groupcode)
        {
            int count = 0;
            if (id != 0)
                count = await _repository.CountAsync<Group>(x => x.GroupCode == groupcode.Trim() && x.GroupId != id && x.IsDelete == false && x.CompId == CompId && x.DeptId == deptId);
            else
                count = await _repository.CountAsync<Group>(x => x.GroupCode == groupcode.Trim() && x.IsDelete == false && x.CompId == CompId && x.DeptId == deptId);

            if (count > 0)
                return false;
            else
                return true;
        }

        public async Task<int> DeleteGroup(int[] ids)
        {
            return await _baseService.DeleteAsync<Group, int>(ids);
        }

        //Get All
        public List<GroupGridDto> GetAllGroup()
        {
            return _baseService.Filter<Group, GroupGridDto>(x => x.CompId == _userIdentity.CompId && !x.IsDelete).ToList();
        }
    }
}
