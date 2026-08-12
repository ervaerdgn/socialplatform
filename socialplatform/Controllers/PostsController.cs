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
    public class PostsController : ControllerBase
    {
       
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env; 

        public PostsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env; 
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
        [Authorize]
        public async Task<ActionResult<Post>> Create(Post yeniPost)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            yeniPost.UserID = userId;

            _context.Posts.Add(yeniPost);
            await _context.SaveChangesAsync();
            return Ok(yeniPost);
        }
        [HttpPost("{postId}/upload-resim")]
        [Authorize]
        public async Task<ActionResult> UploadPostResmi(int postId, IFormFile file)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var post = await _context.Posts.FindAsync(postId);

            if (post == null)
                return NotFound();

            if (post.UserID != userId)
                return Forbid();

            var uploadsKlasoru = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsKlasoru))
                Directory.CreateDirectory(uploadsKlasoru);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var tamYol = Path.Combine(uploadsKlasoru, dosyaAdi);

            using (var stream = new FileStream(tamYol, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            post.Photo = "/uploads/" + dosyaAdi;
            await _context.SaveChangesAsync();

            return Ok(new { resim = post.Photo });
        }
    }
}