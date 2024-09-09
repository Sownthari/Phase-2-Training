using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation property
        public ICollection<Employee>? Employees { get; set; }
    }
}
