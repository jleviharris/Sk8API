using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly DataContext _context;

        public LikeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLikes()
        {
            var likes = await _context.Likes.ToListAsync();

            return Ok(likes);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<Like>> GetLike(Guid rowKey)
        {
            var like = await _context.Likes.FindAsync(rowKey);
            if (like is null)
                return BadRequest("Like not found");
            return Ok(like);
        }

        [HttpPut]
        public async Task<ActionResult<List<Like>>> AddLike(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
            return Ok(await _context.Likes.FindAsync(like.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<Like>>> UpdateLike(Like updatedLike)
        {
            var dbLike = await _context.Likes.FindAsync(updatedLike.RowKey);
            if (dbLike is null)
                return BadRequest("Like not found");
            dbLike.LikeParentTypeCd = "T";


            await _context.SaveChangesAsync();

            return Ok(await _context.Likes.FindAsync(updatedLike.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Like>>> DeleteLike(Guid rowKey)
        {
            var dbLike = await _context.Likes.FindAsync(rowKey);
            if (dbLike is null)
                return BadRequest("Like not found");

            _context.Likes.Remove(dbLike);
            await _context.SaveChangesAsync();

            return Ok(await _context.Likes.ToListAsync());
        }
    }
}

