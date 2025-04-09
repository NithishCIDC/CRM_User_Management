using System.ComponentModel.DataAnnotations;


namespace CRM_User.Domain.Model
{
    public class RolePermission
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public UserRoles? Role { get; set; }
        public Permissions? Permission { get; set; }
    }
}
