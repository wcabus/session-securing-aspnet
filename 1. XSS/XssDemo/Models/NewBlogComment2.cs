using System.ComponentModel.DataAnnotations;

namespace XssDemo.Models
{
    public class NewBlogComment2 : IBlogComment
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}