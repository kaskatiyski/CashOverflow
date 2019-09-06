using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CashOverflow.Web.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Home;
using CashOverflow.Web.ViewModels.Transaction;
using AutoMapper;
using System;
using System.Threading.Tasks;
using CashOverflow.Web.ViewModels.Map;
using CashOverflow.Web.ViewModels.Dashboard;
using CashOverflow.Web.ViewModels.Todo;
using CashOverflow.Web.ViewModels.Note;

namespace CashOverflow.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITransactionService transactionService;
        private readonly ITodoService todoService;
        private readonly INoteService noteService;
        private readonly IMapper mapper;

        public HomeController(IMapper mapper,
                              ITodoService todoService,
                              INoteService noteService,
                              ITransactionService transactionService)
        {
            this.transactionService = transactionService;
            this.todoService = todoService;
            this.noteService = noteService;
            this.mapper = mapper;
        }

        // TODO: Research if this is a correct way of checking authorization users and returning another view
        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                // TODO: Find a way to get client date
                var date = Request.Cookies["localDate"] != null ? this.Request.Cookies["localDate"] : DateTime.UtcNow.ToString("yyyy-MM-dd");
                var transactions = await this.transactionService.GetTransactionsByDay(this.User.Identity.Name, date);

                HomeViewModel homeViewModel = new HomeViewModel()
                {
                    Transactions = transactions.Select(x => mapper.Map<TransactionViewModel>(x))
                };

                var a = homeViewModel.Transactions.GroupBy(x => "kur");

                return this.View("IndexLoggedIn", homeViewModel);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Map()
        {
            var transactions = await this.transactionService.GetAllTransactions(this.User.Identity.Name);

            var mapViewModel = new MapViewModel()
            {
                Transactions = transactions.Where(transaction => transaction.Location != null)
                                           .Select(transaction => mapper.Map<TransactionViewModel>(transaction))
            };

            return View(mapViewModel);
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
