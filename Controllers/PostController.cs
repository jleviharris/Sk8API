using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DataContext _context;

        public PostController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _context.Posts.ToListAsync();

            return Ok(posts);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<Post>> GetPost(Guid rowKey)
        {
            var post = await _context.Posts.FindAsync(rowKey);
            if (post is null)
                return BadRequest("Post not found");
            return Ok(post);
        }

        [HttpPut]
        public async Task<ActionResult<List<Post>>> AddPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return Ok(await _context.Posts.FindAsync(post.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<Post>>> UpdatePost(Post updatedPost)
        {
            var dbPost = await _context.Posts.FindAsync(updatedPost.RowKey);
            if (dbPost is null)
                return BadRequest("Post not found");
            dbPost.Content = "Test Post";


            await _context.SaveChangesAsync();

            return Ok(await _context.Posts.FindAsync(updatedPost.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Post>>> DeletePost(Guid rowKey)
        {
            var dbPost = await _context.Posts.FindAsync(rowKey);
            if (dbPost is null)
                return BadRequest("Post not found");

            _context.Posts.Remove(dbPost);
            await _context.SaveChangesAsync();

            return Ok(await _context.Posts.ToListAsync());
        }
    }
}

