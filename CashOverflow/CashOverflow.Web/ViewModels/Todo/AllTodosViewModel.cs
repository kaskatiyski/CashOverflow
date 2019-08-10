using CashOverflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashOverflow.Web.ViewModels.Todo
{
    public class AllTodosViewModel
    {
        public IEnumerable<TodoViewModel> Todos { get; set; }
    }
}
