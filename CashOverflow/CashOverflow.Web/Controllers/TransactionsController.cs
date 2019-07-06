using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Services.Contracts;
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

        public ActionResult All(string date)
        {
            if (date == null)
            {
                date = DateTime.Parse(this.Request.Cookies["localDate"]).ToString("yyyy-MM");
            }

            IEnumerable<Transaction> transactions = new List<Transaction>();
            IEnumerable<Category> categories = this.categoryService.GetCategoriesByUsername(this.User.Identity.Name);
            Func<TransactionViewModel, string> groupBy = x => x.Date.ToString("dddd, dd");

            if (date.Contains('>'))
            {
                var split = date.Split('>');
                var startDate = split[0];
                var endDate = split[1];

                transactions = this.transactionService.GetTransactionsByRange(this.User.Identity.Name, startDate, endDate);

            } else {
                var parts = date.Split('-');

                switch (parts.Length)
                {
                    case 1:
                        transactions = this.transactionService.GetTransactionsByYear(this.User.Identity.Name, date);

                        groupBy = x => x.Date.ToString("MMMM");

                        break;
                    case 2:
                        transactions = this.transactionService.GetTransactionsByMonth(this.User.Identity.Name, date);

                        break;
                    case 3:
                        transactions = this.transactionService.GetTransactionsByDay(this.User.Identity.Name, date);

                        break;
                }
            }

            AllTransactionsViewModel allTransactionsViewModel = new AllTransactionsViewModel()
            {
                Transactions = transactions.Select(x => mapper.Map<TransactionViewModel>(x)).GroupBy(groupBy),
                Categories = categories.Select(x => mapper.Map<TransactionCategoryViewModel>(x)).GroupBy(x => x.Type.ToString())
            };
            
            return View(allTransactionsViewModel);
        }
        
        public ActionResult Create()
        {
            var createTransactionViewModel = new CreateTransactionViewModel();

            createTransactionViewModel.Categories = this.categoryService.GetCategoriesByUsername(this.User.Identity.Name)
                .Select(x => mapper.Map<CreateTransactionCaregoryViewModel>(x))
                .ToList();

            this.ViewData["Categories"] = createTransactionViewModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTransactionInputModel model)
        {
            try
            {
                await this.transactionService.CreateAsync(this.User.Identity.Name, mapper.Map<Transaction>(model));

                return this.Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        // GET: Transactions1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var editTransactionViewModel = new EditTransactionViewModel();

            editTransactionViewModel.Categories = this.categoryService.GetCategoriesByUsername(this.User.Identity.Name)
                .Select(x => mapper.Map<CreateTransactionCaregoryViewModel>(x))
                .ToList();

            this.ViewData["Categories"] = editTransactionViewModel;

            if (id == null)
            {
                return NotFound();
            }

            var transaction = await this.transactionService.GetTransactionByIdAsync(this.User.Identity.Name, id);

            if (transaction == null)
            {
                return NotFound();
            }

            var editTransactionInputModel = mapper.Map<EditTransactionInputModel>(transaction);

            return View(editTransactionInputModel);
        }

        // POST: Transactions1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTransactionInputModel model)
        {
            var transaction = mapper.Map<Transaction>(model);

            //if (id != transaction.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                //try
                //{
                    await this.transactionService.UpdateTransactionAsync(this.User.Identity.Name, transaction);
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!TransactionExists(transaction.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(All));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", transaction.CategoryId);
            //ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", transaction.LocationId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaction.UserId);
            return View(transaction);
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