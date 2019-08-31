using CashOverflow.Models;
using CashOverflow.Models.Enum;
using CashOverflow.Services.Contracts;
using CashOverflow.Utilities.Exceptions;
using CashOverflow.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;
        private readonly ITransactionService transactionService;

        public CategoryService(ApplicationDbContext db,
                               IUserService userService,
                               ITransactionService transactionService)
        {
            this.db = db;
            this.userService = userService;
            this.transactionService = transactionService;
        }

        public async Task CreateAsync(string username, Category category)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);

            category.UserId = user.Id;

            this.db.Add(category);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<Category> GetCategoriesByUsername(string username)
        {
            var categories = this.db.Categories
                .Include(x => x.Transactions)
                .Where(x => x.User.UserName == username);

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(string username, string id)
        {
            var category = await this.db.Categories
                .FirstOrDefaultAsync(c => c.User.UserName == username && c.Id == id);

            return category;

        }

        public async Task DeleteCategoryAsync(string username, string id)
        {
            var category = await this.GetCategoryByIdAsync(username, id);

            bool categoryIsEmpty = await this.transactionService.CategoryIsEmpty(username, category.Id);

            if (categoryIsEmpty)
            {
                throw new CategoryNotEmptyException("You cannot delete this category, because it has transactions.");
            }

            this.db.Categories.Remove(category);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(string username, Category category)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);

            category.UserId = user.Id;

            this.db.Categories.Update(category);
            await this.db.SaveChangesAsync();
        }        
    }
}
