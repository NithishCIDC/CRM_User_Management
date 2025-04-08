using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM_User.Domain.Model
{
    public class Organization : BaseModel
    {
        public string? Org_Name { get; set; }
        public string? Org_type { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
        public bool IsActive { get; set; } = true;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Branch>? Branches { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<UserRoles>? Roles { get; set; }
    }
}
