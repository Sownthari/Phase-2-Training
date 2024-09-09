using FinanceApplication.Interface;
using FinanceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApplication.Repository
{
    public class AccountRepository : IAccount
    {
        private readonly FinanceDbContext _context;

        public AccountRepository(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task AddAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(string accountNumber)
        {
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }
            _context.Accounts.Remove(existingAccount);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccountByAccountNumber(string accountNumber)
        {
            return await _context.Accounts.Include(u => u.User).FirstOrDefaultAsync(a => a.AccountNumber == accountNumber) ?? throw new KeyNotFoundException("Account not found.");
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _context.Accounts.Include(u => u.User).ToListAsync();
        }

        public async Task UpdateAccount(string accountNumber, Account account)
        {
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }
            _context.Entry(existingAccount).CurrentValues.SetValues(account);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountByUserId(int userId)
        {
            return await _context.Accounts.Include(u => u.User).Where(a => a.UserId == userId).ToListAsync() ?? throw new KeyNotFoundException($"No accounts found with user {userId}");
        }
    }
}
