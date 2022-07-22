using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class UserExDTO : UserDTO
    {
        public List<RoleDTO> Roles { get; set; }
    }
}
