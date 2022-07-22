using DiffReviewer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.Interfaces
{
    public interface IUserRoleService
    {
        public InsertedDTO CreateUser(string userName);

        public InsertedDTO CreateRole(string roleName);

        public InsertedDTO CreateUserRole(int userId, int roleId);

        public List<RoleDTO> GetRolesForUser(int userId);

        public List<UserDTO> GetAllUsersByRoleId(int roleId);

        public List<UserDTO> GetAllUsersByRoleName(string roleName);

        public DeletedDTO RemoveUserRole(int userId, int roleId);

        public RoleDTO GetRoleByRoleName(string roleName);

        public RoleDTO GetRoleByRoleId(int roleId);

        public List<RoleDTO> GetAllRoles();

        public UserExDTO GetUserByUserName(string userName);

        public UserExDTO GetUserByUserId(int userId);

        public List<UserDTO> GetAllUsers();

        public List<UserExDTO> GetAllUsersWithRoles();
    }
}
