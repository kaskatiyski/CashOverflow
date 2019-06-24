using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashOverflow.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext db;

        public TransactionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Transaction> GetTransactionsByDay(string username, string date)
        {
            DateTime dateParsed = DateTime.Parse(date);

            var transactions = db.Transactions
                .Include(x => x.Category)
                .Where(t => t.User.UserName == username && (t.Date.Year == dateParsed.Year && t.Date.Month == dateParsed.Month && t.Date.Day == dateParsed.Day));
            
            return transactions;
        }
    }
}
