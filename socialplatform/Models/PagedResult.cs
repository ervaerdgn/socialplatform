namespace socialplatform.Models
{
    public class PagedResult<T>
    {
        public List<T> Veriler { get; set; } = new();
        public int ToplamKayit { get; set; }
        public int SayfaNo { get; set; }
        public int SayfaBoyutu { get; set; }
        public int ToplamSayfa { get; set; }
    }
}