using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialplatform.Data;
using socialplatform.Models;

namespace socialplatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAll()
        {

            return await _context.Posts.Include(p => p.User).ToListAsync();
        }
        [HttpGet("feed/{userId}")]
        public async Task<ActionResult<List<Post>>> GetFeed(int userId)
        {
            var takipedilenler = await _context.Follows
                .Where(f => f.FollowerID == userId)
                .Select(f => f.FollowingID)
                .ToListAsync();

            var feed = await _context.Posts
                .Where(p => takipedilenler.Contains(p.UserID))
                .Include(p => p.User)
                .OrderByDescending(p => p.Zaman)
                .ToListAsync();

            return feed;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Create(Post yeniPost)
        {
            _context.Posts.Add(yeniPost);
            await _context.SaveChangesAsync();
            return Ok(yeniPost);
        }
    }
}