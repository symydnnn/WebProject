namespace WebProjeleri2022.Models
{
    public class KullaniciModel
    {
        public int id { get; set; }

        public string kullaniciAdi { get; set; }

        public string sifre { get; set; }

        public string adi { get; set; }

        public string soyadi { get; set; }

        public string eMail { get; set; }

        public string dogumTarihi { get; set; }   ///datetime

        public List<int> likesId { get; set; }

        public KullaniciModel()
        {
            likesId = new List<int>();
        }

    }
}
