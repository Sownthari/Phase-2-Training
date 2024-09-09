using FinanceApplication.Models;

namespace FinanceApplication.Interface
{
    public interface ITransaction
    {
        
        Task<IEnumerable<Transaction>> GetTransactions(string accountNumber);
        Task<IEnumerable<Transaction>> GetTransactionsByRange(string accountNumber, string range);
        Task<int> GetTransactionCountByRange(string accountNumber, string range); 
        Task AddTransaction(Transaction transaction);
        Task UpdateTransaction(int id, Transaction transaction);
        Task DeleteTransaction(int id);
        Task<object> GetTransactionSummary(string accountNumber, string range);
        Task<IEnumerable<Transaction>> GetTransactionsByUserId(int id, string range);


    }
}
