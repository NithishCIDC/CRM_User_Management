using CRM_User.Domain.Model;

namespace CRM_User.Service.UserService
{
    public interface IUserService
    {
        Task<bool> CreateUser(User entity);
        Task<User?> GetUserById(Guid id);
        Task<List<User>> GetAllUser();
        Task<bool> UpdateUser(User entity);
        Task<bool> DeleteUser(Guid id);
    }
}
