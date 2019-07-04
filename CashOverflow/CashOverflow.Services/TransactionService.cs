using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CashOverflow.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;
        private readonly ILocationService locationService;

        public TransactionService(ApplicationDbContext db,
                                      IUserService userService,
                                      ILocationService locationService)

        {
            this.db = db;
            this.userService = userService;
            this.locationService = locationService;
        }

        public IEnumerable<Transaction> GetTransactionsByDay(string username, string date)
        {
            DateTime dateParsed = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var transactions = db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && (t.Date.Year == dateParsed.Year && t.Date.Month == dateParsed.Month && t.Date.Day == dateParsed.Day))
                .OrderByDescending(x => x.Date);
            
            return transactions;
        }

        public IEnumerable<Transaction> GetTransactionsByMonth(string username, string date)
            {
            DateTime dateParsed = DateTime.ParseExact(date, "yyyy-MM", CultureInfo.InvariantCulture);

            var transactions = db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && (t.Date.Year == dateParsed.Year && t.Date.Month == dateParsed.Month))
                .OrderByDescending(x => x.Date);

            return transactions;
        }

        public IEnumerable<Transaction> GetTransactionsByYear(string username, string date)
        {
            DateTime dateParsed = DateTime.ParseExact(date, "yyyy", CultureInfo.InvariantCulture);

            var transactions = db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username && (t.Date.Year == dateParsed.Year))
                .OrderByDescending(x => x.Date);

            return transactions;
        }

        public IEnumerable<Transaction> GetTransactionsByRange(string username, string startDate, string endDate)
        {
            DateTime startDateParsed = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDateParsed = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var transactions = db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Location)
                .Where(t => t.User.UserName == username
                    && (t.Date >= startDateParsed) 
                    && (t.Date <= endDateParsed))
                .OrderByDescending(x => x.Date);

            return transactions;
        }

        public void Create(string username, Transaction transaction)
        {
            transaction.UserId = this.db.Users.FirstOrDefault(u => u.UserName == username).Id;

            transaction.Location = this.locationService.Create(transaction.Location);

            this.db.Add(transaction);
            this.db.SaveChanges();
        }
    }
}
