using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialplatform.Data;
using socialplatform.Models;

namespace socialplatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User yeniKullanici)
        {
            _context.Users.Add(yeniKullanici);
            await _context.SaveChangesAsync();
            return Ok(yeniKullanici);
        }
    }
}