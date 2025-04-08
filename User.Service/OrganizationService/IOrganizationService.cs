using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_User.Application.DTO;
using CRM_User.Domain.Model;

namespace CRM_User.Service.OrganizationService
{
    public interface IOrganizationService 
    {
        Task AddOrganization(AddOrganizationDTO organization); 
        Task<Organization?> GetByEmail(string email);
        Task<IEnumerable<Organization>> GetAll();
        Task<Organization?> GetById(Guid id);
        Task UpdateOrganization(Organization org,UpdateOraganizationDTO organization);
        Task DeleteOrganization(Organization entity);
    }
}
