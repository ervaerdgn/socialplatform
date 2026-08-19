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
    public class FollowsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FollowsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Follow>>> GetAll()
        {
            return await _context.Follows
                .Include(f => f.Follower)
                .Include(f => f.Following)
                .ToListAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Follow>> Create(Follow newfollow)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           newfollow.FollowerID = userId;

            _context.Follows.Add(newfollow);
            await _context.SaveChangesAsync();

            var bildirim = new Notification
            {
                UserID = newfollow.FollowingID,
                Mesaj = "Seni takip etmeye başladı"
            };
            _context.Notifications.Add(bildirim);
            await _context.SaveChangesAsync();

            return Ok(newfollow);
        }
    }
}