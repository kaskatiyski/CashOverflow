using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Todo
{
    public class EditTodoInputModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public bool Alert { get; set; }

        [Required]
        [EnumDataType(typeof(TodoStatus))]
        public TodoStatus Status { get; set; }

        public string UserId { get; set; }
    }
}
