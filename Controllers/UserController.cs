using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

using the_wall.Models;
namespace the_wall.Controllers;

public class UserController : Controller
    {
    private MyContext _context;

    public UserController(MyContext context)
    {
        _context = context;
    }

    //------------------------------------------------------
    // HANDLE REGISTRATION
    //------------------------------------------------------

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SaveUser(User newUser)
    {
        if(ModelState.IsValid)
        {
            if(_context.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email","Email already in use");
                return View("Register");
            }
            else
            {
                PasswordHasher<User> Haser = new PasswordHasher<User>();
                newUser.Password = Haser.HashPassword(newUser, newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                HttpContext.Session.SetString("UserName",newUser.FirstName);
                return Redirect("/");
            }
        }
        else
        {
            return View("Register");
        }
    }

    //------------------------------------------------------
    // HANDLE LOGIN
    //------------------------------------------------------

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LoginUser(LoginUser submittedUser)
    {
        if(ModelState.IsValid)
        {
            User userInDb = _context.Users.FirstOrDefault(u => u.Email == submittedUser.LoginEmail);

            if(userInDb == null)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                return View("Login");
            }

            var hasher = new PasswordHasher<LoginUser>();

            var result = hasher.VerifyHashedPassword(submittedUser, userInDb.Password, submittedUser.LoginPassword);

            if(result == 0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                return View("Login");
            }

            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            HttpContext.Session.SetString("UserName", userInDb.FirstName);
            return Redirect("/");
        }
        else
        {
            return View("Login");
        }
    }

    //------------------------------------------------------
    // HANDLE LOGOUT
    //------------------------------------------------------

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}