using FinanceApplication.Interface;
using FinanceApplication.Models;

namespace FinanceApplication.Service
{
    public class AccountService : IAccount
    {
        private readonly IAccount _accountRepo;

        public AccountService(IAccount accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task AddAccount(Account account)
        {
            await _accountRepo.AddAccount(account);
        }

        public async Task DeleteAccount(string accountNumber)
        {
            await _accountRepo.DeleteAccount(accountNumber);
        }

        public async Task<Account> GetAccountByAccountNumber(string accountNumber)
        {
            return await _accountRepo.GetAccountByAccountNumber(accountNumber);
        }

        public async Task<IEnumerable<Account>> GetAccountByUserId(int userId)
        {
            return await _accountRepo.GetAccountByUserId(userId);
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepo.GetAllAccounts();
        }

        public async Task UpdateAccount(string accountNumber, Account account)
        {
            await _accountRepo.UpdateAccount(accountNumber, account);
        }
    }
}
