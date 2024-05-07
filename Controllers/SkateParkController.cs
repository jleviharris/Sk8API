using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkateParkController : ControllerBase
    {
        private readonly DataContext _context;

        public SkateParkController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkateParks()
        {
            var skateParks = await _context.SkateParks.ToListAsync();

            return Ok(skateParks);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<SkatePark>> GetSkatePark(Guid rowKey)
        {
            var skatePark = await _context.SkateParks.FindAsync(rowKey);
            if (skatePark is null)
                return BadRequest("SkatePark not found");
            return Ok(skatePark);
        }

        [HttpPut]
        public async Task<ActionResult<List<SkatePark>>> AddSkatePark(SkatePark skatePark)
        {
            _context.SkateParks.Add(skatePark);
            await _context.SaveChangesAsync();
            return Ok(await _context.SkateParks.FindAsync(skatePark.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<SkatePark>>> UpdateSkatePark(SkatePark updatedSkatePark)
        {
            var dbSkatePark = await _context.SkateParks.FindAsync(updatedSkatePark.RowKey);
            if (dbSkatePark is null)
                return BadRequest("SkatePark not found");
            dbSkatePark.Fenced = "T";


            await _context.SaveChangesAsync();

            return Ok(await _context.SkateParks.FindAsync(updatedSkatePark.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<SkatePark>>> DeleteSkatePark(Guid rowKey)
        {
            var dbSkatePark = await _context.SkateParks.FindAsync(rowKey);
            if (dbSkatePark is null)
                return BadRequest("SkatePark not found");

            _context.SkateParks.Remove(dbSkatePark);
            await _context.SaveChangesAsync();

            return Ok(await _context.SkateParks.ToListAsync());
        }
    }
}

