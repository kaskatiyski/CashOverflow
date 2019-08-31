using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Admin
{
    public class AllUsersAdminViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
