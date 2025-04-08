using CRM_User.Domain.Interface;
using CRM_User.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_User.infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(_dbContext);
            BranchRepository = new BranchRepository(_dbContext);
            OrganizationRepository = new OrganizationRepository(_dbContext);
        }
        public IUserRepository UserRepository { get; private set; }
        public IBranchRepository BranchRepository { get; private set; }
        public IOrganizationRepository OrganizationRepository { get; private set; }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
