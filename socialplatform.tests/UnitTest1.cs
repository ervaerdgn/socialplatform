
using Microsoft.EntityFrameworkCore;
using socialplatform.Models; 
using socialplatform.Data;  

namespace socialplatform.Tests
{
    public class DatabaseTests
    {
        [Fact]
        public async Task VeritabaninaKullaniciEklendigindeKayitBasariliOlmali()
        {
          
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestVeritabani")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var yeniKullanici = new User
                {
                    Name = "Test Erva",
                    Email = "erva@test.com",
                    Password = "123",
                    Time = DateTime.Now
                };

                context.Users.Add(yeniKullanici);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var kaydedilenKullanici = await context.Users.FirstOrDefaultAsync(u => u.Email == "erva@test.com");

                Assert.NotNull(kaydedilenKullanici); 
                Assert.Equal("Test Erva", kaydedilenKullanici.Name); 
            }
        }
    }
}