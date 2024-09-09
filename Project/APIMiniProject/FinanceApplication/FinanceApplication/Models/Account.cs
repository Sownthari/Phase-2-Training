using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApplication.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public string? AccountType { get; set; }
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User? User { get; set; } 
        // Navigation property
        public ICollection<Transaction>? Transactions { get; set; }
    }

}
