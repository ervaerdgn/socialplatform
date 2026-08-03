namespace socialplatform.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string metin { get; set; }=string.Empty;
        public int UserID { get; set; }
        public DateTime zaman { get; set; }=DateTime.Now;
       
        public User? User { get; set; }
    }
}
