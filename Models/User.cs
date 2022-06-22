#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace the_wall.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set;}
        [Required]
        [Display(Name ="First Name")]
        public string FirstName {get; set;}
        [Required]
        [Display(Name ="Last Name")]
        public string LastName {get; set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        [Compare("Password")]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
        public List<Message> CreatedMessages {get; set;} = new List<Message>();
        public List<Comment> CreatedComments {get; set;} = new List<Comment>();
    }
}