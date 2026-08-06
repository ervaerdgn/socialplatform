using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialplatform.Data;
using socialplatform.Models;

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
        public async Task<ActionResult<Like>> Create(Like yeniLike)
        {
            _context.Likes.Add(yeniLike);
            await _context.SaveChangesAsync();
            return Ok(yeniLike);
        }
    }
}