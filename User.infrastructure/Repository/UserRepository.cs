using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using CRM_User.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_User.infrastructure.Repository
{
    public class UserRepository(ApplicationDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
    { }
}
