using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace CashOverflow.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;

        public UserService(ApplicationDbContext db,
                            UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
    }
}
