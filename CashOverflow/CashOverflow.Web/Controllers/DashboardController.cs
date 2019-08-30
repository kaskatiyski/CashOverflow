using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Dashboard;
using CashOverflow.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashOverflow.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        private readonly ITodoService todoService;
        private readonly INoteService noteService;

        public DashboardController(IMapper mapper,
                                  ITransactionService transactionService,
                                  INoteService noteService,
                                  ITodoService todoService)
        {
            this.mapper = mapper;
            this.transactionService = transactionService;
            this.todoService = todoService;
            this.noteService = noteService;
        }

        public IActionResult Dashboard()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            dashboardViewModel.Transactions = this.transactionService.GetTransactionsByMonth(this.User.Identity.Name)
                                                                     //.Select(t => mapper.Map<TransactionViewModel>(t))
                                                                     .Select(t => mapper.Map<DashboardTransactionViewModel>(t))
                                                                     .ToList();

            dashboardViewModel.Todos = this.todoService.GetTodosByMonth(this.User.Identity.Name, DateTime.UtcNow.ToString("yyyy-MM-dd"))
                                                                     .Select(t => mapper.Map<DashboardTodoViewModel>(t))
                                                                     .ToList();

            dashboardViewModel.Notes = this.noteService.GetNotes(this.User.Identity.Name).ToList()
                                                                     .Select(t => mapper.Map<DashboardNoteViewModel>(t))
                                                                     .ToList(); ;

            return View(dashboardViewModel);
        }
    }
}