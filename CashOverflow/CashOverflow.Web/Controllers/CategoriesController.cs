using System.Collections.Generic;
using System.IO;
using System.Linq;
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



        //private List<Transaction> GetTransactions(int categoryId)
        //{
        //    return db.Transactions.Where(transaction => transaction.CategoryId == categoryId && transaction.User.Id == userService).ToList();
        //}
        //public JsonResult GetTransactionsAsJson(int categoryId/*, string groupBy = "monthly"*/)
        //{

        //    // TODO: Research if there is a way to not load the transactions every time this method is called.
        //    var transactions = this.GetTransactions(categoryId).OrderBy(transaction => transaction.Date).ToList();

        //    if (transactions.Count <= 0)
        //    {
        //        return Json(false);
        //    }



        //    //var startDate = transactions.FirstOrDefault().Date;
        //    //var endDate = transactions.LastOrDefault().Date;

        //    //var transactionsSummed = new List<KeyValuePair<string, List<Transaction>>>();

        //    #region a
        //    //switch (groupBy.ToLower())
        //    //{
        //    //    case "weekly":
        //    //        var transactionsGroupedW = transactions
        //    //            .GroupBy(transaction => new
        //    //            {
        //    //                transaction.Date.Year,
        //    //                transaction.Date.Month,
        //    //                Week = transaction.Date.GetWeekOfYear()
        //    //            });

        //    //        for (int i = 0; i < startDate.GetWeeksDifference(endDate); i++)
        //    //        {
        //    //            var date = startDate.AddDays(i * 7);

        //    //            var group = transactionsGroupedW.FirstOrDefault(x => x.Key.Year == date.Year && x.Key.Month == date.Month && x.Key.Week == date.GetWeekOfYear());

        //    //            transactionsSummed.Add(new KeyValuePair<string, List<Transaction>>($"{date.FirstDayOfWeek().ToString("dd/MM/y")} - {date.LastDayOfWeek().ToString("dd/MM/y")}", group?.ToList()));
        //    //        }

        //    //        break;
        //    //    case "monthly":
        //    //    default:
        //    //        var transactionsGroupedM = transactions
        //    //            .GroupBy(transaction => new { transaction.Date.Year, transaction.Date.Month }).ToList();


        //    //        for (int i = (transactionsGroupedM.Count / 2) - 3; i < (transactionsGroupedM.Count / 2) + 3; i++)
        //    //        {
        //    //            var date = startDate.AddMonths(i);

        //    //            if (i < 0 || i > startDate.GetMonthsDifference(endDate))
        //    //            {
        //    //                transactionsSummed.Add(new KeyValuePair<string, List<Transaction>>(date.ToString("MMM/y"), null));
        //    //            }
        //    //            else
        //    //            {
        //    //                var group = transactionsGroupedM.FirstOrDefault(x => x.Key.Year == date.Year && x.Key.Month == date.Month);
        //    //                transactionsSummed.Add(new KeyValuePair<string, List<Transaction>>(date.ToString("MMM/y"), group?.ToList()));
        //    //            }
        //    //        }

        //    //        break;
        //    //    case "annually":
        //    //    case "yearly":
        //    //        var transactionsGroupedY = transactions
        //    //            .GroupBy(transaction => new { transaction.Date.Year });

        //    //        for (int i = 0; i < startDate.GetYearsDifference(endDate); i++)
        //    //        {
        //    //            var date = startDate.AddYears(i);

        //    //            var group = transactionsGroupedY.FirstOrDefault(x => x.Key.Year == date.Year);

        //    //            transactionsSummed.Add(new KeyValuePair<string, List<Transaction>>(date.ToString("yyyy"), group?.ToList()));
        //    //        }

        //    //        break;
        //    //}
        //    #endregion  

        //    return Json(transactions);

        //}



        // GET: Categories
        //public ActionResult All()
        //{
        //    var categories = this.categoryService.GetAllCategoriesByUsername(this.User.Identity.Name).Select(x => new CategoryViewModel()
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Type = x.Type,
        //        ImagePath = x.ImagePath
        //    }).ToList();

        //    var allCategoriesViewModel = new AllCategoriesViewModel()
        //    {
        //        Categories = categories
        //    };

        //    foreach (var category in allCategoriesViewModel.Categories)
        //    {
        //        var transactions = this.transactionService.GetTransactionsByCategoryId(category.Id)
        //            .OrderBy(transaction => transaction.Date)
        //            .Select(x => new TransactionAmountViewModel() { Ammount = x.Ammount })
        //            .ToList();

        //        category.Transactions = transactions;
        //    }

        //    return View(allCategoriesViewModel);
        //}

        // GET: Categories/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Categories/Create
        public ActionResult Create()
        {
            this.ViewData["CategoryExpenseImages"] = new DirectoryInfo(@"wwwroot\resources\icons\expense").GetFiles().Select(f => @"\resources\icons\expense\" + f.Name);
            this.ViewData["CategoryIncomeImages"] = new DirectoryInfo(@"wwwroot\resources\icons\income").GetFiles().Select(f => @"\resources\icons\income\" + f.Name);

            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryInputModel model)
        {
            this.categoryService.Create(this.User.Identity.Name, mapper.Map<Category>(model));

            return this.Redirect("/");
        }

        //// GET: Categories/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Categories/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(All));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Categories/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Categories/Delete/5
        // TODO: Maybe remove try/catch, research a better way for deleting items and make it secure
        //[HttpPost]
        //public JsonResult DeleteConfirm(int id)
        //{
        //    try
        //    {
        //        this.categoryService.Delete(id);

        //        return Json(true);
        //    }
        //    catch
        //    {
        //        return Json(false);
        //    }
        //}
    }
}