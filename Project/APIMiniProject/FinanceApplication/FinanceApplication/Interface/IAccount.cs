using FinanceApplication.Models;

namespace FinanceApplication.Interface
{
    public interface IAccount
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountByAccountNumber(string accountNumber);
        Task AddAccount(Account account);
        Task UpdateAccount(string accountNumber, Account account);
        Task DeleteAccount(string accountNumber);
        Task<IEnumerable<Account>> GetAccountByUserId(int userId);

    }
}
