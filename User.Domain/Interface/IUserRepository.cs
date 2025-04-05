using CRM_User.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_User.Domain.Interface
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User entity);
        Task<User?> GetUserById(Guid id);
        Task<List<User>> GetAllUser();
        Task<bool> UpdateUser(User entity);
        Task<bool> DeleteUser(Guid id);
    }
}
