using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_User.Service.UserService
{
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        public async Task<bool> CreateUser(User entity)
        {
            return await unitOfWork.UserRepository.CreateUser(entity);
        }

        public bool DeleteUser(Guid id)
        {
            return unitOfWork.UserRepository.DeleteUser(id);
        }

        public async Task<List<User>> GetAllUser()
        {
            return await unitOfWork.UserRepository.GetAllUser();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await unitOfWork.UserRepository.GetUserById(id);
        }

        public bool UpdateUser(User entity)
        {
            return unitOfWork.UserRepository.UpdateUser(entity);
        }
    }
}
