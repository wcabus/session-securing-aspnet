using System;
using System.ComponentModel.DataAnnotations;
using Securing.AspNet.Domain;

namespace Securing.AspNet.Models.BlogController
{
    public class PostWithCommentCount
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Published at")]
        public DateTimeOffset PublishedAt { get; set; }

        [Display(Name = "Written by")]
        public string Author { get; set; }

        [Display(Name = "Comments")]
        public int CommentCount { get; set; }

        public string Contents { get; set; }

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
    }
}