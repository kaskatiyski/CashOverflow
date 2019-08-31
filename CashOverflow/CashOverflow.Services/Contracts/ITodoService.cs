using CashOverflow.Models;
using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services.Contracts
{
    public interface ITodoService
    {
        Task CreateAsync(string username, Todo todo);

        Task<IEnumerable<Todo>> GetTodos(string username);

        Task<IEnumerable<Todo>> GetTodosByDate(string username, DateTime date);

        Task<IEnumerable<Todo>> GetTodosByMonth(string username, string date);

        Task<TodoStatus> CompleteAsync(string username, Todo todo);

        Task<Todo> GetTodoByIdAsync(string username, string id);

        Task UpdateTodoAsync(string username, Todo todo);

        Task<bool> DeleteTodoAsync(string username, string id);
    }
}
