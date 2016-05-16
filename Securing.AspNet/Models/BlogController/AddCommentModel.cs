using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Securing.AspNet.Domain;

namespace Securing.AspNet.Models.BlogController
{
    public class AddCommentModel
    {
        [Required, StringLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, DataType(DataType.MultilineText), AllowHtml]
        public string Comment { get; set; }

        public BlogPostComment ToEntity()
        {
            return new BlogPostComment
            {
                Email = Email,
                Name = Name,
                Comment = Comment
            };
        }
    }
}