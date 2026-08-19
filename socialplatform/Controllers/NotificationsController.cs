using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialplatform.Data;
using socialplatform.Models;
using System.Security.Claims;

namespace socialplatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Notification>>> GetAll()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return await _context.Notifications
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.Zaman)
                .ToListAsync();
        }

        [HttpPut("{id}/okundu")]
        [Authorize]
        public async Task<ActionResult> OkunduIsaretle(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var bildirim = await _context.Notifications.FindAsync(id);

            if (bildirim == null)
                return NotFound();

            if (bildirim.UserID != userId)
                return Forbid();

            bildirim.Okundu = true;
            await _context.SaveChangesAsync();

            return Ok(bildirim);
        }
    }
}