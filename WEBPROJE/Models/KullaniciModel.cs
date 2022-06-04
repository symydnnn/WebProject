namespace WebProjeleri2022.Models
{
    public class KullaniciModel
    {
        public int id { get; set; }

        ///[Required(ErrorMessage = "Kullanıcı adı girilmeli")]
        public string kullaniciAdi { get; set; }

        //[Required(ErrorMessage = "Şifre girilmeli")]
        public string sifre { get; set; }

        public string adi { get; set; }

        public string soyadi { get; set; }

        public string eMail { get; set; }

        public string dogumTarihi { get; set; }   ///datetime

        public List<int> likesId { get; set; }



    }
}
