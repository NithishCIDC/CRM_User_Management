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
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        public IUserRepository UserRepository { get; private set; } = new UserRepository(dbContext); 
    }
}
