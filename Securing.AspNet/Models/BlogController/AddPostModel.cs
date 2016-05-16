using System;
using System.ComponentModel.DataAnnotations;
using Securing.AspNet.Domain;

namespace Securing.AspNet.Models.BlogController
{
    public class AddPostModel
    {
        [Required, StringLength(100)]
        public string Author { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Contents { get; set; }

        public BlogPost ToEntity()
        {
            return new BlogPost
            {
                Author = Author,
                Title = Title,
                Contents = Contents,
                PublishedAt = DateTimeOffset.UtcNow
            };
        }
    }
}