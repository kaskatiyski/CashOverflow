using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Note
{
    public class EditNoteInputModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        [EnumDataType(typeof(NoteStatus))]
        public NoteStatus Status { get; set; }

        public DateTime Date { get; set; }
    }
}
