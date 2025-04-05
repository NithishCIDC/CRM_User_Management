using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using CRM_User.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_User.infrastructure.Repository
{
    public class UserRepository(ApplicationDbContext _dbContext) : IUserRepository
    {
        public async Task<bool> CreateUser(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var entity = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            { 
                _dbContext.Users.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
           return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateUser(User entity)
        {
            _dbContext.Users.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
