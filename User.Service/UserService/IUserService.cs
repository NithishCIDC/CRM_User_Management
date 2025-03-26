using CRM_User.Domain.Model;

namespace CRM_User.Service.UserService
{
    public interface IUserService
    {
        Task<bool> CreateUser(User entity);
        Task<User?> GetUserById(Guid id);
        Task<List<User>> GetAllUser();
        bool UpdateUser(User entity);
        bool DeleteUser(Guid id);
    }
}
