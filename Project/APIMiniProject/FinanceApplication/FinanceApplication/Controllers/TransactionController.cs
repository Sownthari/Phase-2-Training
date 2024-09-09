using FinanceApplication.Models;
using FinanceApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace FinanceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private readonly AccountService _accountService;
        private readonly UserService _userService;
        private static int _userId;

        public TransactionController(TransactionService transactionService, AccountService accountService, UserService userService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _userService = userService;
        }

        private void StoreClaims()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                _userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        }

        [HttpGet("TransactionByAccountNo/{accountNumber}")]
        public async Task<IEnumerable<Transaction>> Get(string accountNumber)
        {
            StoreClaims();
            Account account = await _accountService.GetAccountByAccountNumber(accountNumber);
            if (account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            return await _transactionService.GetTransactions(accountNumber);
        }

        [HttpGet("TransactionsByRange/{accountNumber}/{range}")]
        public async Task<IEnumerable<Transaction>> Get(string accountNumber, string range)
        {
            StoreClaims();
            Account account = await _accountService.GetAccountByAccountNumber(accountNumber);
            if (account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            return await _transactionService.GetTransactionsByRange(accountNumber, range);
        }

        [HttpGet("TransactionsCountByRange/{range}/{accountNumber}")]
        public async Task<int> GetCount(string accountNumber, string range)
        {
            StoreClaims();
            Account account = await _accountService.GetAccountByAccountNumber(accountNumber);
            if (account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            return await _transactionService.GetTransactionCountByRange(accountNumber, range);
        }

        [HttpGet("TransactionsSummaryByRange/{range}/{accountNumber}")]
        public async Task<object> GetSummary(string accountNumber, string range)
        {
            StoreClaims();
            Account account = await _accountService.GetAccountByAccountNumber(accountNumber);
            if (account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            return await _transactionService.GetTransactionSummary(accountNumber, range);
        }

        [HttpGet("TransactionsByUserId/{range}/{id}")]
        public async Task<IEnumerable<Transaction>> GetByUserId(string range, int id)
        {
            StoreClaims();
            User user = await _userService.GetUserById(id);
            if (user.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            return await _transactionService.GetTransactionsByUserId(id, range);
        }

        [HttpPost]
        public async Task Post([FromBody] Transaction transaction)
        {
            StoreClaims();
            IEnumerable<Account> accounts = await _accountService.GetAccountByUserId(_userId);
            Account account = accounts.FirstOrDefault(a => a.AccountId == transaction.AccountId)!;
            if (account!= null && account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            if(account == null)
            {
                throw new Exception("Account not found");
            }
            if(transaction.TransactionType!.ToLower() == "debit" && account.Balance < transaction.Amount)
            {
                throw new Exception("Insufficient Balance");
            }
            await _transactionService.AddTransaction(transaction);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Transaction transaction)
        {
            StoreClaims();
            IEnumerable<Account> accounts = await _accountService.GetAccountByUserId(_userId);
            Account account = accounts.FirstOrDefault(a => a.AccountId == id)!;
            if (account != null && account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            await _transactionService.UpdateTransaction(id, transaction);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            StoreClaims();
            IEnumerable<Account> accounts = await _accountService.GetAccountByUserId(_userId);
            Account account = accounts.FirstOrDefault(a => a.AccountId == id)!;
            if (account != null && account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            await _transactionService.DeleteTransaction(id);
        }
    }
}
