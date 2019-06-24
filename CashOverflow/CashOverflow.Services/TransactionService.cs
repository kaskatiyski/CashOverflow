using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashOverflow.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext db;

        public TransactionService(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}
