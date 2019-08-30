using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Note
{
    public class AllNotesViewModel
    {
        public IEnumerable<NoteViewModel> Notes { get; set; }
    }
}
