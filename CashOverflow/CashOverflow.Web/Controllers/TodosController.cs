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
using Microsoft.AspNetCore.Authorization;

namespace CashOverflow.Web.Controllers
{
    [Authorize]
    public class TodosController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITodoService todoService;

        public TodosController(IMapper mapper,
                               ITodoService todoService)
        {
            this.mapper = mapper;
            this.todoService = todoService;
        }

        private void SetReturnUrl()
        {
            this.ViewData["ReturnUrl"] = Request.Headers["Referer"].ToString();
        }

        public async Task<IActionResult> All()
        {
            var todos = await this.todoService.GetTodos(this.User.Identity.Name);

            AllTodosViewModel allTodosViewModel = new AllTodosViewModel
            {
                Todos = todos.Select(todo => mapper.Map<TodoViewModel>(todo))
            };

            return View(allTodosViewModel);
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
        public async Task<IActionResult> Create(CreateTodoInputModel model, string returnUrl)
        {
            model.Status = TodoStatus.Pending;

            if (ModelState.IsValid)
            {
                await this.todoService.CreateAsync(this.User.Identity.Name, mapper.Map<Todo>(model));

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

            var todo = await this.todoService.GetTodoByIdAsync(this.User.Identity.Name, id);

            if (todo == null)
            {
                return NotFound();
            }

            SetReturnUrl();

            var editTodoInputModel = mapper.Map<EditTodoInputModel>(todo);

            return View(editTodoInputModel);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditTodoInputModel model, string returnUrl)
        {
            var todo = mapper.Map<Todo>(model);

            if (id != todo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.todoService.UpdateTodoAsync(this.User.Identity.Name, todo);

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(All));
                }
            }

            return View(todo);
        }
        
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(string id)
        {          
            return await this.todoService.DeleteTodoAsync(this.User.Identity.Name, id);
        }

        public async Task<JsonResult> Complete(string id)
        {
            var todo = await this.todoService.GetTodoByIdAsync(this.User.Identity.Name, id);

            var status = await this.todoService.CompleteAsync(this.User.Identity.Name, todo);

            return this.Json(status);
        }
    }
}
