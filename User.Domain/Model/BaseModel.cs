using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace CRM_User.Domain.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
