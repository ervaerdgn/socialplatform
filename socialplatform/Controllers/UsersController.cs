using Microsoft.AspNetCore.Mvc;
using socialplatform.Models;

namespace socialplatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> kullanicilar = new List<User>();

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return kullanicilar;
        }

        [HttpPost]
        public ActionResult<User> Create(User yeniKullanici)
        {
            yeniKullanici.Id = kullanicilar.Count + 1;
            kullanicilar.Add(yeniKullanici);
            return Ok(yeniKullanici);
        }
    }
}