namespace socialplatform.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Icerik { get; set; } = string.Empty;
        public DateTime Zaman { get; set; }= DateTime.UtcNow;
        public int GonderenId { get; set; } 
        public User? Gonderen { get; set; }

        public int AliciID { get; set; }
        public User? Alici { get; set; }



    }
}
