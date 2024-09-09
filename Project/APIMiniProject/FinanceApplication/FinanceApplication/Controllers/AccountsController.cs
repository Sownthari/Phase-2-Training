using FinanceApplication.Models;
using FinanceApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace FinanceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;
        private static int _userId;

        public AccountsController(AccountService accountService)
        {
            _accountService = accountService;
        }

        private void StoreClaims()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                _userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<Account>> Get()
        {
            return await _accountService.GetAllAccounts();
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IEnumerable<Account>> Get(int userId)
        {
            StoreClaims();
            if (userId == _userId)
            {
                return await _accountService.GetAccountByUserId(userId);
            }
            throw new Exception($"Unauthorized access");
        }

        [HttpGet("{accountNumber}")]
        public async Task<Account> Get(string accountNumber)
        {
            StoreClaims();
            Account account = await _accountService.GetAccountByAccountNumber(accountNumber);
            if (account.UserId != _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            return await _accountService.GetAccountByAccountNumber(accountNumber);

        }

        [HttpPost]
        public async Task Post([FromBody] Account account)
        {
            StoreClaims();
            if (account.UserId == _userId)
            {
                await _accountService.AddAccount(account);
            }
            else throw new Exception($"Unauthorized access");
        }

        [HttpPut("{accountNumber}")]
        public async Task Put(string accountNumber, [FromBody] Account account)
        {
            StoreClaims();
            if (account.UserId == _userId)
            {
                await _accountService.UpdateAccount(accountNumber, account);
            }
            else throw new Exception($"Unauthorized access");
        }

        [HttpDelete("{accountNumber}")]
        public async Task Delete(string accountNumber)
        {
            StoreClaims();
            Account account = await _accountService.GetAccountByAccountNumber(accountNumber);
            if (account!=null && account.UserId == _userId)
            {
                throw new Exception($"Unauthorized access");
            }
            else await _accountService.DeleteAccount(accountNumber);

        }
    }
}
