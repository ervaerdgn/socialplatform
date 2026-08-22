namespace socialplatform.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public DateTime zaman { get; set; }= DateTime.UtcNow;
        public int FollowerID { get; set; } //takip eden kişi olarak düşün
        public User? Follower { get; set; }
        public int FollowingID { get; set; } //takip edilen kişi olarak 
        public User? Following { get; set; }


    }
}
    