using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Company;
using EPS.Service.Dtos.Role;
using EPS.Service.Dtos.User;
using EPS.Service.Helpers;
using EPS.Utils.Repository;
using EPS.Utils.Repository.Audit;
using EPS.Utils.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class AuthorizationService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private ILogger<AuthorizationService> _logger;
        private IUserIdentity<int> _userIdentity;
        private Data.EPSContext _context;
        public AuthorizationService(EPSRepository repository, IMapper mapper, UserManager<User> userManager, ILogger<AuthorizationService> logger, IUserIdentity<int> userIdentity, Data.EPSContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _userIdentity = userIdentity;
            _baseService = new EPSBaseService(repository, mapper);
            _context = context;
        }


        public async Task<List<string>> GetUserPrivileges(int userId)
        {
            return await _repository.Filter<UserPrivilege>(x => x.UserId == userId).Select(x => x.PrivilegeId).ToListAsync();
        }

        public async Task<List<string>> GetRolePrivileges(int roleId)
        {
            return await _repository.Filter<RolePrivilege>(x => x.RoleId == roleId).Select(x => x.PrivilegeId).ToListAsync();
        }

        public async Task SaveUserPrivileges(int userId, string[] privileges)
        {
            using (var ts = _repository.BeginTransaction())
            {
                var db = _repository.GetDbContext<EPSContext>();
                var userPrivileges = await db.UserPrivileges.Include(x => x.Privilege).Where(x => x.UserId == userId).ToListAsync();

                foreach (var removingPrivilges in userPrivileges.Where(x => !privileges.Contains(x.PrivilegeId)))
                {
                    db.Remove(removingPrivilges);
                }

                foreach (var newPrivilege in privileges.Where(x => !userPrivileges.Any(y => y.PrivilegeId == x)))
                {
                    db.Add(new UserPrivilege() { UserId = userId, PrivilegeId = newPrivilege });
                }

                await db.SaveChangesAsync();

                ts.Commit();
            }
        }

        public async Task SaveRolePrivileges(int roleId, string[] privileges)
        {
            using (var ts = _repository.BeginTransaction())
            {
                var db = _repository.GetDbContext<EPSContext>();
                var rolePrivileges = await db.RolePrivileges.Include(x => x.Privilege).Where(x => x.RoleId == roleId).ToListAsync();

                foreach (var removingPrivilges in rolePrivileges.Where(x => !privileges.Contains(x.PrivilegeId)))
                {
                    db.Remove(removingPrivilges);
                }

                foreach (var newPrivilege in privileges.Where(x => !rolePrivileges.Any(y => y.PrivilegeId == x)))
                {
                    db.Add(new RolePrivilege() { RoleId = roleId, PrivilegeId = newPrivilege });
                }

                await db.SaveChangesAsync();

                ts.Commit();
            }
        }

        public async Task SaveUserRoles(int userId, List<int> roles = null)
        {
            if (roles == null) roles = new List<int>();

            var db = _repository.GetDbContext<EPSContext>();
            var userRoles = await db.UserRoles.Include(x => x.Role).Where(x => x.UserId == userId).ToListAsync();

            foreach (var removingRole in userRoles.Where(x => !roles.Contains(x.RoleId)))
            {
                db.Remove(removingRole);
            }

            foreach (var newRole in roles.Where(x => !userRoles.Any(y => y.RoleId == x)))
            {
                db.Add(new UserRole() { UserId = userId, RoleId = newRole });
            }

            await db.SaveChangesAsync();
        }

        public async Task<bool> ChangePassword(string userName, ChangePasswordDto model)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception(string.Format(". \n\r", result.Errors.Select(x => x.Description)));
            }
        }

        #region Role
        public async Task<int> CreateRole(RoleCreateDto roleCreate, bool isExploiting = false)
        {
            roleCreate.Status = 1;
            await _baseService.CreateAsync<Role, RoleCreateDto>(roleCreate);
            return roleCreate.Id;
        }

        public async Task<PagingResult<RoleGridDto>> GetRoles(RoleGridPagingDto pagingModel)
        {
            return await _baseService.FilterPagedAsync<Role, RoleGridDto>(pagingModel);
        }

        public async Task<int> DeleteRole(int id)
        {
            return await _baseService.DeleteAsync<Role, int>(id);
        }

        public async Task<int> DeleteRole(int[] ids)
        {
            return await _baseService.DeleteAsync<Role, int>(ids);
        }

        public async Task<RoleDetailDto> GetRoleById(int id)
        {
            return await _baseService.FindAsync<Role, RoleDetailDto>(id);
        }

        public async Task<int> UpdateRole(int id, RoleUpdateDto editedRole)
        {
            return await _baseService.UpdateAsync<Role, RoleUpdateDto>(id, editedRole);
        }
        #endregion

        #region User
        public async Task<int> CreateUser(UserCreateDto newUser)
        {
            //if (!newUser.CompanyId.HasValue)
            //{
            //    throw new EPSException("AuthorizationService.Message.UserNew");
            //}

            ////check quyen api
            //int? curComId = _userIdentity.CompanyId;
            //if (curComId.HasValue)
            //{
            //    var curCompany = await _baseService.FindAsync<Company, CompanyDetailDto>(company => company.Id == curComId.Value);
            //    if (curCompany != null)
            //    {
            //        string hiddenParentField = curCompany.HiddenParentField;

            //        var rsComp = await _baseService.FindAsync<Company, CompanyDetailDto>(company => company.Id == newUser.CompanyId);

            //        if (rsComp != null)
            //        {
            //            if (!rsComp.HiddenParentField.StartsWith(hiddenParentField))
            //            {
            //                throw new EPSException("AuthorizationService.Message.UserPrivileges1");
            //            }
            //        }
            //        else
            //        {
            //            throw new EPSException("AuthorizationService.Message.UserPrivileges2");
            //        }

            //    }
            //}
            //else
            //{
            //    throw new EPSException("AuthorizationService.Message.UserPrivileges2");
            //}
            newUser.IsDelete = false;
            using (var ts = _repository.BeginTransaction())
            {
                var entityUser = _mapper.Map<User>(newUser);

                var result = await _userManager.CreateAsync(entityUser, newUser.Password);
                var userCheck = _userManager.FindByNameAsync(entityUser.UserName).Result;


                if (!result.Succeeded)
                {
                    var errors = string.Join(".", result.Errors.Select(x => x.Description));
                    var errorsCode = string.Join(".", result.Errors.Select(x => x.Code));
                    if (userCheck != null && userCheck.IsDelete == false )
                    {

                        if (errorsCode == "DuplicateUserName")
                        {
                            throw new EPSException("AuthorizationService.Message.DuplicateUserName");
                        }
                        throw new EPSException("AuthorizationService.Message.UserPrivileges2");
                    }

                    throw new EPSException("AuthorizationService.Message.UserPrivileges2");
                }
                else
                {
                    await SaveUserRoles(entityUser.Id, newUser.RoleIds);
                    // Must log manually if not using BaseService
                    _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, default(EventId), new ExtraPropertyLogger("User {username} added new {entity} {identifier}", _userIdentity.Username, typeof(User).Name, entityUser.ToString()).AddProp("data", entityUser), null, ExtraPropertyLogger.Formatter);
                }

                ts.Commit();
                return entityUser.Id;
            }
        }

        public async Task<PagingResult<UserGridDto>> GetUsers(UserGridPagingDto pagingModel)
        {
            if (pagingModel.CompanyId.GetValueOrDefault() <= 0)
            {
                pagingModel.CompanyId = _userIdentity.CompanyId;
            }
            return await _baseService.FilterPagedAsync<User, UserGridDto>(pagingModel);
        }

        public async Task<int> DeleteUser(int id)
        {
            var userRemove = await _repository.FindAsync<User>(x => x.Id == id);
            userRemove.IsDelete = true;
            return await _repository.UpdateAsync<User>(userRemove);
        }

        public async Task<int> DeleteUser(int[] ids)
        {
            return await _baseService.DeleteAsync<User, int>(ids);
        }

        public async Task<UserDetailDto> GetUserById(int id)
        {
            return await _baseService.FindAsync<User, UserDetailDto>(id);
        }

        public async Task<bool> UpdateUser(int id, UserUpdateDto editedUser)
        {
            using (var ts = _repository.BeginTransaction())
            {
                editedUser.IsDelete = false;
                await _baseService.UpdateAsync<User, UserUpdateDto>(id, editedUser);

                await SaveUserRoles(id, editedUser.RoleIds);
                if (!string.IsNullOrEmpty(editedUser.NewPassword))
                {
                    var user = await _baseService.GetDbContext<Data.EPSContext>().Users.FindAsync(id);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var result = await _userManager.ResetPasswordAsync(user, token, editedUser.NewPassword);

                    if (!result.Succeeded)
                    {
                        throw new EPSException(string.Join(".", result.Errors.Select(x => x.Description)));
                    }
                    else
                    {
                        // Must log manually if not using BaseService
                        _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, default(EventId), string.Format("User {0} has changed password for user {1}", _userIdentity.Username, user.ToString()), null, ExtraPropertyLogger.Formatter);
                    }
                }

                ts.Commit();
                return true;
            }
        }
        public async Task<UserDetailDto> GetCurrentUserInfo()
        {
            int curUserId = _userIdentity.UserId;
            return await _baseService.FindAsync<User, UserDetailDto>(curUserId);
        }
        public async Task<bool> PutUpdateUser(int id, UserUpdateDto editedUser)
        {

            using (var ts = _repository.BeginTransaction())
            {
                int curUserId = _userIdentity.UserId;
                if (id != curUserId)
                {
                    throw new EPSException("AuthorizationService.Message.UserPrivileges3 ");
                }

                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == editedUser.Username);
                if (user == null)
                {
                    throw new EPSException("AuthorizationService.Message.UserPrivileges4");
                    //return BadRequest("Invalid user infomation.");
                }

                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, editedUser.exPass);

                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    throw new EPSException("AuthorizationService.Message.PasswordPrivileges");
                    //return BadRequest("Invalid user infomation.");
                }

                //clone các giá trị cũ
                //user.FullName = editedUser.FullName;
                //user.Email = editedUser.Email;
                //user.PhoneNumber = editedUser.PhoneNumber;
                //await _repository.UpdateAsync<User>(user);


                //await SaveUserRoles(id, editedUser.RoleIds);
                if (!string.IsNullOrEmpty(editedUser.NewPassword))
                {
                    var userUpdate = await _baseService.GetDbContext<Data.EPSContext>().Users.FindAsync(id);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var result = await _userManager.ResetPasswordAsync(userUpdate, token, editedUser.NewPassword);

                    if (!result.Succeeded)
                    {
                        throw new EPSException(string.Join(".", result.Errors.Select(x => x.Description)));
                    }
                    else
                    {
                        // Must log manually if not using BaseService
                        _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, default(EventId), string.Format("User {0} has changed password for user {1}", _userIdentity.Username, user.ToString()), null, ExtraPropertyLogger.Formatter);
                    }
                }

                ts.Commit();
                return true;
            }
        }
        #endregion
    }
}
