using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CashOverflow.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public AdminController(IMapper mapper,
                                  IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<IActionResult> Users()
        {
            var users = await this.userService.GetAllUsersAsync();

            var allUsersAdminViewModel = new AllUsersAdminViewModel()
            {
                Users = mapper.Map<IEnumerable<UserViewModel>>(users)
            };

            return View(allUsersAdminViewModel);
        }
    }
}