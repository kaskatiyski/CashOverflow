using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Admin
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
    }
}
