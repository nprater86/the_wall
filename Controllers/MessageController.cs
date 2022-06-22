using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

using the_wall.Models;
namespace the_wall.Controllers;

public class MessageController : Controller
    {
    private MyContext _context;

    public MessageController(MyContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateMessage(Message newMessage)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newMessage);
            _context.SaveChanges();
            return Redirect("/");
        }
        else
        {
            List<Message> messagesWithUserAndComments = _context.Messages
            .Include(m => m.Creator)
            .Include(m => m.CommentsOnMessage)
            .ThenInclude(c => c.User).ToList();
            ViewBag.Messages = messagesWithUserAndComments;
            return View("../Home/Index");
        }
    }

    [HttpGet("/messages/delete/{MessageId}")]
    public IActionResult DeleteMessage(int MessageId)
    {
        var messageInDb = _context.Messages.FirstOrDefault(m => m.MessageId == MessageId);
        if(messageInDb != null)
        {
            if(messageInDb.UserId == HttpContext.Session.GetInt32("UserId") && messageInDb.AgeUnder30Min())
            {
                _context.Messages.Remove(messageInDb);
                _context.SaveChanges();
                return Redirect("/");
            }
            else
            {
                return Redirect("/");
            }
        }
        else
        {
            return Redirect("/");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}