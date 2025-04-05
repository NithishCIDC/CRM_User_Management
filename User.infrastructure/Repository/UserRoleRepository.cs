using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using CRM_User.infrastructure.Data;

namespace CRM_User.infrastructure.Repository
{
    public class UserRoleRepository : GenericRepository<UserRoles>, IUserRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
