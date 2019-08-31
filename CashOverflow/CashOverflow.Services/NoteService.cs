using CashOverflow.Models;
using CashOverflow.Models.Enum;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashOverflow.Services
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;

        public NoteService(ApplicationDbContext db,
                               IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        public async Task CreateAsync(string username, Note note)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);

            note.Status = NoteStatus.NotArchived;
            note.UserId = user.Id;

            this.db.Add(note);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(string username, string id)
        {
            var note = await this.GetNoteByIdAsync(username, id);

            try
            {
                this.db.Notes.Remove(note);
                await this.db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Note> GetNoteByIdAsync(string username, string id)
        {
            var note = await this.db.Notes
                .FirstOrDefaultAsync(n => n.User.UserName == username && n.Id == id);

            return note;
        }

        public async Task<IEnumerable<Note>> GetNotes(string username)
        {
            var notes = await this.db.Notes.Where(note => note.User.UserName == username).ToListAsync();

            return notes;
        }

        public async Task UpdateAsync(string username, Note note)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);

            note.UserId = user.Id;

            this.db.Notes.Update(note);
            await this.db.SaveChangesAsync();
        }
    }
}
