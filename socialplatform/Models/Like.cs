namespace socialplatform.Models
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime Zaman { get; set; } = DateTime.Now;
        public int UserID { get; set; }
        public User? User { get; set; }

        public int PostID { get; set; }
        public Post? Post { get; set; }
    }
}
