using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using CRM_User.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_User.infrastructure.Repository
{
    public class BranchRepository(ApplicationDbContext dbContext) : GenericRepository<Branch>(dbContext), IBranchRepository
    {
        
    }
}
