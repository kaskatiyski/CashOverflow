using CashOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Services.Contracts
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactionsByDay(string username, string date);
    }
}
