using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Web.ViewModels.Category;
using CashOverflow.Web.ViewModels.Location;
using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.MapperConfiguration
{
    public class CashOverflowProfile : Profile
    {
        public CashOverflowProfile()
        {
            // Transactions
            this.CreateMap<CreateTransactionInputModel, Transaction>();
            this.CreateMap<Transaction, TransactionViewModel>();
            this.CreateMap<Transaction, TransactionAmountViewModel>();

            // Categories
            this.CreateMap<CreateCategoryInputModel, Category>();
            this.CreateMap<Category, CategoryViewModel>();

                // Transactions
                this.CreateMap<Category, CreateTransactionCaregoryViewModel>();
                this.CreateMap<Category, TransactionCategoryViewModel>();

            // Locations
            this.CreateMap<CreateLocationInputModel, Location>();
            this.CreateMap<Location, LocationViewModel>();

        }
    }
}
