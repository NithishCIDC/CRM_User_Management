using CRM_User.Application.DTO;
using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using Mapster;
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

        public async Task CreateUser(AddUserDTO entity)
        {
            User user = entity.Adapt<User>();
            user.Created_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            user.Created_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            user.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUser(User user)
        {
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task UpdateUser(User user,UpdateUserDTO entity)
        {
            entity.Adapt(user);
            user.Updated_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            user.Updated_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await _unitOfWork.UserRepository.GetByEmail(email);
        }
        public async Task<bool> IsUserExists(Guid id)
        {
            return await _unitOfWork.UserRepository.IsAny(id);
        }
    }
}
