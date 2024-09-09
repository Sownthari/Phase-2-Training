using System.ComponentModel.DataAnnotations;

namespace FinanceApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }  
        [Required]
        public string Password { get; set; }  
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Navigation property
        public ICollection<Account>? Accounts { get; set; }
    }

}
