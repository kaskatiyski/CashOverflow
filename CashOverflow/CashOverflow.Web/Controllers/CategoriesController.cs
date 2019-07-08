using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using CashOverflow.Web.ViewModels.Category;
using CashOverflow.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashOverflow.App.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly ITransactionService transactionService;

        public CategoriesController(ApplicationDbContext db,
                                    IMapper mapper,
                                    IUserService userService,
                                    ICategoryService categoryService,
                                    ITransactionService transactionService)
        {
            this.db = db;
            this.mapper = mapper;
            this.userService = userService;
            this.categoryService = categoryService;
            this.transactionService = transactionService;
        }
               
        public async Task<ActionResult> All()
        {
            var categories = this.categoryService.GetCategoriesByUsername(this.User.Identity.Name);

            var allCategoriesViewModel = new AllCategoriesViewModel()
            {
                Categories = categories.Select(c => mapper.Map<CategoryViewModel>(c)).ToList()
            };

            foreach (var category in allCategoriesViewModel.Categories)
            {
                var count = await this.transactionService.GetTransactionsCountByCategoryAsync(this.User.Identity.Name, category.Id);
                var sum = await this.transactionService.GetTransactionsSumByCategoryAsync(this.User.Identity.Name, category.Id);

                category.TransactionCount = count;
                category.TransactionSum = sum;
            }
            
            return this.View(allCategoriesViewModel);
        }
        
        public ActionResult Create()
        {
            this.ViewData["CategoryExpenseImages"] = new DirectoryInfo(@"wwwroot\resources\icons\expense").GetFiles().Select(f => @"\resources\icons\expense\" + f.Name);
            this.ViewData["CategoryIncomeImages"] = new DirectoryInfo(@"wwwroot\resources\icons\income").GetFiles().Select(f => @"\resources\icons\income\" + f.Name);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            if (ModelState.IsValid)
            {
                await this.categoryService.CreateAsync(this.User.Identity.Name, mapper.Map<Category>(model));
            }

            return this.Redirect("/");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.categoryService.GetCategoryByIdAsync(this.User.Identity.Name, id);

            if (category == null)
            {
                return NotFound();
            }

            this.ViewData["CategoryExpenseImages"] = new DirectoryInfo(@"wwwroot\resources\icons\expense").GetFiles().Select(f => @"\resources\icons\expense\" + f.Name);
            this.ViewData["CategoryIncomeImages"] = new DirectoryInfo(@"wwwroot\resources\icons\income").GetFiles().Select(f => @"\resources\icons\income\" + f.Name);

            var editCategoryInputModel = mapper.Map<EditCategoryInputModel>(category);

            return View(editCategoryInputModel);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryInputModel model)
        {
            var category = mapper.Map<Category>(model);

            //if (id != transaction.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                //try
                //{
                await this.categoryService.UpdateCategoryAsync(this.User.Identity.Name, category);
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
            return View(category);
        }


        public async Task<ActionResult> Details(string id)
        {
            var category = await this.categoryService.GetCategoryByIdAsync(this.User.Identity.Name, id);

            var detailsCategoryViewModel = mapper.Map<DetailsCategoryViewModel>(category);

            var transactions = this.transactionService.GetTransactionsByCategoryAsync(this.User.Identity.Name, category.Id);

            detailsCategoryViewModel.Transactions = transactions.Select(t => mapper.Map<TransactionViewModel>(t)).OrderBy(t => t.Date).GroupBy(t => t.Date.ToString("MMMM yyyy"));
            
            return this.View(detailsCategoryViewModel);
        }


        // POST: Categories/Delete/5
        // TODO: Maybe remove try/catch, research a better way for deleting items and make it secure
        // POST: Transactions1/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(string id)
        {
            return await this.categoryService.DeleteCategoryAsync(this.User.Identity.Name, id);
        }

    }
}