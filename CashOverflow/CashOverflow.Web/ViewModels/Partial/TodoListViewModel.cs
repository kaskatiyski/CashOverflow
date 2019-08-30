using CashOverflow.Web.ViewModels.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Partial
{
    public class TodoListViewModel
    {
        public bool HideOption { get; set; } = false;

        public IEnumerable<TodoViewModel> Todos { get; set; }

    }
}
