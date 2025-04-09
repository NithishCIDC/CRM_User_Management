using System.ComponentModel.DataAnnotations;


namespace CRM_User.Domain.Model
{
    public class Permissions
    {
        [Key]
        public Guid Id { get; set; }
        public string? Permission { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}
