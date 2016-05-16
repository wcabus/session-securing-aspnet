using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Securing.AspNet.Domain
{
    public class BlogPostComment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid Id { get; set; }

        [Required]
        public Guid PostId { get; set; }
        public virtual BlogPost Post { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}