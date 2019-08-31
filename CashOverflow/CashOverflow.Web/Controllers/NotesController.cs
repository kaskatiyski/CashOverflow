using AutoMapper;
using CashOverflow.Models;
using CashOverflow.Models.Enum;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly IMapper mapper;
        private readonly INoteService noteService;

        public NotesController(IMapper mapper,
                               INoteService noteService)
        {
            this.mapper = mapper;
            this.noteService = noteService;
        }

        private void SetReturnUrl()
        {
            this.ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
        }

        public async Task<IActionResult> All()
        {
            var notes = await this.noteService.GetNotes(this.User.Identity.Name);

            AllNotesViewModel allNotesViewModel = new AllNotesViewModel
            {
                Notes = notes.Select(note => mapper.Map<NoteViewModel>(note))
            };

            return View(allNotesViewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await this.noteService.GetNoteByIdAsync(this.User.Identity.Name, id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        public IActionResult Create()
        {
            SetReturnUrl();

            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNoteInputModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await this.noteService.CreateAsync(this.User.Identity.Name, mapper.Map<Note>(model));

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }
            }

            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await this.noteService.GetNoteByIdAsync(this.User.Identity.Name, id);

            if (note == null)
            {
                return NotFound();
            }

            SetReturnUrl();

            var editNoteInputModel = mapper.Map<EditNoteInputModel>(note);

            return View(editNoteInputModel);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditNoteInputModel model, string returnUrl)
        {
            var note = mapper.Map<Note>(model);

            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.noteService.UpdateAsync(this.User.Identity.Name, note);

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }
            }

            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(string id)
        {            
            return await this.noteService.DeleteAsync(this.User.Identity.Name, id);
        }
    }
}
