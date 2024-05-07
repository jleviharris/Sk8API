using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly DataContext _context;

        public AppUserController(DataContext context)
        {
            _context = context;
        }

      [HttpGet]
      public async Task<IActionResult> GetAllUsers()
      {
          var users = await _context.AppUsers.ToListAsync();

          return Ok(users);
      }

      [HttpGet("{rowKey}")]
      public async Task<ActionResult<AppUser>> GetUser(Guid rowKey)
      {
          var user = await _context.AppUsers.FindAsync(rowKey);
          if (user is null)
              return BadRequest("User not found");
          return Ok(user);
      }

      [HttpPut]
      public async Task<ActionResult<List<AppUser>>> AddUser(AppUser user)
      {
          _context.AppUsers.Add(user);
          await _context.SaveChangesAsync();
          return Ok(await _context.AppUsers.FindAsync(user.RowKey));
      }


      [HttpPost]
      public async Task<ActionResult<List<AppUser>>> UpdateUser(AppUser updatedUser)
      {
          var dbUser = await _context.AppUsers.FindAsync(updatedUser.RowKey);
          if (dbUser is null)
              return BadRequest("User not found");
          dbUser.Username = "Test";



          await _context.SaveChangesAsync();
     
          return Ok(await _context.AppUsers.FindAsync(updatedUser.RowKey));
      }

        [HttpDelete]
        public async Task<ActionResult<List<AppUser>>> DeleteUser(Guid rowKey)
        {
            var dbUser = await _context.AppUsers.FindAsync(rowKey);
            if (dbUser is null)
                return BadRequest("User not found");

            _context.AppUsers.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await _context.AppUsers.ToListAsync());
        }
    }
}
