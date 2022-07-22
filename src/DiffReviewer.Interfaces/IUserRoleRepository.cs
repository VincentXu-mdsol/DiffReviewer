using DiffReviewer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.Interfaces
{
    public interface IUserRoleRepository
    {
        public int CreateUser(string userName);

        public int CreateRole(string roleName);

        public int CreateUserRole(int userId, int roleId);

        public List<RoleDTO> GetRolesForUser(int userId);

        public List<UserDTO> GetAllUsersByRoleId(int roleId);

        public List<UserDTO> GetAllUsersByRoleName(string roleName);

        public int RemoveUserRole(int userId, int roleId);

        public RoleDTO GetRoleByRoleName(string roleName);

        public RoleDTO GetRoleByRoleId(int roleId);

        public List<RoleDTO> GetAllRoles();

        public UserDTO GetUserByUserName(string userName);

        public UserDTO GetUserByUserId(int userId);

        public List<UserDTO> GetAllUsers();
    }
}
