﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_User.Domain.Model
{
    public class UserRoles
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
