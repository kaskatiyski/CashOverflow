using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CashOverflow.Models;
using CashOverflow.Web.Data;
using CashOverflow.Web.ViewModels.Todo;
using AutoMapper;
using CashOverflow.Services.Contracts;
using CashOverflow.Models.Enum;

namespace CashOverflow.Web.Controllers
{
    public class TodosController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly ITodoService todoService;

        public TodosController(IMapper mapper,
                               ITodoService todoService,
                               ApplicationDbContext db)
        {
            this.db = db;
            this.mapper = mapper;
            this.todoService = todoService;
        }

        // GET: Todoes
        public async Task<IActionResult> All()
        {
            var todos = await db.Todos.ToListAsync();

            AllTodosViewModel allTodosViewModel = new AllTodosViewModel();
            allTodosViewModel.Todos = todos.Select(todo => mapper.Map<TodoViewModel>(todo));

            return View(allTodosViewModel);
        }

        // GET: Todoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await db.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTodoInputModel model)
        {
            model.Status = TodoStatus.Pending;

            if (ModelState.IsValid)
            {
                await this.todoService.CreateAsync(this.User.Identity.Name, mapper.Map<Todo>(model));
            }

            return this.RedirectToAction(nameof(All));
        }

        // GET: Todoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await db.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content,Date,Alert,Status")] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(todo);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(All));
            }
            return View(todo);
        }

        // GET: Todoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await db.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var todo = await db.Todos.FindAsync(id);
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }

        private bool TodoExists(string id)
        {
            return db.Todos.Any(e => e.Id == id);
        }
    }
}
