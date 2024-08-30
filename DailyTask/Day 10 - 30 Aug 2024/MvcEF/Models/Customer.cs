using System.ComponentModel.DataAnnotations;

namespace MvcEF.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string? Email { get; set; }
        public long ContactNo { get; set; }
        public string Location { get; set; }
        public DateTime? DOB {  get; set; }

        //Navigation Property
        public IEnumerable<Order> Orders { get; set; }
    }
}
