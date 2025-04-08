using CRM_User.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_User.Application.DTO
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Role { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;
        public required string BranchId { get; set; }
    }
}
