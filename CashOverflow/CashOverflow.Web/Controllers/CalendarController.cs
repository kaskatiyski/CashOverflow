using AutoMapper;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Calendar;
using CashOverflow.Web.ViewModels.Todo;
using CashOverflow.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.Controllers
{
    [Authorize]
    public class CalendarController : BaseController
    {
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        private readonly ITodoService todoService;

        public CalendarController(IMapper mapper,
                                  ITodoService todoService,
                                  ITransactionService transactionService)
        {
            this.mapper = mapper;
            this.transactionService = transactionService;
            this.todoService = todoService;
        }

        public ActionResult Calendar()
        {
            return this.View();
        }

        public async Task<JsonResult> GetTransactionsForMonth(string date)
        {
            var transactions = await this.transactionService.GetTransactionsByMonth(this.User.Identity.Name, date);

            CalendarViewModel calendarViewModel = new CalendarViewModel
            {
                Transactions = transactions.Select(t => mapper.Map<TransactionViewModel>(t))
                                                         .GroupBy(t => t.Date.ToString("yyyyMMdd"))
                                                         .ToDictionary(group => group.Key, group => group.ToList())
            };

            foreach (var kvp in calendarViewModel.Transactions)
            {
                var todos = await this.todoService.GetTodosByMonth(this.User.Identity.Name, date);

                calendarViewModel.Todos = todos.Select(t => mapper.Map<TodoViewModel>(t))
                                               .GroupBy(t => t.Date.ToString("yyyyMMdd"))
                                               .ToDictionary(group => group.Key, group => group.ToList());

            }

                return this.Json(calendarViewModel);
        }

        public async Task<JsonResult> CompleteTodo(string id)
        {
            var status = await this.todoService.CompleteAsync(this.User.Identity.Name, id);

            return this.Json(status);
        }
    }
}
