using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly DataContext _context;

        public AddressController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdresses()
        {
            var addresses = await _context.Address.ToListAsync();

            return Ok(addresses);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<Address>> GetAddress(Guid rowKey)
        {
            var address = await _context.Address.FindAsync(rowKey);
            if (address is null)
                return BadRequest("Address not found");
            return Ok(address);
        }

        [HttpPut]
        public async Task<ActionResult<List<Address>>> AddAddress(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();
            return Ok(await _context.AppUsers.FindAsync(address.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<Address>>> UpdateAddress(Address updatedAddress)
        {
            var dbAddress = await _context.Address.FindAsync(updatedAddress.RowKey);
            if (dbAddress is null)
                return BadRequest("Address not found");
            dbAddress.StreetName = "Post Test";


            await _context.SaveChangesAsync();

            return Ok(await _context.AppUsers.FindAsync(updatedAddress.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Address>>> DeleteAddress(Guid rowKey)
        {
            var dbAddress = await _context.Address.FindAsync(rowKey);
            if (dbAddress is null)
                return BadRequest("Address not found");

            _context.Address.Remove(dbAddress);
            await _context.SaveChangesAsync();

            return Ok(await _context.Address.ToListAsync());
        }
    }
}