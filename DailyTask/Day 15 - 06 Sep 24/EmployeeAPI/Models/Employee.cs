using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public int OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]

        public Organization? Organization { get; set; }

    }
}
