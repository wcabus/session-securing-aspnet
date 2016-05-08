using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace XssDemo.Models
{
    public class NewBlogComment
    {
        [EmailAddress, Required]
        public string Email { get; set; } 

        [Required, AllowHtml, DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}