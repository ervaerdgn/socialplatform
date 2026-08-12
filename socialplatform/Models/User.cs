namespace socialplatform.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Time {  get; set; }= DateTime.Now;
      
        public string? ProfilePhoto { get; set; }
    }
}
