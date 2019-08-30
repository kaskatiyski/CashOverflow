using CashOverflow.Models.Enum;
using CashOverflow.Web.ViewModels.Todo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Dashboard
{
    public class DashboardNoteViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        [EnumDataType(typeof(NoteStatus))]
        public NoteStatus Status { get; set; }

        public DateTime DateCreated { get; set; }

        public string BackgroundColor { get; set; }

        public string TextColor { get; set; }

        public bool IsSticky { get; set; }
    }
}
