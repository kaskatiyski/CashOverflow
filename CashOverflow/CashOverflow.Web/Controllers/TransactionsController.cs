using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Utilities.Extensions;
using CashOverflow.Web.ViewModels.Category;
using CashOverflow.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CashOverflow.App.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        private readonly ICategoryService categoryService;

        public TransactionsController(IMapper mapper,
                                      ITransactionService transactionService,
                                      ICategoryService categoryService)
        {
            this.transactionService = transactionService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        private void SetReturnUrl()
        {
            this.ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
        }

        public async Task<ActionResult> All(string from, string to, string exact, bool allTime = false)
        {
            IEnumerable<Transaction> transactions = new List<Transaction>();

            if (allTime)
            {
                transactions = await this.transactionService.GetAllTransactions(this.User.Identity.Name);
            }
            else if (exact != null)
            {
                transactions = await this.transactionService.GetTransactionsByDay(this.User.Identity.Name, exact);
            }
            else if (from != null && to != null)
            {
                transactions = await this.transactionService.GetTransactionsInRange(this.User.Identity.Name, from, to);
            }
            else if (from != null && to == null)
            {
                transactions = await this.transactionService.GetTransactionsSince(this.User.Identity.Name, from);
            }
            else if (from == null && to != null)
            {
                transactions = await this.transactionService.GetTransactionsUntil(this.User.Identity.Name, to);
            }
            else
            {
                transactions = await this.transactionService.GetTransactionsByMonth(this.User.Identity.Name);
            }                       

            Func<TransactionViewModel, string> groupBy = x => x.Date.ToString("dddd, dd");

            var firstTransaction = transactions.FirstOrDefault();
            var lastTransaction = transactions.LastOrDefault();

            if (firstTransaction != null && lastTransaction != null)
            {
                if (!firstTransaction.Date.IsSameYear(lastTransaction.Date))
                {
                    //groupBy = x => x.Date.ToString("MMMM yyyy");
                    groupBy = x => x.Date.ToString("yyyy, MMMM dd");
                }
                else if (!firstTransaction.Date.IsSameMonth(lastTransaction.Date))
                {
                    groupBy = x => x.Date.ToString("MMMM, dd");
                }
            }

            AllTransactionsViewModel allTransactionsViewModel = new AllTransactionsViewModel()
            {
                Transactions = mapper.Map<IEnumerable<TransactionViewModel>>(transactions).GroupBy(groupBy)
            };

            return View(allTransactionsViewModel);
        }

        public ActionResult Create(string type, string categoryId, string date)
        {
            var categories = this.categoryService.GetCategoriesByUsername(this.User.Identity.Name);

            if (categories.Count() <= 0)
            {
                return this.RedirectToAction("Create", "Categories");
            }

            var createTransactionViewModel = new CreateTransactionViewModel
            {
                Categories = mapper.Map<List<CategoryViewModel>>(categories)
            };

            this.ViewData["Categories"] = createTransactionViewModel;

            if (type != null)
            {
                this.ViewData["Type"] = type;
            }

            if (categoryId != null)
            {
                this.ViewData["Category"] = categoryId;
            }

            if (date != null)
            {
                this.ViewData["Date"] = date;
            }

            SetReturnUrl();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTransactionInputModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await this.transactionService.CreateAsync(this.User.Identity.Name, mapper.Map<Transaction>(model));

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }
            }

            return RedirectToAction(nameof(Create));
        }

        // GET: Transactions1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var editTransactionViewModel = new EditTransactionViewModel
            {
                Categories = this.categoryService.GetCategoriesByUsername(this.User.Identity.Name)
                .Select(x => mapper.Map<CategoryViewModel>(x))
                .ToList()
            };

            if (id == null)
            {
                return NotFound();
            }

            var transaction = await this.transactionService.GetTransactionByIdAsync(this.User.Identity.Name, id);

            if (transaction == null)
            {
                return NotFound();
            }

            this.ViewData["Categories"] = editTransactionViewModel;
            SetReturnUrl();

            var editTransactionInputModel = mapper.Map<EditTransactionInputModel>(transaction);

            return View(editTransactionInputModel);
        }

        // POST: Transactions1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditTransactionInputModel model, string returnUrl)
        {
            var transaction = mapper.Map<Transaction>(model);

            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.transactionService.UpdateTransactionAsync(this.User.Identity.Name, transaction);

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }
            }
            
            return await this.Edit(model.Id);
        }

        // POST: Transactions1/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(string id)
        {
            return await this.transactionService.DeleteTransactionAsync(this.User.Identity.Name, id);
        }


    }
}