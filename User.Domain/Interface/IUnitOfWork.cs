using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_User.Domain.Interface
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IBranchRepository BranchRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }
    }
}
