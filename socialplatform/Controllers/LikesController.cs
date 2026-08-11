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
    public class LikesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LikesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Like>>> GetAll()
        {
            return await _context.Likes
                .Include(l => l.User)
                .Include(l => l.Post)
                .ToListAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Like>> Create(Like yeniLike)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            yeniLike.UserID = userId;

            _context.Likes.Add(yeniLike);
            await _context.SaveChangesAsync();
            return Ok(yeniLike);
        }
    }
}