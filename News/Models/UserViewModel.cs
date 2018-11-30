﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Image { get; set; }

    }
}