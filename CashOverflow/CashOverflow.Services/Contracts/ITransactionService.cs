using CashOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Services.Contracts
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactionsByDay(string username, string date);

        IEnumerable<Transaction> GetTransactionsByMonth(string username, string date);

        IEnumerable<Transaction> GetTransactionsByYear(string username, string date);

        IEnumerable<Transaction> GetTransactionsByRange(string username, string startDate, string endDate);

        void Create(string username, Transaction transaction);

    }
}
