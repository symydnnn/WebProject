using WebProjeleri2022.Models;


namespace WebProjeleri2022.Services
{
    public class AuthServices
    {
        AuthModel authModel;


        public void addKullaniciAdi(String kullaniciAdi)
        {
            AuthModel authModel = new AuthModel();
            authModel.kullaniciAdi = kullaniciAdi;
        }




    }
}
