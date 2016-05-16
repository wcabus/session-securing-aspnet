using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Securing.AspNet.Domain
{
    public class BlogPost
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Author { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        public DateTimeOffset PublishedAt { get; set; }

        public ICollection<BlogPostComment> Comments { get; set; } 
    }
}