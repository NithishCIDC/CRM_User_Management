using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using CRM_User.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_User.infrastructure.Repository
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
         
        }
    }
}
