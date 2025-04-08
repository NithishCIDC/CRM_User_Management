using CRM_User.Application.DTO;
using CRM_User.Domain.Interface;
using CRM_User.Domain.Model;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRM_User.Service.OrganizationService
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrganizationService(IUnitOfWork unitOfwork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfwork = unitOfwork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task AddOrganization(AddOrganizationDTO org)
        {
            Organization organization = org.Adapt<Organization>();
            organization.Created_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value; ;
            organization.Created_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            await _unitOfwork.OrganizationRepository.Add(organization);
            await _unitOfwork.SaveAsync();

        }

        public async Task DeleteOrganization(Organization entity)
        {
            _unitOfwork.OrganizationRepository.Delete(entity);
            await _unitOfwork.SaveAsync();
        }
        public async Task<IEnumerable<Organization>> GetAll()
        {
            return await _unitOfwork.OrganizationRepository.GetAll();
        }

        public async Task<Organization?> GetByEmail(string email)
        {
            return await _unitOfwork.OrganizationRepository.GetByEmail(email);
        }

        public async Task<Organization?> GetById(Guid id)
        {
            return await _unitOfwork.OrganizationRepository.GetById(id);
        }

        public async Task UpdateOrganization(UpdateOraganizationDTO organization)
        {
            Organization org = organization.Adapt<Organization>();
            org.Updated_By = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            org.Updated_At = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            _unitOfwork.OrganizationRepository.Update(org);
            await _unitOfwork.SaveAsync();

        }
    }
}
