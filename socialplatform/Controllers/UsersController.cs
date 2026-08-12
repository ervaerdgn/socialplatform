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
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UsersController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User yenikullanici)
        {
            yenikullanici.Password = BCrypt.Net.BCrypt.HashPassword(yenikullanici.Password);

            _context.Users.Add(yenikullanici);
            await _context.SaveChangesAsync();
            return Ok(yenikullanici);
        }
        [HttpPost("upload-profile-photo")]
        [Authorize]
        public async Task<ActionResult> UploadProfilePhoto(IFormFile file)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var kullanici = await _context.Users.FindAsync(userId);

            if (kullanici == null)
                return NotFound();

            var uploadsKlasoru = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsKlasoru))
                Directory.CreateDirectory(uploadsKlasoru);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var tamYol = Path.Combine(uploadsKlasoru, dosyaAdi);

            using (var stream = new FileStream(tamYol, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            kullanici.ProfilePhoto = "/uploads/" + dosyaAdi;
            await _context.SaveChangesAsync();

            return Ok(new { profilResmi = kullanici.ProfilePhoto });
        }
    }
}