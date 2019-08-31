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

namespace CashOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IMapper mapper;

        public HomeController(IMapper mapper,
                              ITransactionService transactionService)
        {
            this.transactionService = transactionService;
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
    }
}
