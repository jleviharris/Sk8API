using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly DataContext _context;

        public FriendshipController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFriendships()
        {
            var friendships = await _context.Friendships.ToListAsync();

            return Ok(friendships);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<Friendship>> GetFriendship(Guid rowKey)
        {
            var friendship = await _context.Friendships.FindAsync(rowKey);
            if (friendship is null)
                return BadRequest("Friendship not found");
            return Ok(friendship);
        }

        [HttpPut]
        public async Task<ActionResult<List<Friendship>>> AddFriendships(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
            return Ok(await _context.Friendships.FindAsync(friendship.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<Friendship>>> UpdateFriendship(Friendship updatedFriendship)
        {
            var dbFriendship = await _context.Friendships.FindAsync(updatedFriendship.RowKey);
            if (dbFriendship is null)
                return BadRequest("Friendship not found");
            dbFriendship.FriendsInd = "T";


            await _context.SaveChangesAsync();

            return Ok(await _context.Friendships.FindAsync(updatedFriendship.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Friendship>>> DeleteFriendships(Guid rowKey)
        {
            var dbFriendship = await _context.Friendships.FindAsync(rowKey);
            if (dbFriendship is null)
                return BadRequest("Friendship not found");

            _context.Friendships.Remove(dbFriendship);
            await _context.SaveChangesAsync();

            return Ok(await _context.Friendships.ToListAsync());
        }
    }
}
