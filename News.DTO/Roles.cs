using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DTO
{
    public class Roles
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class UserRoles
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
