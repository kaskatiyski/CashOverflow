using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CashOverflow.Web.Controllers
{
    public class BaseController : Controller
    {
        private void SetReturnUrl()
        {
            this.ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
        }

        public override ViewResult View()
        {
            SetReturnUrl();

            return base.View();
        }

        internal async Task<IEnumerable<Transaction>> GetTransactions(ITransactionService transactionService, string from, string to, string exact, bool allTime)
        {
            IEnumerable<Transaction> transactions;
               
            if (allTime)
            {
                transactions = await transactionService.GetAllTransactions(this.User.Identity.Name);
                this.ViewData["Period"] = "All Time";
            }
            else if (exact != null)
            {
                transactions = await transactionService.GetTransactionsByDay(this.User.Identity.Name, exact);
                this.ViewData["Period"] = exact.ReformatDate();
            }
            else if (from != null && to != null)
            {
                transactions = await transactionService.GetTransactionsInRange(this.User.Identity.Name, from, to);
                this.ViewData["Period"] = from.ReformatDate() + " - " + to.ReformatDate();
            }
            else if (from != null && to == null)
            {
                transactions = await transactionService.GetTransactionsSince(this.User.Identity.Name, from);
                this.ViewData["Period"] = from.ReformatDate() + " - ∞";
            }
            else if (from == null && to != null)
            {
                transactions = await transactionService.GetTransactionsUntil(this.User.Identity.Name, to);
                this.ViewData["Period"] = "∞ - " + to.ReformatDate();
            }
            else
            {
                transactions = await transactionService.GetTransactionsByMonth(this.User.Identity.Name);
                this.ViewData["Period"] = DateTime.UtcNow.ReformatDate();
            }

            return transactions;
        }

        //internal async Task<IEnumerable<Todo>> GetTodos(ITodoService transactionService, string from, string to, string exact, bool allTime)
        //{
        //    IEnumerable<Todo> todos;

        //    if (allTime)
        //    {
        //        todos = await transactionService.GetAllTransactions(this.User.Identity.Name);
        //    }
        //    else if (exact != null)
        //    {
        //        todos = await transactionService.GetTransactionsByDay(this.User.Identity.Name, exact);
        //    }
        //    else if (from != null && to != null)
        //    {
        //        todos = await transactionService.GetTransactionsInRange(this.User.Identity.Name, from, to);
        //    }
        //    else if (from != null && to == null)
        //    {
        //        todos = await transactionService.GetTransactionsSince(this.User.Identity.Name, from);
        //    }
        //    else if (from == null && to != null)
        //    {
        //        todos = await transactionService.GetTransactionsUntil(this.User.Identity.Name, to);
        //    }
        //    else
        //    {
        //        todos = await transactionService.GetTransactionsByMonth(this.User.Identity.Name);
        //    }

        //    return todos;
        //}
    }
}
