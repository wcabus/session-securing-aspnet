using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Securing.AspNet.Domain;

namespace Securing.AspNet.Models.BlogController
{
    public class PostWithComments
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Published at")]
        public DateTimeOffset PublishedAt { get; set; }

        [Display(Name = "Written by")]
        public string Author { get; set; }

        public string Contents { get; set; }

        public ICollection<Comment> Comments { get; set; } 

        public static PostWithCommentCount Map(BlogPost p, int count)
        {
            return new PostWithCommentCount
            {
                Id = p.Id,
                Title = p.Title,
                PublishedAt = p.PublishedAt,
                Author = p.Author,
                CommentCount = count,
                Contents = p.Contents
            };
        }

        public static PostWithComments Map(BlogPost p)
        {
            return new PostWithComments
                {
                    Id = p.Id,
                    Title = p.Title,
                    PublishedAt = p.PublishedAt,
                    Author = p.Author,
                    Contents = p.Contents,
                    Comments = p.Comments.Select(Comment.Map).ToList()
                };
        } 
    }
}