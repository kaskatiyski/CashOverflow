using CashOverflow.Web.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class EditTransactionInputModel
    {
        public string Id { get; set; }

        [Required]
        public decimal Ammount { get; set; }

        public string Recipient { get; set; }

        public string Notes { get; set; }
                
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Category")]
        [Required]
        public string CategoryId { get; set; }

        public virtual CreateTransactionCaregoryViewModel Category { get; set; }

        public string LocationId { get; set; }

        public virtual CreateLocationInputModel Location { get; set; }        
    }
}
