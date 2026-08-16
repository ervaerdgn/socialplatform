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
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessagesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Message>> Gonder(Message yenimesaj)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            yenimesaj.GonderenId = userId;

            _context.Messages.Add(yenimesaj);
            await _context.SaveChangesAsync();
            return Ok(yenimesaj);
        }

        [HttpGet("konusma/{digerKullaniciId}")]
        [Authorize]
        public async Task<ActionResult<List<Message>>> Konusma(int digerKullaniciId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var mesajlar = await _context.Messages
                .Where(m =>
                    (m.GonderenId == userId && m.AliciID == digerKullaniciId) ||
                    (m.GonderenId == digerKullaniciId && m.AliciID == userId))
                .OrderBy(m => m.Zaman)
                .ToListAsync();

            return mesajlar;
        }
    }
}