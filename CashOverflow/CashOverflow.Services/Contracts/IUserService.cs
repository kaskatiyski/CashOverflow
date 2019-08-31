using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CashOverflow.Models;
using Microsoft.AspNetCore.Identity;

namespace CashOverflow.Services.Contracts
{
    public interface IUserService
    {
        Task<IdentityUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
    }
}
