using CashOverflow.Models;
using CashOverflow.Models.Enum;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashOverflow.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;

        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string username, Category category)
        {
            category.UserId = this.db.Users.FirstOrDefault(u => u.UserName == username).Id;

            this.db.Add(category);
            this.db.SaveChanges();
        }

        public IEnumerable<Category> GetCategoriesByUsername(string username)
        {
            var categories = this.db.Categories.Where(x => x.User.UserName == username);

            return categories;
        }
    }
}
