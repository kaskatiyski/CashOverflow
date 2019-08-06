using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Models
{
    public class Note
    {
        public Note()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public bool Alert { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
