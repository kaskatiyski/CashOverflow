using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Web.ViewModels.Calendar;
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
            this.CreateMap<EditTransactionInputModel, Transaction>().ReverseMap();
            this.CreateMap<Transaction, EditTransactionViewModel>();
            this.CreateMap<Transaction, TransactionViewModel>();
            this.CreateMap<Transaction, TransactionAmountViewModel>();

                // Calendar
                this.CreateMap<Transaction, CalendarTransactionViewModel>();

                // Categories
                this.CreateMap<Transaction, CategoryTransactionViewModel>();

            // Categories
            this.CreateMap<EditCategoryInputModel, Category>().ReverseMap();
            this.CreateMap<CreateCategoryInputModel, Category>();
            this.CreateMap<Category, DetailsCategoryViewModel>();
            this.CreateMap<Category, CategoryViewModel>();

            // Transactions
                this.CreateMap<Category, CreateTransactionCaregoryViewModel>();
                this.CreateMap<Category, TransactionCategoryViewModel>();
                this.CreateMap<Category, TransactionAmountViewModel>();

            // Calendar
            this.CreateMap<Category, CalendarCategoryViewModel>();

            // Locations
            this.CreateMap<CreateLocationInputModel, Location>().ReverseMap();
            this.CreateMap<Location, LocationViewModel>();

                // Transactions
                this.CreateMap<Location, TransactionLocationViewModel>();
                this.CreateMap<Location, CalendarLocationViewModel>();

            // Calendar

        }
    }
}
