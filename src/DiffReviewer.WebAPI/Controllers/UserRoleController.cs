using DiffReviewer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiffReviewer.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserRoleController : Controller
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(ILogger<UserRoleController> logger, IUserRoleService userRoleService)
        {
            _logger = logger;
            _userRoleService = userRoleService;
        }

        [HttpPost("{userName}")]
        public JsonResult CreateUser(string userName)
        {
            var retval = _userRoleService.CreateUser(userName);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{roleName}")]
        public JsonResult CreateRole(string roleName)
        {
            var retval = _userRoleService.CreateRole(roleName);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{userId}/{roleId}")]
        public JsonResult CreateUserRole(int userId, int roleId)
        {
            var retval = _userRoleService.CreateUserRole(userId, roleId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{userId}")]
        public JsonResult GetRolesForUser(int userId)
        {
            var retval = _userRoleService.GetRolesForUser(userId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{roleId}")]
        public JsonResult GetAllUsersByRoleId(int roleId)
        {
            var retval = _userRoleService.GetAllUsersByRoleId(roleId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{roleName}")]
        public JsonResult GetAllUsersByRoleName(string roleName)
        {
            var retval = _userRoleService.GetAllUsersByRoleName(roleName);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{userId}/{roleId}")]
        public JsonResult RemoveUserRole(int userId, int roleId)
        {
            var retval = _userRoleService.RemoveUserRole(userId, roleId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{roleName}")]
        public JsonResult GetRoleByRoleName(string roleName)
        {
            var retval = _userRoleService.GetRoleByRoleName(roleName);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{roleId}")]
        public JsonResult GetRoleByRoleId(int roleId)
        {
            var retval = _userRoleService.GetRoleByRoleId(roleId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet]
        public JsonResult GetAllRoles()
        {
            var retval = _userRoleService.GetAllRoles();
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{userName}")]
        public JsonResult GetUserByUserName(string userName)
        {
            var retval = _userRoleService.GetUserByUserName(userName);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{userId}")]
        public JsonResult GetUserByUserId(int userId)
        {
            var retval = _userRoleService.GetUserByUserId(userId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {
            var retval = _userRoleService.GetAllUsers();
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet]
        public JsonResult GetAllUsersWithRoles()
        {
            var retval = _userRoleService.GetAllUsersWithRoles();
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
    }
}
