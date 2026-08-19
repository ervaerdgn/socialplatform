using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialplatform.Data;
using socialplatform.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace socialplatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetAll()
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .ToListAsync();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Like>> Create(Like newcomment)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            newcomment.UserID = userId;

            _context.Likes.Add(newcomment);
            await _context.SaveChangesAsync();

            var post = await _context.Posts.FindAsync(newcomment.PostID);
            if (post != null && post.UserID != userId)
            {
                var bildirim = new Notification
                {
                    UserID = post.UserID,
                    Mesaj = "Paylaşımına Yorum Yapıldı"
                };
                _context.Notifications.Add(bildirim);
                await _context.SaveChangesAsync();
            }

            return Ok(newcomment);
        }
    }
}