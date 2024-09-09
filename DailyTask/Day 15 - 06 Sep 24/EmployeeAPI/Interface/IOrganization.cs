using EmployeeAPI.Models;

namespace EmployeeAPI.Interface
{
    public interface IOrganization
    {
        Task<IEnumerable<Organization>> GetAllOrganizations();
        Task<Organization> GetOrganizationById(int id);
        Task AddOrganization(Organization organization);
        //Task UpdateOrganization(Organization organization);
        //Task DeleteOrganization(Organization organization);
    }
}
