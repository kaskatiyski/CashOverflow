using CashOverflow.Models;
using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategoriesByUsername(string username);

        void Create(string username, Category category);

    }
}
