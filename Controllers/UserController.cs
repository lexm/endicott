using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using endicott.Models;

namespace endicott.Controllers
{
    public class UserController : Controller
    {
        private EndicottContext _context;

        public UserController(EndicottContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult LogReg()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel user)
        {
            System.Console.WriteLine("start");
            if (ModelState.IsValid)
            {
                System.Console.WriteLine("valid");
                string UserEmail = user.Email;
                User LookupUser = _context.Users.SingleOrDefault(login => login.Email == UserEmail);
                if (LookupUser == null)
                {
                    PasswordHasher<UserViewModel> Hasher = new PasswordHasher<UserViewModel>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    System.Console.WriteLine("length is {0}", user.Password.Length);
                    User NewUser = new User
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.Password,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    _context.Add(NewUser);
                    _context.SaveChanges();
                    NewUser = _context.Users.SingleOrDefault(login => login.Email == UserEmail);
                    HttpContext.Session.SetInt32("userid", NewUser.userid);
                    HttpContext.Session.SetString("email", NewUser.Email);
                    System.Console.WriteLine("Iz guud!");
                    // return RedirectToAction("Plan", "Wedding");
                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError("email", "This Email is already registered.");
                    ViewBag.errors = ModelState.Values;
                    return View("~/Views/User/LogReg.cshtml", user);
                }
            }
            else
            {
                System.Console.WriteLine("Iz nah guud.");
                return View("~/Views/User/LogReg.cshtml", user);
            }
        }

        [HttpPost]
        [Route("Login/login")]
        public IActionResult Login(UserViewModel user)
        {
            string UserEmail = user.Email;
            User LookupUser = _context.Users.SingleOrDefault(login => login.Email == UserEmail);
            if (LookupUser != null)
            {
                PasswordHasher<UserViewModel> Hasher = new PasswordHasher<UserViewModel>();
                string CheckPassword = Hasher.HashPassword(user, user.Password);
                // if(LookupUser.Password == CheckPassword)
                if (0 != Hasher.VerifyHashedPassword(user, LookupUser.Password, user.Password))
                {
                    HttpContext.Session.SetInt32("userid", LookupUser.userid);
                    HttpContext.Session.SetString("email", LookupUser.Email);
                    // return RedirectToAction("Plan", "Wedding");
                    return RedirectToAction("Success");
                }
            }
            ModelState.AddModelError("Password", "User/password mismatch");
            ViewBag.errors = ModelState.Values;
            return View("~/Views/User/LogReg.cshtml", user);
        }


        public IActionResult Success()
        {
            return View("~/Views/User/Success.cshtml");
            // return RedirectToAction("Plan", "Wedding");
        }
    }
}