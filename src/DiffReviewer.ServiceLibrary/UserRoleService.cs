using DiffReviewer.DTO;
using DiffReviewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DiffReviewer.ServiceLibrary
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public InsertedDTO CreateRole(string roleName)
        {
            return new InsertedDTO()
            {
                InsertedId = _userRoleRepository.CreateRole(roleName)
            };
        }

        public InsertedDTO CreateUser(string userName)
        {
            return new InsertedDTO()
            {
                InsertedId = _userRoleRepository.CreateUser(userName)
            };
        }

        public InsertedDTO CreateUserRole(int userId, int roleId)
        {
            return new InsertedDTO()
            {
                InsertedId = _userRoleRepository.CreateUserRole(userId, roleId)
            };
        }

        public List<RoleDTO> GetAllRoles()
        {
            return _userRoleRepository.GetAllRoles();
        }

        public List<UserDTO> GetAllUsers()
        {
            return _userRoleRepository.GetAllUsers();
        }

        public List<UserExDTO> GetAllUsersWithRoles()
        {
            var users = _userRoleRepository.GetAllUsers();
            var userExs = new List<UserExDTO>();
            foreach (var user in users)
            {
                var userEx = new UserExDTO();
                userEx.UserId = user.UserId;
                userEx.UserName = user.UserName;
                userEx.CreatedDate = user.CreatedDate;
                userEx.Roles = _userRoleRepository.GetRolesForUser(user.UserId);

                userExs.Add(userEx);
            }
            return userExs;
        }

        public List<UserDTO> GetAllUsersByRoleId(int roleId)
        {
            return _userRoleRepository.GetAllUsersByRoleId(roleId);
        }

        public List<UserDTO> GetAllUsersByRoleName(string roleName)
        {
            return _userRoleRepository.GetAllUsersByRoleName(roleName);
        }

        public RoleDTO GetRoleByRoleId(int roleId)
        {
            return _userRoleRepository.GetRoleByRoleId(roleId);
        }

        public RoleDTO GetRoleByRoleName(string roleName)
        {
            return _userRoleRepository.GetRoleByRoleName(roleName);
        }

        public List<RoleDTO> GetRolesForUser(int userId)
        {
            return _userRoleRepository.GetRolesForUser(userId);
        }

        public UserExDTO GetUserByUserId(int userId)
        {
            var user = _userRoleRepository.GetUserByUserId(userId);
            var userEx = new UserExDTO();
            userEx.UserId = user.UserId;
            userEx.UserName = user.UserName;
            userEx.CreatedDate = user.CreatedDate;
            userEx.Roles = _userRoleRepository.GetRolesForUser(user.UserId);
            return userEx;
        }

        public UserExDTO GetUserByUserName(string userName)
        {
            var user = _userRoleRepository.GetUserByUserName(userName);
            var userEx = new UserExDTO();
            userEx.UserId = user.UserId;
            userEx.UserName = user.UserName;
            userEx.CreatedDate = user.CreatedDate;
            userEx.Roles = _userRoleRepository.GetRolesForUser(user.UserId);
            return userEx;
        }

        public DeletedDTO RemoveUserRole(int userId, int roleId)
        {
            return new DeletedDTO()
            {
                DeletedId = _userRoleRepository.RemoveUserRole(userId, roleId)
            };
        }
    }
}
