//using CashOverflow.Data;
//using CashOverflow.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace CashOverflow.App.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly CashOverflowDbContext db;

//        public UsersController(CashOverflowDbContext db)
//        {
//            this.db = db;
//        }

//        public IActionResult Login()
//        {
//            return View();
//        }

//        private string HashPassword(string password)
//        {
//            using (SHA256 sha256Hash = SHA256.Create())
//            {
//                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
//            }
//        }

//        public IActionResult LoginConfirm(string username, string password, string passwordConfirm, string email)
//        {
//            if (password != passwordConfirm)
//            {
//                return this.RedirectToAction("Register");
//            }

//            var user = new User
//            {
//                Username = username,
//                Password = this.HashPassword(password),
//                Email = email
//            };

//            db.Add(user);
//            db.SaveChanges();
//            db.Dispose();

//            return this.Redirect("/");
//        }

//        public IActionResult Register()
//        {
//            return View();
//        }

//        public IActionResult RegisterConfirm()
//        {
//            return null;
//        }
//    }
//}
