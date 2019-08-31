using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public async Task<IdentityUser> GetUserByUsernameAsync(string username)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            return user;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            var users = await this.db.Users.ToListAsync();

            return users;
        }
    }
}
