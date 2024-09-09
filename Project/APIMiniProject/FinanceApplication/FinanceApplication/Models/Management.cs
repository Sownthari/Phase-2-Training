using System.ComponentModel.DataAnnotations;

namespace FinanceApplication.Models
{
    public class Management
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
