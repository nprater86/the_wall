#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace the_wall.Models
{
    public class Message
    {
        [Key]
        public int MessageId {get; set;}
        [Required]
        public string Content {get; set;}
        [Required]
        public int UserId {get; set;}
        public User? Creator {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
        public List<Comment> CommentsOnMessage {get; set;} = new List<Comment>();

        public bool AgeUnder30Min()
        {
            DateTime now = DateTime.Now;
            TimeSpan ts = now - CreatedAt;
            return ts.TotalMinutes < 30;
        }
    }
}