using System;

namespace Securing.AspNet.Models.BlogController
{
    public class DeleteCommentModel
    {
        public Guid Id { get; set; } 
        public Guid PostId { get; set; }
    }
}