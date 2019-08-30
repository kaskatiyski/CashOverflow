using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Web.ViewModels.Calendar;
using CashOverflow.Web.ViewModels.Category;
using CashOverflow.Web.ViewModels.Dashboard;
using CashOverflow.Web.ViewModels.Location;
using CashOverflow.Web.ViewModels.Map;
using CashOverflow.Web.ViewModels.Note;
using CashOverflow.Web.ViewModels.Todo;
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
            
            // Categories
            this.CreateMap<EditCategoryInputModel, Category>().ReverseMap();
            this.CreateMap<CreateCategoryInputModel, Category>();
            this.CreateMap<Category, DetailsCategoryViewModel>();
            this.CreateMap<Category, CategoryViewModel>();

            // Locations
            this.CreateMap<CreateLocationInputModel, Location>().ReverseMap();
            this.CreateMap<Location, LocationViewModel>();

            // Todos
            this.CreateMap<Todo, TodoViewModel>();            
            this.CreateMap<Todo, EditTodoInputModel>().ReverseMap();
            this.CreateMap<CreateTodoInputModel, Todo>();

            // Notes
            this.CreateMap<Note, NoteViewModel>();
            this.CreateMap<Note, EditNoteInputModel>().ReverseMap();
            this.CreateMap<CreateNoteInputModel, Note>();

        }
    }
}
