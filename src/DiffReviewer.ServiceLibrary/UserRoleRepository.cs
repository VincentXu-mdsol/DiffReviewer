using DiffReviewer.DTO;
using DiffReviewer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DiffReviewer.ServiceLibrary
{
    public class UserRoleRepository :IUserRoleRepository
    {
        private readonly string sqlConnString;

        public UserRoleRepository(IConfiguration configuration)
        {
            sqlConnString = configuration.GetConnectionString("database");
        }

        public int CreateRole(string roleName)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spCreateRole", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", SqlDbType.NVarChar).Value = roleName;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public int CreateUser(string userName)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spCreateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = userName;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public int CreateUserRole(int userId, int roleId)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spCreateUserRole", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.AddWithValue("@RoleId", SqlDbType.Int).Value = roleId;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public List<RoleDTO> GetAllRoles()
        {
            var retval = new List<RoleDTO>();

            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetAllRoles", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];

                        foreach (DataRow row in dt.Rows)
                        {
                            var roleDTO = new RoleDTO();
                            roleDTO.RoleId = Convert.ToInt32(row["RoleId"]);
                            roleDTO.RoleName = row["RoleName"].ToString();
                            roleDTO.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            retval.Add(roleDTO);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public List<UserDTO> GetAllUsers()
        {
            var retval = new List<UserDTO>();

            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetAllUsers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];

                        foreach (DataRow row in dt.Rows)
                        {
                            var userDTO = new UserDTO();
                            userDTO.UserId = Convert.ToInt32(row["UserId"]);
                            userDTO.UserName = row["UserName"].ToString();
                            userDTO.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            retval.Add(userDTO);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public List<UserDTO> GetAllUsersByRoleId(int roleId)
        {
            var retval = new List<UserDTO>();

            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetAllUsersByRoleId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", SqlDbType.Int).Value = roleId;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];

                        foreach (DataRow row in dt.Rows)
                        {
                            var userDTO = new UserDTO();
                            userDTO.UserId = Convert.ToInt32(row["UserId"]);
                            userDTO.UserName = row["UserName"].ToString();
                            userDTO.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            retval.Add(userDTO);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public List<UserDTO> GetAllUsersByRoleName(string roleName)
        {
            var retval = new List<UserDTO>();

            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetAllUsersByRoleName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", SqlDbType.NVarChar).Value = roleName;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];

                        foreach (DataRow row in dt.Rows)
                        {
                            var userDTO = new UserDTO();
                            userDTO.UserId = Convert.ToInt32(row["UserId"]);
                            userDTO.UserName = row["UserName"].ToString();
                            userDTO.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            retval.Add(userDTO);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public RoleDTO GetRoleByRoleId(int roleId)
        {
            var retval = new RoleDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetRoleByRoleId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", SqlDbType.Int).Value = roleId;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        if (dt.Rows.Count > 0)
                        {
                            var row = dt.Rows[0];

                            retval.RoleId = Convert.ToInt32(row["RoleId"]);
                            retval.RoleName = row["RoleName"].ToString();
                            retval.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public RoleDTO GetRoleByRoleName(string roleName)
        {
            var retval = new RoleDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetRoleByRoleName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", SqlDbType.NVarChar).Value = roleName;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        if (dt.Rows.Count > 0)
                        {
                            var row = dt.Rows[0];

                            retval.RoleId = Convert.ToInt32(row["RoleId"]);
                            retval.RoleName = row["RoleName"].ToString();
                            retval.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public List<RoleDTO> GetRolesForUser(int userId)
        {
            var retval = new List<RoleDTO>();

            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetRolesForUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];

                        foreach (DataRow row in dt.Rows)
                        {
                            var roleDTO = new RoleDTO();
                            roleDTO.RoleId = Convert.ToInt32(row["RoleId"]);
                            roleDTO.RoleName = row["RoleName"].ToString();
                            roleDTO.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            retval.Add(roleDTO);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public UserDTO GetUserByUserId(int userId)
        {
            var retval = new UserDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetUserByUserId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        if (dt.Rows.Count > 0)
                        {
                            var row = dt.Rows[0];

                            retval.UserId = Convert.ToInt32(row["UserId"]);
                            retval.UserName = row["UserName"].ToString();
                            retval.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public UserDTO GetUserByUserName(string userName)
        {
            var retval = new UserDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetUserByUserName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = userName;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        if (dt.Rows.Count > 0)
                        {
                            var row = dt.Rows[0];

                            retval.UserId = Convert.ToInt32(row["UserId"]);
                            retval.UserName = row["UserName"].ToString();
                            retval.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public int RemoveUserRole(int userId, int roleId)
        {
            var retVal = 0;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spRemoveUserRole", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.AddWithValue("@RoleId", SqlDbType.Int).Value = roleId;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("RowCount");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }
    }
}
