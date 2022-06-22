using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

using the_wall.Models;
namespace the_wall.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    
    public HomeController(MyContext context)
    {
        _context = context;
    }
    [HttpGet("")]

    public IActionResult Index()
    {
        List<Message> messagesWithUserAndComments = _context.Messages
        .OrderByDescending(m => m.CreatedAt)
        .Include(m => m.Creator)
        .Include(m => m.CommentsOnMessage)
        .ThenInclude(c => c.User).ToList();
        ViewBag.Messages = messagesWithUserAndComments;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
