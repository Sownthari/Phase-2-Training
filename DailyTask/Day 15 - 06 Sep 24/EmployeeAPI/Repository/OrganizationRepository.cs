using EmployeeAPI.Interface;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repository
{
    public class OrganizationRepository : IOrganization
    {
        private readonly EmployeeContext _context;
        public OrganizationRepository(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Organization>> GetAllOrganizations()
        {
            return await _context.Organizations.Include(e => e.Employees).ToListAsync();
        }

        public async Task<Organization> GetOrganizationById(int id)
        {
            return await _context.Organizations.Include(e => e.Employees).FirstOrDefaultAsync(o => o.Id == id) ?? new Organization();
        }

        public async Task AddOrganization(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }
    }
}
