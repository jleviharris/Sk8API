using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _context;

        public MessageController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _context.Messages.ToListAsync();

            return Ok(messages);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<Message>> GetMessage(Guid rowKey)
        {
            var message = await _context.Messages.FindAsync(rowKey);
            if (message is null)
                return BadRequest("Message not found");
            return Ok(message);
        }

        [HttpPut]
        public async Task<ActionResult<List<Message>>> AddMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok(await _context.Messages.FindAsync(message.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<Message>>> UpdateMessage(Message updatedMessage)
        {
            var dbMessage = await _context.Messages.FindAsync(updatedMessage.RowKey);
            if (dbMessage is null)
                return BadRequest("Message not found");
            dbMessage.Content = "Test Post";


            await _context.SaveChangesAsync();

            return Ok(await _context.Messages.FindAsync(dbMessage.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Message>>> DeleteMessage(Guid rowKey)
        {
            var dbMessage = await _context.Messages.FindAsync(rowKey);
            if (dbMessage is null)
                return BadRequest("Message not found");

            _context.Messages.Remove(dbMessage);
            await _context.SaveChangesAsync();

            return Ok(await _context.Messages.ToListAsync());
        }
    }
}
