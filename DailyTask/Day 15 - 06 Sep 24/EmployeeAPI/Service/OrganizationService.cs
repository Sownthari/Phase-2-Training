using EmployeeAPI.Interface;
using EmployeeAPI.Models;
using EmployeeAPI.Repository;

namespace EmployeeAPI.Service
{
    public class OrganizationService
    {
        private readonly IOrganization _organizationRepo;

        public OrganizationService(IOrganization organizationRepo)
        {
            _organizationRepo = organizationRepo;
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizations()
        {
            return await _organizationRepo.GetAllOrganizations();
        }

        public async Task<Organization> GetOrganizationById(int id)
        {
            return await _organizationRepo.GetOrganizationById(id);
        }

        public async Task AddOrganization(Organization organization)
        {
            await _organizationRepo.AddOrganization(organization);
        }

    }
}
