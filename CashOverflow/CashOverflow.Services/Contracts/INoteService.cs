using CashOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services.Contracts
{
    public interface INoteService
    {
        IEnumerable<Note> GetNotes(string username);

        Task<bool> DeleteAsync(string username, string id);

        Task CreateAsync(string username, Note note);

        Task UpdateAsync(string username, Note note);

        Task<Note> GetNoteByIdAsync(string username, string id);
    }
}
