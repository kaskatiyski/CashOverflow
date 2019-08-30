using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Note
{
    public class CreateNoteInputModel
    {
        [Required]
        public string Content { get; set; }

        public string BackgroundColor { get; set; }

        public string TextColor { get; set; }

        public bool IsSticky { get; set; }
    }
}
