namespace socialplatform.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Mesaj { get; set; } = string.Empty;
        public DateTime Zaman { get; set; } = DateTime.Now;
        public bool Okundu { get; set; } = false;

        public int UserID { get; set; }
        public User? User { get; set; }
    }
}