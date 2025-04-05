using System.Security.Claims;
using CRM_User.Application.DTO;
using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CRM_User.Service.BranchService
{
    public class BranchService(IUnitOfWork unitOfwork, IHttpContextAccessor httpContextAccessor) : IBranchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork = unitOfwork;

        public async Task AddBranch(AddBranchDTO branchEntity)
        {
            Branch branch = branchEntity.Adapt<Branch>();
            branch.Created_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            branch.Created_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            await _unitOfWork.BranchRepository.Add(branch);
        }

        public async Task DeleteBranch(Branch entity)
        {
            await _unitOfWork.BranchRepository.Delete(entity.Id);
        }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            return await _unitOfWork.BranchRepository.GetAll();
        }

        public async Task<Branch?> GetByEmail(string email)
        {
            return await _unitOfWork.BranchRepository.GetByEmail(email);
        }

        public Task<Branch?> GetById(Guid id)
        {
            return _unitOfWork.BranchRepository.GetById(id);
        }

        public async Task UpdateBranch(UpdateBranchDTO entity)
        {
            Branch branch = entity.Adapt<Branch>();
            branch.Updated_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            branch.Updated_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            await _unitOfWork.BranchRepository.Update(branch);
        }
    }
}
