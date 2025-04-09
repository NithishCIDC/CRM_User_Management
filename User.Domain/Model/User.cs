using CRM_User.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CRM_User.Domain.Model
{
    public class User : BaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public Guid RoleId { get; set; }
        public UserStatus Status { get; set; }
        public Guid BranchId { get; set; }

        [ForeignKey("BranchId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Branch? Branch { get; set; }

        [ForeignKey("RoleId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UserRoles? Role { get; set; }
    }
}
