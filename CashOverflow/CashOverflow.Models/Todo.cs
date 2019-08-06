using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CashOverflow.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace CashOverflow.Models
{
    public class Todo
    {
        public Todo()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public bool Alert { get; set; }

        [EnumDataType(typeof(TodoStatus))]
        public TodoStatus Status { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
