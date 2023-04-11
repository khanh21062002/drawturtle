using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.GroupAccess;
using EPS.Service.Helpers;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class GroupAccessService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private CheckDataService _checkDataService;
        private IUserIdentity<int> _userIdentity;


        public GroupAccessService(EPSRepository repository, IMapper mapper, CheckDataService checkDataService, IUserIdentity<int> userIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
            _userIdentity = userIdentity;
            _checkDataService = checkDataService;
        }

        public async Task<PagingResult<GroupAccessGridDto>> GetGroupAccesss(GroupAccessGridPagingDto pagingModel)
        {
            pagingModel.compId = _userIdentity.CompId;
            return await _baseService.FilterPagedAsync<v_GroupAccessDevices, GroupAccessGridDto>(pagingModel);
        }

        public async Task<bool> CreateGroupAccess(GroupAccessCreateDto categoryCreate, bool isExploiting = false)
        {
            if (categoryCreate.CompId.HasValue)
            {
                await _checkDataService.CheckCompIdIsAuthorize(categoryCreate.CompId.Value, (int)_userIdentity.CompId);
            }

            foreach (var item in categoryCreate.LstMachineId)
            {
                GroupAccessCreateDto insItem = new GroupAccessCreateDto();
                insItem.GroupId = categoryCreate.GroupId;
                insItem.MachineId = item;
                insItem.TimeSegId = categoryCreate.TimeSegId;
                insItem.Status = categoryCreate.Status;
                insItem.CompId = categoryCreate.CompId;
                insItem.IsDelete = false;
                insItem.DeptId = categoryCreate.DeptId;
                await _baseService.CreateAsync<GroupAccess, GroupAccessCreateDto>(insItem);
            }

            return true;
        }
        public async Task<int> UpdateDelete1(string id)
        {
            //Cắt chuỗi để lấy được id theo thứ tự
            var lstId = id.Split('_');
            int groupId = int.Parse(lstId[0]);
            int timeSegId = int.Parse(lstId[1]);
            int status = int.Parse(lstId[2]);
            int compId = int.Parse(lstId[3]);
            bool isDelete = false; //Chỉ sửa các bản ghi chưa xóa
            int deptId = int.Parse(lstId[5]);

            await _checkDataService.CheckCompIdIsAuthorize(compId, (int)_userIdentity.CompId);

            var result = await _baseService.FindAsync<v_GroupAccessDevices, GroupAccessDetailDto>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
            && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId);
            //Danh sách cấu hình vùng truy nhập tồn tại
            var listGroupAccess = _repository.Filter<GroupAccess>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
            && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId).Select(i => i.MachineId).ToList();

            foreach (var item in listGroupAccess)
            {
                var groupAccess = await _repository.FindAsync<GroupAccess>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
                && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId && x.MachineId == item);

                groupAccess.IsDelete = true;

                await _repository.UpdateAsync<GroupAccess>(groupAccess);
            }
            return 0;
        }
        public async Task<int> UpdateDeletes1(int[] ids)
        {
            foreach (var item in ids)
            {
                await _checkDataService.CheckGroupAccess(item, (int)_userIdentity.CompId);
                var company = await _baseService.FindAsync<GroupAccess, GroupAccessUpdateDto>(x => x.Id == item);
                if (company != null)
                {
                    company.IsDelete = true;
                    await _baseService.UpdateAsync<GroupAccess, GroupAccessUpdateDto>(company.Id, company);
                }
            }
            return 0;
        }
        public async Task<GroupAccessDetailDto> GetGroupAccessById(string id)
        {
            //Cắt chuỗi để lấy được id theo thứ tự
            var lstId = id.Split('_');
            int groupId = int.Parse(lstId[0]);
            int timeSegId = int.Parse(lstId[1]);
            int status = int.Parse(lstId[2]);
            int compId = int.Parse(lstId[3]);
            bool isDelete = false; //Chỉ sửa các bản ghi chưa xóa
            int deptId = int.Parse(lstId[5]);
            await _checkDataService.CheckCompIdIsAuthorize(compId, (int)_userIdentity.CompId);
            var result = await _baseService.FindAsync<v_GroupAccessDevices, GroupAccessDetailDto>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
            && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId);

            //list thiết bị
            var lstMachineId = _repository.Filter<GroupAccess>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
            && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId).Select(x => x.MachineId).ToList();

            result.LstMachineId = lstMachineId;

            return result;
        }

        public async Task<bool> UpdateGroupAccess(string id, GroupAccessUpdateDto editedGroupAccess)
        {
            //Cắt chuỗi để lấy được id theo thứ tự
            var lstId = id.Split('_');
            int groupId = int.Parse(lstId[0]);
            int timeSegId = int.Parse(lstId[1]);
            int status = int.Parse(lstId[2]);
            int compId = int.Parse(lstId[3]);
            bool isDelete = false; //Chỉ sửa các bản ghi chưa xóa
            int deptId = int.Parse(lstId[5]);

            await _checkDataService.CheckCompIdIsAuthorize(compId, (int)_userIdentity.CompId);
            await _checkDataService.CheckCompIdIsAuthorize((int)editedGroupAccess.CompId, (int)_userIdentity.CompId);
            //Danh sách cấu hình vùng truy nhập tồn tại
            var listGroupAccess = _repository.Filter<GroupAccess>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
            && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId).Select(i => i.MachineId).ToList();

            //Danh sách cần xóa
            var lstDelete = listGroupAccess.Except(editedGroupAccess.LstMachineId).ToList();
            foreach (var item in lstDelete)
            {
                var groupAccess = await _repository.FindAsync<GroupAccess>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
                && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId && x.MachineId == item);

                groupAccess.IsDelete = true;

                await _repository.UpdateAsync<GroupAccess>(groupAccess);
            }
            //Danh sách cần update
            var lstUpdate = listGroupAccess.Intersect(editedGroupAccess.LstMachineId).ToList();
            foreach (var item in lstUpdate)
            {
                var groupAccess = await _repository.FindAsync<GroupAccess>(x => x.GroupId == groupId && x.TimeSegId == timeSegId
              && x.Status == status && x.CompId == compId && x.IsDelete == isDelete && x.DeptId == deptId && x.MachineId == item);

                GroupAccessUpdateDto update_item = new GroupAccessUpdateDto();
                update_item.Id = groupAccess.Id;
                update_item.GroupId = editedGroupAccess.GroupId;
                update_item.MachineId = item;
                update_item.TimeSegId = editedGroupAccess.TimeSegId;
                update_item.Status = editedGroupAccess.Status;
                update_item.CompId = editedGroupAccess.CompId;
                update_item.IsDelete = false;
                update_item.DeptId = editedGroupAccess.DeptId;
                await _baseService.UpdateAsync<GroupAccess, GroupAccessUpdateDto>(groupAccess.Id, update_item);
            }

            //Danh sách cần thêm mới
            var lstCreate = editedGroupAccess.LstMachineId.Except(listGroupAccess).ToList();
            foreach (var item in lstCreate)
            {
                GroupAccessCreateDto insItem = new GroupAccessCreateDto();
                insItem.GroupId = editedGroupAccess.GroupId;
                insItem.MachineId = item;
                insItem.TimeSegId = editedGroupAccess.TimeSegId;
                insItem.Status = editedGroupAccess.Status;
                insItem.CompId = editedGroupAccess.CompId;
                insItem.IsDelete = false;
                insItem.DeptId = editedGroupAccess.DeptId;
                await _baseService.CreateAsync<GroupAccess, GroupAccessCreateDto>(insItem);
            }

            return true;
        }

        public async Task<int> DeleteGroupAccess(int id)
        {
            await _checkDataService.CheckGroupAccess(id, (int)_userIdentity.CompId);
            return await _baseService.DeleteAsync<GroupAccess, int>(id);
        }

        public async Task<int> DeleteGroupAccess(int[] ids)
        {
            foreach (var item in ids)
            {
                await _checkDataService.CheckGroupAccess(item, (int)_userIdentity.CompId);
            }
            return await _baseService.DeleteAsync<GroupAccess, int>(ids);
        }
    }
}
