﻿using System.ComponentModel.DataAnnotations;


namespace CRM_User.Domain.Model
{
    public class UserRoles
    {
        [Key]
        public Guid Id { get; set; }
        public string? Role { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = [];
        public ICollection<User>? Users { get; set; }
    }
}
