using System.Data.Entity;

namespace Securing.AspNet.Domain
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("BlogDB")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
                .HasMany(p => p.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<BlogPostComment>();
        }
    }
}