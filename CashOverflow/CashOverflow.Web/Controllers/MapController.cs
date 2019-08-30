using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Map;
using CashOverflow.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashOverflow.Web.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly ITransactionService transactionService;

        public MapController(IMapper mapper,
                             IUserService userService,
                             ICategoryService categoryService,
                             ITransactionService transactionService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.categoryService = categoryService;
            this.transactionService = transactionService;
        }

        public IActionResult Map()
        {
            var transactions = this.transactionService.GetAllTransactions(this.User.Identity.Name);

            var mapViewModel = new MapViewModel() {
                Transactions = transactions.Where(transaction => transaction.Location != null)
                                           .Select(transaction => mapper.Map<TransactionViewModel>(transaction))
            };

            return View(mapViewModel);
        }
    }
}