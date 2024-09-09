using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApplication.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string? ToAccountNumber { get; set; }
        public string? TransactionType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]

        public Account? Account { get; set; }
    }

}
