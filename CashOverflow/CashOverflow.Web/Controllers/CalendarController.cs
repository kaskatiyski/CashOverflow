using AutoMapper;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Calendar;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;

        public CalendarController(IMapper mapper,
                                  ITransactionService transactionService)
        {
            this.mapper = mapper;
            this.transactionService = transactionService;
        }

        public ActionResult Index()
        {
            //var transactions = this.transactionService.GetTransactionsByMonth(this.User.Identity.Name, DateTime.UtcNow.ToString("yyyy-MM"));

            //CalendarViewModel calendarViewModel = new CalendarViewModel();

            //calendarViewModel.Transactions = transactions.Select(t => mapper.Map<CalendarTransactionViewModel>(t))
            //                                             .GroupBy(t => t.Date.Day.ToString());

            return this.View(/*calendarViewModel*/);
        }

        public JsonResult GetTransactionsForMonth(string date)
        {
            var transactions = this.transactionService.GetTransactionsByMonth(this.User.Identity.Name, date);

            CalendarViewModel calendarViewModel = new CalendarViewModel();

            calendarViewModel.Transactions = transactions.Select(t => mapper.Map<CalendarTransactionViewModel>(t))
                                                         .GroupBy(t => t.Date.Day.ToString());

            return this.Json(calendarViewModel);
        }
    }
}
