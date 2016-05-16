using System;
using System.ComponentModel.DataAnnotations;
using Securing.AspNet.Domain;

namespace Securing.AspNet.Models.BlogController
{
    public class Comment
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Contents { get; set; }

        public static Comment Map(BlogPostComment c)
        {
            return new Comment
            {
                Id = c.Id,
                Name = c.Name,
                Contents = c.Comment
            };
        } 
    }
}