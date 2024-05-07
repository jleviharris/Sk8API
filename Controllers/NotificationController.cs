using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Data;
using SkateAPI.Enities;

namespace SkateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly DataContext _context;

        public NotificationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _context.Notifications.ToListAsync();

            return Ok(notifications);
        }

        [HttpGet("{rowKey}")]
        public async Task<ActionResult<Notification>> GetNotification(Guid rowKey)
        {
            var notification = await _context.Notifications.FindAsync(rowKey);
            if (notification is null)
                return BadRequest("Notification not found");
            return Ok(notification);
        }

        [HttpPut]
        public async Task<ActionResult<List<Notification>>> AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return Ok(await _context.Notifications.FindAsync(notification.RowKey));
        }


        [HttpPost]
        public async Task<ActionResult<List<Notification>>> UpdateNotification(Notification updatedNotification)
        {
            var dbNotification = await _context.Notifications.FindAsync(updatedNotification.RowKey);
            if (dbNotification is null)
                return BadRequest("Notification not found");
            dbNotification.Message = "Test Post";


            await _context.SaveChangesAsync();

            return Ok(await _context.Notifications.FindAsync(updatedNotification.RowKey));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Notification>>> DeleteNotification(Guid rowKey)
        {
            var dbNotification = await _context.Notifications.FindAsync(rowKey);
            if (dbNotification is null)
                return BadRequest("Notification not found");

            _context.Notifications.Remove(dbNotification);
            await _context.SaveChangesAsync();

            return Ok(await _context.Notifications.ToListAsync());
        }
    }
}

