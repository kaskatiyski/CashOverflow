using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Dashboard;
using CashOverflow.Web.ViewModels.Note;
using CashOverflow.Web.ViewModels.Todo;
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

        public async Task<IActionResult> Dashboard()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            var transactions = await this.transactionService.GetTransactionsByMonth(this.User.Identity.Name);
            var todos = await this.todoService.GetTodos(this.User.Identity.Name);
            var notes = await this.noteService.GetNotes(this.User.Identity.Name);

            dashboardViewModel.Transactions = transactions.Select(transaction => mapper.Map<TransactionViewModel>(transaction))
                                                          .ToList();

            dashboardViewModel.Todos = todos.Select(todo => mapper.Map<TodoViewModel>(todo))
                                                          .ToList();

            dashboardViewModel.Notes = notes.Select(note => mapper.Map<NoteViewModel>(note))
                                                          .ToList();

            return View(dashboardViewModel);
        }
    }
}