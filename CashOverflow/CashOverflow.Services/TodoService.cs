using CashOverflow.Models;
using CashOverflow.Models.Enum;
using CashOverflow.Services.Contracts;
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
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;

        public TodoService(ApplicationDbContext db,
                               IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        public async Task<Todo> GetTodoByIdAsync(string username, string id)
        {
            var todo = await this.db.Todos
                .FirstOrDefaultAsync(t => t.User.UserName == username && t.Id == id);

            return todo;

        }

        public async Task<string> CompleteAsync(string username, string id)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);

            var todo = await GetTodoByIdAsync(username, id);

            string status;

            if (todo.Status == TodoStatus.Pending)
            {
                todo.Status = TodoStatus.Completed;
                status = "true";
            }
            else
            {
                todo.Status = TodoStatus.Pending;
                status = "false";
            }

            todo.UserId = user.Id;

            this.db.Todos.Update(todo);
            await this.db.SaveChangesAsync();

            return status;
        }

        public async Task CreateAsync(string username, Todo todo)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);

            todo.UserId = user.Id;

            this.db.Add(todo);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Todo>> GetTodosByDate(string username, DateTime date)
        {
            var todos = await this.db.Todos
                .Where(t => t.User.UserName == username && (t.Date.Year == date.Year && t.Date.Month == date.Month && t.Date.Day == date.Day))
                .ToListAsync();

            return todos;
        }

        public async Task<IEnumerable<Todo>> GetTodosByMonth(string username, string date)
        {
            DateTime dateParsed = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var todos = await this.db.Todos
                .Where(t => t.User.UserName == username && (t.Date.Year == dateParsed.Year && t.Date.Month == dateParsed.Month))
                .ToListAsync();

            return todos;
        }

        public async Task UpdateTodoAsync(string username, Todo todo)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);

            todo.UserId = user.Id;

            this.db.Todos.Update(todo);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DeleteTodoAsync(string username, string id)
        {
            var todo = await this.GetTodoByIdAsync(username, id);

            try
            {
                this.db.Todos.Remove(todo);
                await this.db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Todo>> GetTodos(string username)
        {
            var todos = await this.db.Todos.Where(todo => todo.User.UserName == username).ToListAsync();

            return todos;
        }
    }
}
