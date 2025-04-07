using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace CRM_User.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateUser(User entity)
        {
            entity.Created_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            entity.Created_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            await _unitOfWork.UserRepository.Add(entity);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
           return await _unitOfWork.UserRepository.Delete(id);
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<bool> UpdateUser(User entity)
        {
            entity.Updated_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            entity.Updated_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            return await _unitOfWork.UserRepository.Update(entity);
        }
    }
}
