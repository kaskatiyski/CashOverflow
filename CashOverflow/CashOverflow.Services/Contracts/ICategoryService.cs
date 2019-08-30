using CashOverflow.Models;
using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategoriesByUsername(string username);

        Task<Category> GetCategoryByIdAsync(string username, string id);

        Task CreateAsync(string username, Category category);

        Task UpdateCategoryAsync(string username, Category category);

        Task DeleteCategoryAsync(string username, string id);
    }
}
