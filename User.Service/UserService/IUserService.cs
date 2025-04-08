using CRM_User.Application.DTO;
using CRM_User.Domain.Model;

namespace CRM_User.Service.UserService
{
    public interface IUserService
    {
        Task<bool> IsUserExists(Guid Id);
        Task CreateUser(AddUserDTO entity);
        Task<User?> GetUserById(Guid id);
        Task<User?> GetByEmail(string email);
        Task<List<User>> GetAllUser();
        Task UpdateUser(User user,UpdateUserDTO entity);
        Task DeleteUser(User user);
    }
}
