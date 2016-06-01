using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NWebsec.Mvc.HttpHeaders.Csp;
using Securing.AspNet.Domain;
using Securing.AspNet.Models.BlogController;

namespace Securing.AspNet.Controllers
{
    [AllowAnonymous, CspScriptSrcReportOnly(CustomSources = "*.realdolmen.com")]
    public class BlogController : Controller
    {
        private readonly BlogContext _dbContext = new BlogContext();

        // GET: Blog
        public async Task<ActionResult> Index()
        {
            var data = await GetPostsWithCommentCountAsync();

            return View(data);
        }

        // GET: Blog/Read/<guid>
        public async Task<ActionResult> Read(Guid id)
        {
            var data = await GetPostWithCommentsAsync(id);

            return View(data);
        }

        // GET: Blog/AddPost
        [Authorize]
        public ActionResult AddPost()
        {
            return View(new AddPostModel { Author = User.Identity.Name });
        }

        // POST: Blog/AddPost
        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPost(AddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = await AddPostAsync(model);
            return RedirectToAction("Read", new { id });
        }

        // GET: Blog/AddComment/<guid>
        public ActionResult AddComment(Guid id)
        {
            ViewBag.Id = id;
            var model = new AddCommentModel();

            if (Request.IsAuthenticated)
            {
                model.Email = User.Identity.Name;
                model.Name = User.Identity.Name;
            }

            return View(model);
        }

        // POST: Blog/AddComment/<guid>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(Guid id, AddCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(model);
            }

            await AddCommentToPostAsync(id, model);
            return RedirectToAction("Read", new { id });
        }

        // POST: Blog/DeleteComment
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteComment(DeleteCommentModel model)
        {
            if (ModelState.IsValid)
            {
                await DeleteCommentAsync(model.Id);
            }
            
            return RedirectToAction("Read", new { Id = model.PostId });
        }




        /*
        if (!string.IsNullOrEmpty(model?.Comment))
        {
            var s = new Ganss.XSS.HtmlSanitizer();
            model.Comment = s.Sanitize(model.Comment);
        } 
        */


        private async Task<IEnumerable<PostWithCommentCount>> GetPostsWithCommentCountAsync()
        {
            var posts = await _dbContext
                .Set<BlogPost>()
                .OrderByDescending(p => p.PublishedAt)
                .Select(p => new { Post = p, CommentCount = p.Comments.Count })
                .ToListAsync();

            return posts.Select(p => PostWithCommentCount.Map(p.Post, p.CommentCount));
        }

        private async Task<PostWithComments> GetPostWithCommentsAsync(Guid id)
        {
            var post = await _dbContext
                .Set<BlogPost>()
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == id);

            return PostWithComments.Map(post);
        }

        private async Task<Guid> AddPostAsync(AddPostModel model)
        {
            var entity = model.ToEntity();

            _dbContext.Set<BlogPost>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        private async Task AddCommentToPostAsync(Guid id, AddCommentModel model)
        {
            var entity = model.ToEntity();
            entity.PostId = id;

            _dbContext.Set<BlogPostComment>().Add(entity);

            await _dbContext.SaveChangesAsync();
        }

        private async Task DeleteCommentAsync(Guid id)
        {
            var set = _dbContext.Set<BlogPostComment>();
            var comment = await set.SingleOrDefaultAsync(x => x.Id == id);

            if (comment != null)
            {
                set.Remove(comment);
                await _dbContext.SaveChangesAsync();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}