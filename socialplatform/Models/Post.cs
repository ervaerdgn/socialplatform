namespace socialplatform.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Metin { get; set; }=string.Empty;
        public int UserID { get; set; }
        public DateTime Zaman { get; set; }=DateTime.Now;
       
        public User? User { get; set; }
        public string? Photo {  get; set; }
    }
}
