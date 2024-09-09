using FinanceApplication.Interface;
using FinanceApplication.Models;

namespace FinanceApplication.Service
{
    public class TransactionService
    {
        private readonly ITransaction _transactionRepo;

        public TransactionService(ITransaction transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _transactionRepo.AddTransaction(transaction);
        }

        public async Task DeleteTransaction(int id)
        {
            await _transactionRepo.DeleteTransaction(id);
        }

        public async Task<int> GetTransactionCountByRange(string accountNumber, string range)
        {
            return await _transactionRepo.GetTransactionCountByRange(accountNumber, range);
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(string accountNumber)
        {
            return await _transactionRepo.GetTransactions(accountNumber);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByRange(string accountNumber, string range)
        {
            return await _transactionRepo.GetTransactionsByRange(accountNumber, range);
        }

        public async Task<object> GetTransactionSummary(string accountNumber, string range)
        {
            return await _transactionRepo.GetTransactionSummary(accountNumber, range);
        }

        public async Task UpdateTransaction(int id, Transaction transaction)
        {
            await _transactionRepo.UpdateTransaction(id, transaction);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(int id, string range)
        {
            return await _transactionRepo.GetTransactionsByUserId(id, range);
        }
    }
}
