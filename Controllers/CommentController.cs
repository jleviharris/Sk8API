using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase

    {
        private readonly DataContext _context;

        public CommentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _context.Comments.ToListAsync();

            return Ok(comments);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<Comment>> GetComment(Guid rowKey)
        {
            var comment = await _context.Comments.FindAsync(rowKey);
            if (comment is null)
                return BadRequest("Comment not found");
            return Ok(comment);
        }

        [HttpPut]
        public async Task<ActionResult<List<Comment>>> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return Ok(await _context.Comments.FindAsync(comment.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<Comment>>> UpdateComment(Comment updatedcomment)
        {
            var dbComment = await _context.Comments.FindAsync(updatedcomment.RowKey);
            if (dbComment is null)
                return BadRequest("Comment not found");
            dbComment.Content = "Post Test";


            await _context.SaveChangesAsync();

            return Ok(await _context.Comments.FindAsync(updatedcomment.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Comment>>> DeleteComment(Guid rowKey)
        {
            var dbComment = await _context.Comments.FindAsync(rowKey);
            if (dbComment is null)
                return BadRequest("Comment not found");

            _context.Comments.Remove(dbComment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Comments.ToListAsync());
        }
    }
}
