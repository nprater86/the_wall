using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

using the_wall.Models;
namespace the_wall.Controllers;

public class CommentController : Controller
    {
    private MyContext _context;

    public CommentController(MyContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateComment(Comment newComment)
    {
        _context.Add(newComment);
        _context.SaveChanges();
        return Redirect("/");
        // if(ModelState.IsValid)
        // {
        //     Console.WriteLine("-------------------is valid!-------------------------");
        //     _context.Add(newComment);
        //     _context.SaveChanges();
        //     return Redirect("/");
        // }
        // else
        // {
        //     Console.WriteLine("-------------------kicked out!-------------------------");
        //     Console.WriteLine($"newComment MessageId = {newComment.MessageId}");
        //     Console.WriteLine($"newComment UserId = {newComment.UserId}");
        //     Console.WriteLine($"newComment Content = {newComment.Content}");
        //     List<Message> messagesWithUserAndComments = _context.Messages
        //     .Include(m => m.Creator)
        //     .Include(m => m.CommentsOnMessage)
        //     .ThenInclude(c => c.User).ToList();
        //     ViewBag.Messages = messagesWithUserAndComments;
        //     return View("../Home/Index");
        // }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}