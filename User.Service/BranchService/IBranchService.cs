using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_User.Application.DTO;
using CRM_User.Domain.Model;

namespace CRM_User.Service.BranchService
{
    public interface IBranchService
    {
        Task AddBranch(AddBranchDTO branch);
        Task<Branch?> GetByEmail(string email);
        Task<IEnumerable<Branch>> GetAll();
        Task<Branch?> GetById(Guid id);
        Task UpdateBranch(Branch branch,UpdateBranchDTO entity);
        Task DeleteBranch(Branch entity);
    }
}
