using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Utilities.Extensions;
using CashOverflow.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;
        private readonly ILocationService locationService;

        private const string parseFormat = "yyyy-MM-dd";

        public TransactionService(ApplicationDbContext db,
                                      IUserService userService,
                                      ILocationService locationService)

        {
            this.db = db;
            this.userService = userService;
            this.locationService = locationService;
        }

        private DateTime ParseDate(string date)
        {
            var dateParsed = DateTime.ParseExact(date, parseFormat, CultureInfo.InvariantCulture);

            return dateParsed;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDay(string username, string date)
        {
            DateTime dateParsed = ParseDate(date);

            var transactions = await this.db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && t.Date.IsSameDay(dateParsed))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsUntil(string username, string date)
        {
            DateTime dateParsed = ParseDate(date);

            var transactions = await this.db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && (t.Date <= dateParsed))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsSince(string username, string date)
        {
            DateTime dateParsed = ParseDate(date);

            var transactions = await this.db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && (t.Date >= dateParsed))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByMonth(string username, string date)
            {
            DateTime dateParsed = ParseDate(date);

            var transactions = await this.db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && t.Date.IsSameMonth(dateParsed))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByMonth(string username)
        {
            DateTime dateParsed = DateTime.UtcNow;

            var transactions = await this.db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && t.Date.IsSameMonth(dateParsed))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByYear(string username, string date)
        {
            DateTime dateParsed = ParseDate(date);

            var transactions = await this.db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && t.Date.IsSameYear(dateParsed))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(string username)
        {
            var transactions = await this.db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsInRange(string username, string startDate, string endDate)
        {
            DateTime startDateParsed = ParseDate(startDate);
            DateTime endDateParsed = ParseDate(endDate);

            var transactions = await db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username
                    && (t.Date >= startDateParsed) 
                    && (t.Date <= endDateParsed))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return transactions;
        }

        public async Task CreateAsync(string username, Transaction transaction)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            transaction.UserId = user.Id;
                                          
            if (transaction.Location.PlaceId != null)
            {
                var location = await this.locationService.CreateAsync(transaction.Location);

                transaction.Location = location;
            }
            else
            {
                transaction.Location = null;
            }

            if (transaction.Recipient == null)
            {
                var category = await this.db.Categories.FirstOrDefaultAsync(cat => cat.User.UserName == username && cat.Id == transaction.CategoryId);
                transaction.Recipient = category.Name;
            }

            this.db.Add(transaction);
            await this.db.SaveChangesAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(string username, string id)
        {
            var transaction = await this.db.Transactions
                .Include(x => x.Location)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(t => t.User.UserName == username && t.Id == id);

            return transaction;
            
        }

        public async Task UpdateTransactionAsync(string username, Transaction transaction)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            transaction.UserId = user.Id;

            if (transaction.Location.PlaceId != null)
            {
                var location = await this.locationService.CreateAsync(transaction.Location);

                transaction.Location = location;
            }
            else
            {
                transaction.Location = null;
            }

            if (transaction.Recipient == null)
            {
                var category = await this.db.Categories.FirstOrDefaultAsync(cat => cat.User.UserName == username && cat.Id == transaction.CategoryId);
                transaction.Recipient = category.Name;
            }

            this.db.Transactions.Update(transaction);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DeleteTransactionAsync(string username, string id)
        {
            var transaction = await this.GetTransactionByIdAsync(username, id);

            try
            {
                this.db.Transactions.Remove(transaction);
                await this.db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(string username, string categoryId)
        {
            var transactions = await this.db.Transactions
                               .Include(t => t.Location)
                               .Where(t => t.User.UserName == username && t.CategoryId == categoryId)
                               .OrderByDescending(x => x.Date)
                               .ToListAsync();

            return transactions;
        }

        public async Task<int> GetTransactionsCountByCategoryAsync(string username, string categoryId)
        {
            var count = await this.db.Transactions
                               .Where(t => t.User.UserName == username && t.CategoryId == categoryId)
                               .CountAsync();

            return count;
        }

        public async Task<decimal> GetTransactionsSumByCategoryAsync(string username, string categoryId)
        {
            var sum = await this.db.Transactions
                               .Where(t => t.User.UserName == username && t.CategoryId == categoryId)
                               .Select(t => t.Ammount)
                               .SumAsync();

            return sum;
        }

        public async Task<bool> CategoryIsEmpty(string username, string categoryId)
        {
            return await this.db.Transactions.Where(t => t.User.UserName == username && t.CategoryId == categoryId).AnyAsync();
        }
    }
}
