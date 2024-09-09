using EmployeeAPI.Interface;
using EmployeeAPI.Models;

namespace EmployeeAPI.Service
{
    public class EmployeeService
    {
        private readonly IEmployee _employeeRepo;

        public EmployeeService(IEmployee employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepo.GetAllEmployees();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepo.GetEmployeeById(id);
        }
        public async Task AddEmployee(Employee employee)
        {
            await _employeeRepo.AddEmployee(employee);
        }
    }
}
