using AutoMapper;
using CashOverflow.Models.Enum;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Calendar;
using CashOverflow.Web.ViewModels.Todo;
using CashOverflow.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.Controllers
{
    [Authorize]
    public class CalendarController : Controller
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
            //var transactions = this.transactionService.GetTransactionsByMonth(this.User.Identity.Name, DateTime.UtcNow.ToString("yyyy-MM"));

            //CalendarViewModel calendarViewModel = new CalendarViewModel();

            //calendarViewModel.Transactions = transactions.Select(t => mapper.Map<CalendarTransactionViewModel>(t))
            //                                             .GroupBy(t => t.Date.Day.ToString());

            return this.View(/*calendarViewModel*/);
        }

        public async Task<JsonResult> GetTransactionsForMonth(string date)
        {
            var transactions = await this.transactionService.GetTransactionsByMonth(this.User.Identity.Name, date);
           

            CalendarViewModel calendarViewModel = new CalendarViewModel();

            calendarViewModel.Transactions = transactions.Select(t => mapper.Map<TransactionViewModel>(t))
                                                         .GroupBy(t => t.Date.ToString("yyyyMMdd"))
                                                         .ToDictionary(group => group.Key, group => group.ToList());

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
            var todo = await this.todoService.GetTodoByIdAsync(this.User.Identity.Name, id);

            var status = await this.todoService.CompleteAsync(this.User.Identity.Name, todo);

            return this.Json(status);
        }
    }
}
