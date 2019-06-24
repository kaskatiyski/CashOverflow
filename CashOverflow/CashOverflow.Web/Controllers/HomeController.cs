using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CashOverflow.Web.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Home;
using CashOverflow.Web.ViewModels.Transaction;
using AutoMapper;
using System;

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
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                // TODO: Find a way to get client date
                var transactions = this.transactionService.GetTransactionsByDay(this.User.Identity.Name, DateTime.UtcNow.ToString()).ToList();

                HomeViewModel homeViewModel = new HomeViewModel()
                {
                    Transactions = transactions.Select(x => mapper.Map<HomeTransactionViewModel>(x))
                };

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
