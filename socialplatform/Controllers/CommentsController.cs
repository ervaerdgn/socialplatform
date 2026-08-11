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
        public async Task<ActionResult<Comment>> Create(Comment yeniComment)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            yeniComment.UserID = userId;

            _context.Comments.Add(yeniComment);
            await _context.SaveChangesAsync();
            return Ok(yeniComment);
        }
    }
}