namespace socialplatform.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Metin { get; set; } = string.Empty;
        public DateTime Zaman { get; set; } = DateTime.UtcNow;

        public int UserID { get; set; }
        public User? User { get; set; }

        public int PostID { get; set; }
        public Post? Post { get; set; }
    }
}