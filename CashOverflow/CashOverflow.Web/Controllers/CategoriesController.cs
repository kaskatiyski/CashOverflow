using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Utilities.Exceptions;
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
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly ITransactionService transactionService;

        public CategoriesController(IMapper mapper,
                                    ICategoryService categoryService,
                                    ITransactionService transactionService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.transactionService = transactionService;
        }

        private void SetReturnUrl()
        {
            this.ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
        }

        private void GetCategoryImages()
        {
            WebClient client = new WebClient();

            string baseURL = "http://ivanpetrov.eu/CashOverflow/Resources/Icons/CategoryIcons/";
            string filesHTML = client.DownloadString(baseURL);

            var regex = new Regex("<a\\s+(?:[^>]*?\\s+)?href=\"([^\"]*)\"",
                RegexOptions.IgnoreCase);

            this.ViewData["CategoryImages"] = regex.Matches(filesHTML).Select(match => baseURL + match.Groups[1].Value).Skip(1);
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
            GetCategoryImages();
            SetReturnUrl();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryInputModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await this.categoryService.CreateAsync(this.User.Identity.Name, mapper.Map<Category>(model));

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }
            }

            SetReturnUrl();

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

            GetCategoryImages();
            SetReturnUrl();
        
            var editCategoryInputModel = mapper.Map<EditCategoryInputModel>(category);

            return View(editCategoryInputModel);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditCategoryInputModel model, string returnUrl)
        {
            var category = mapper.Map<Category>(model);

            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.categoryService.UpdateCategoryAsync(this.User.Identity.Name, category);

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }
            }

            return View(category);
        }


        public async Task<ActionResult> Details(string id)
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
        public async Task<string> DeleteConfirmed(string id)
        {
            try
            {
                await this.categoryService.DeleteCategoryAsync(this.User.Identity.Name, id);

                return "true";
            }
            catch (CategoryNotEmptyException ex)
            {
                return ex.Message;
            }
            catch (Exception)
            {
                return "false";
            }
        }

    }
}