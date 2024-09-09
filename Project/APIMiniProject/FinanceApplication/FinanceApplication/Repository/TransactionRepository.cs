using FinanceApplication.Interface;
using FinanceApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using Transaction = FinanceApplication.Models.Transaction;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;

namespace FinanceApplication.Repository
{
    public class TransactionRepository : ITransaction
    {
        private readonly FinanceDbContext _context;

        public TransactionRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == transaction.AccountId);
            if (account != null)
            {
                if (transaction.TransactionType == "Credit")
                {
                    account.Balance += transaction.Amount;
                }
                else if (transaction.TransactionType == "Debit")
                {
                    account.Balance -= transaction.Amount;
                }

                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
                var userEmail = account.User!.Email;
                await SendTransactionEmail(userEmail, transaction);
            }
        }

        public async Task DeleteTransaction(int id)
        {
            var existingTransaction = await _context.Transactions.FindAsync(id);
            if (existingTransaction == null)
            {
                throw new KeyNotFoundException("Transaction not found.");
            }
            _context.Transactions.Remove(existingTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(string accountNumber)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                return Enumerable.Empty<Transaction>();
            }

            return await _context.Transactions
                .Include(a => a.Account).Where(t => t.AccountId == account.AccountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByRange(string accountNumber, string range)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                return Enumerable.Empty<Transaction>();
            }

            DateTime rangeStart;
            DateTime rangeEnd = DateTime.Now;

            switch (range.ToLower())
            {
                case "last day":
                    rangeStart = DateTime.Today.AddDays(-1);
                    rangeEnd = DateTime.Today.AddTicks(-1);
                    break;
                case "last week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7);
                    rangeEnd = rangeStart.AddDays(6).AddTicks(-1);
                    break;
                case "last month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    rangeEnd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddTicks(-1);
                    break;
                case "last year":
                    rangeStart = new DateTime(DateTime.Today.Year - 1, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year, 1, 1).AddTicks(-1);
                    break;
                case "today":
                    rangeStart = DateTime.Today;
                    rangeEnd = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;

                case "this week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    rangeEnd = rangeStart.AddDays(7).AddTicks(-1);
                    break;

                case "this month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    rangeEnd = rangeStart.AddMonths(1).AddTicks(-1);
                    break;

                case "this year":
                    rangeStart = new DateTime(DateTime.Today.Year, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year + 1, 1, 1).AddTicks(-1);
                    break;
                default:
                    throw new ArgumentException("Invalid range specified");
            }

            return await _context.Transactions
                .Include(a => a.Account).Where(t => t.AccountId == account.AccountId && t.TransactionDate >= rangeStart && t.TransactionDate <= rangeEnd)
                .ToListAsync();
        }


        public async Task UpdateTransaction(int id, Transaction transaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(id);
            if (existingTransaction == null)
            {
                throw new KeyNotFoundException("Transaction not found.");
            }
           
            _context.Entry(existingTransaction).CurrentValues.SetValues(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTransactionCountByRange(string accountNumber, string range)
        {
            DateTime rangeStart;
            DateTime rangeEnd = DateTime.Now;
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                return 0;
            }

            switch (range.ToLower())
            {
                case "last day":
                    rangeStart = DateTime.Today.AddDays(-1);
                    rangeEnd = DateTime.Today.AddTicks(-1);
                    break;
                case "last week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7);
                    break;
                case "last month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    rangeEnd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddTicks(-1);
                    break;
                case "last year":
                    rangeStart = new DateTime(DateTime.Today.Year - 1, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year, 1, 1).AddTicks(-1);
                    break;
                case "today":
                    rangeStart = DateTime.Today;
                    rangeEnd = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;

                case "this week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    rangeEnd = rangeStart.AddDays(7).AddTicks(-1);
                    break;

                case "this month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    rangeEnd = rangeStart.AddMonths(1).AddTicks(-1);
                    break;

                case "this year":
                    rangeStart = new DateTime(DateTime.Today.Year, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year + 1, 1, 1).AddTicks(-1);
                    break;
                default:
                    throw new ArgumentException("Invalid range specified");
            }


            return await _context.Transactions
                .Where(t => t.AccountId == account.AccountId
                            && t.TransactionDate >= rangeStart
                            && t.TransactionDate <= rangeEnd)
                .CountAsync();
        }

        public async Task<object> GetTransactionSummary(string accountNumber, string range)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                return new
                {
                    TotalCredit = 0,
                    TotalDebit = 0,
                    NetBalanceChange = 0,
                    NumberOfTransactions = 0
                };
            }

            DateTime rangeStart;
            DateTime rangeEnd = DateTime.Now;

            switch (range.ToLower())
            {
                case "last day":
                    rangeStart = DateTime.Today.AddDays(-1);
                    rangeEnd = DateTime.Today.AddTicks(-1);
                    break;
                case "last week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7);
                    rangeEnd = rangeStart.AddDays(6).AddTicks(-1);
                    break;
                case "last month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    rangeEnd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddTicks(-1);
                    break;
                case "last year":
                    rangeStart = new DateTime(DateTime.Today.Year - 1, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year, 1, 1).AddTicks(-1);
                    break;
                case "today":
                    rangeStart = DateTime.Today;
                    rangeEnd = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;

                case "this week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    rangeEnd = rangeStart.AddDays(7).AddTicks(-1);
                    break;

                case "this month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    rangeEnd = rangeStart.AddMonths(1).AddTicks(-1);
                    break;

                case "this year":
                    rangeStart = new DateTime(DateTime.Today.Year, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year + 1, 1, 1).AddTicks(-1);
                    break;
                default:
                    throw new ArgumentException("Invalid range specified");
            }


            var totalCredit = await _context.Transactions
                .Where(t => t.AccountId == account.AccountId && t.TransactionDate >= rangeStart && t.TransactionDate <= rangeEnd && t.TransactionType == "Credit")
                .SumAsync(t => t.Amount);


            var totalDebit = await _context.Transactions
                .Where(t => t.AccountId == account.AccountId && t.TransactionDate >= rangeStart && t.TransactionDate <= rangeEnd && t.TransactionType == "Debit")
                .SumAsync(t => t.Amount);


            var netBalanceChange = totalCredit - totalDebit;


            var numberOfTransactions = await _context.Transactions
                .Where(t => t.AccountId == account.AccountId && t.TransactionDate >= rangeStart && t.TransactionDate <= rangeEnd)
                .CountAsync();

            return new
            {
                TotalCredit = totalCredit,
                TotalDebit = totalDebit,
                NetBalanceChange = netBalanceChange,
                NumberOfTransactions = numberOfTransactions
            };
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(int id, string range)
        {
            DateTime rangeStart;
            DateTime rangeEnd = DateTime.Now;

            switch (range.ToLower())
            {

                case "last day":
                    rangeStart = DateTime.Today.AddDays(-1);
                    rangeEnd = DateTime.Today.AddTicks(-1);
                    break;
                case "last week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7);
                    break;
                case "last month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    rangeEnd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddTicks(-1);
                    break;
                case "last year":
                    rangeStart = new DateTime(DateTime.Today.Year - 1, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year, 1, 1).AddTicks(-1);
                    break;
                case "today":
                    rangeStart = DateTime.Today;
                    rangeEnd = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;

                case "this week":
                    rangeStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    rangeEnd = rangeStart.AddDays(7).AddTicks(-1);
                    break;

                case "this month":
                    rangeStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    rangeEnd = rangeStart.AddMonths(1).AddTicks(-1);
                    break;

                case "this year":
                    rangeStart = new DateTime(DateTime.Today.Year, 1, 1);
                    rangeEnd = new DateTime(DateTime.Today.Year + 1, 1, 1).AddTicks(-1);
                    break;
                default:
                    throw new ArgumentException("Invalid range specified");
            }

            User? user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            return await _context.Transactions
                .Include(a => a.Account).Where(t => t.Account!.UserId == id && t.TransactionDate >= rangeStart && t.TransactionDate <= rangeEnd)
                .ToListAsync();
        }

        private async Task SendTransactionEmail(string userEmail, Transaction transaction)
        {
            var fromAddress = new MailAddress("sownthariponnusamy@gmail.com", "Payoda");
            var toAddress = new MailAddress(userEmail);
            const string fromPassword = "zwfa cdzm xlan ubri";
            const string subject = "Transaction Confirmation";

            string body = $"Dear Customer,\n\n" +
                          $"A transaction has been made on your account.\n" +
                          $"Transaction Type: {transaction.TransactionType}\n" +
                          $"Amount: {transaction.Amount:C}\n" +
                          $"Date: {transaction.TransactionDate}\n" +
                          $"Transaction ID: {transaction.TransactionId}\n\n" +
                          $"Thank you for using our services.\n\n" +
                          $"Best regards";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}
