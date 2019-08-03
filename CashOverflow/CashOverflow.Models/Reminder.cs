using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Models
{
    public class Reminder
    {
        public Reminder()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Note { get; set; }

        public DateTime Date { get; set; }

        public bool Alert { get; set; }
    }
}
