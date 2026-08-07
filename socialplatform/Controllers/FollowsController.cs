using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialplatform.Data;
using socialplatform.Models;

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
        public async Task<ActionResult<Follow>> Create(Follow yeniFollow)
        {
            _context.Follows.Add(yeniFollow);
            await _context.SaveChangesAsync();
            return Ok(yeniFollow);
        }
    }
}