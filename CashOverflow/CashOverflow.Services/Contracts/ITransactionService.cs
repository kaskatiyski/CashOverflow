using CashOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services.Contracts
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactionsByDay(string username, string date);

        IEnumerable<Transaction> GetTransactionsByMonth(string username, string date);

        IEnumerable<Transaction> GetTransactionsByYear(string username, string date);

        IEnumerable<Transaction> GetTransactionsByRange(string username, string startDate, string endDate);

        Task CreateAsync(string username, Transaction transaction);

        Task<Transaction> GetTransactionByIdAsync(string username, string id);

        Task UpdateTransactionAsync(string username, Transaction transaction);

        Task<bool> DeleteTransactionAsync(string username, string id);

        IEnumerable<Transaction> GetTransactionsByCategoryAsync(string username, string categoryId);

        Task<int> GetTransactionsCountByCategoryAsync(string username, string categoryId);

        Task<decimal> GetTransactionsSumByCategoryAsync(string username, string categoryId);
    }
}
