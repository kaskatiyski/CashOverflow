﻿using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Todo
{
    public class CreateTodoInputModel
    {
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public bool Alert { get; set; }

        public TodoStatus Status { get; set; }
    }
}
