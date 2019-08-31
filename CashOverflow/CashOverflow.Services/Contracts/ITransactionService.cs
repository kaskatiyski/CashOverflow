using CashOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services.Contracts
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsByDay(string username, string date);

        Task<IEnumerable<Transaction>> GetTransactionsByMonth(string username, string date);

        Task<IEnumerable<Transaction>> GetTransactionsByMonth(string username);

        Task<IEnumerable<Transaction>> GetTransactionsByYear(string username, string date);

        Task<IEnumerable<Transaction>> GetAllTransactions(string username);

        Task<IEnumerable<Transaction>> GetTransactionsInRange(string username, string startDate, string endDate);

        Task<IEnumerable<Transaction>> GetTransactionsUntil(string username, string date);

        Task<IEnumerable<Transaction>> GetTransactionsSince(string username, string date);

        Task CreateAsync(string username, Transaction transaction);

        Task<Transaction> GetTransactionByIdAsync(string username, string id);

        Task UpdateTransactionAsync(string username, Transaction transaction);

        Task<bool> DeleteTransactionAsync(string username, string id);

        Task<bool> CategoryIsEmpty(string username, string categoryId);

        Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(string username, string categoryId);

        Task<int> GetTransactionsCountByCategoryAsync(string username, string categoryId);

        Task<decimal> GetTransactionsSumByCategoryAsync(string username, string categoryId);
    }
}
