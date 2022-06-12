using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using WebProjeleri2022.Models;


namespace WebProjeleri2022.Services
{
    public class UserService
    {

        public UserService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;

        }

        public IWebHostEnvironment WebHostEnvironment;
        

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "user.json"); }

        }

        public void JsonWriter(List<KullaniciModel> kullanicilar, bool status)
        {
            FileStream json;

            if (status)
                json = File.Create(JsonFileName);
            else
                json = File.OpenWrite(JsonFileName);

            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<KullaniciModel>>(jsonwriter, kullanicilar);
            json.Close();
        }


        public List<KullaniciModel> GetUsers()
        {
            using var json = File.OpenText(JsonFileName);

            return JsonSerializer.Deserialize<KullaniciModel[]>(json.ReadToEnd()).ToList();

        }

        public List<CommentModel> GetComments()
        {
            using var json = File.OpenText(Path.Combine(WebHostEnvironment.WebRootPath, "data", "user.json"));

            return JsonSerializer.Deserialize<CommentModel[]>(json.ReadToEnd()).ToList();

        }

        public KullaniciModel GetUserByNickname(string nickName)
        {
            List<KullaniciModel> Kullanicilar = GetUsers();

            var kullanici = Kullanicilar.Where(a => a.kullaniciAdi == nickName).FirstOrDefault();
            return kullanici;
        }


        public void AddUser(KullaniciModel newUser)
        {
            List<KullaniciModel> Kullanicilar = GetUsers();
            if (newUser.kullaniciAdi != null && newUser.sifre != null)
            {
                newUser.id = Kullanicilar.Max(x => x.id) + 1;

                Kullanicilar.Add(newUser);
                using var json = File.OpenWrite(JsonFileName);
                Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
                JsonSerializer.Serialize<List<KullaniciModel>>(jsonwriter, Kullanicilar);
            }

        }


        public bool DeleteUserByNickname(KullaniciModel kullanici)
        {
            List<KullaniciModel> kullanicilar = GetUsers();
            KullaniciModel query = kullanicilar.FirstOrDefault(x => x.kullaniciAdi == kullanici.kullaniciAdi);
            if (query != null)
            {
                kullanicilar.Remove(query);
                JsonWriter(kullanicilar, true);
                return true;
            }
            else
                return false;
        }


        public bool DeleteComment(KullaniciModel kullanici, CommentModel comment)
        {
            List<CommentModel> comments = GetComments();

            CommentModel kontrol = comments.FirstOrDefault(x => x.id == comment.id);
            if (kontrol != null)
            {
                comments.Remove(kontrol);
                JsonWriter(comments, true);
                return true;
            }
            else
                return false;
        }

        private void JsonWriter(List<CommentModel> comments, bool v)
        {
            throw new NotImplementedException();
        }



        public void UpdateUserPassword(KullaniciModel kullanici)
        {
            List<KullaniciModel> kullanicilar = GetUsers();

            KullaniciModel query = kullanicilar.FirstOrDefault(x => x.kullaniciAdi == kullanici.kullaniciAdi);
            if (query != null)
            {
                kullanicilar[kullanicilar.FindIndex(x => x.kullaniciAdi == kullanici.kullaniciAdi)].sifre = kullanici.sifre;
                JsonWriter(kullanicilar, true);
            }

        }


        public void UpdateUser(KullaniciModel kullanici, KullaniciModel yeniKullaci)
        {
            List<KullaniciModel> kullanicilar = GetUsers();

            KullaniciModel query = kullanicilar.FirstOrDefault(x => x.kullaniciAdi == kullanici.kullaniciAdi);
            if (query != null)
            {
                if (yeniKullaci.adi != null)
                    kullanicilar[kullanicilar.FindIndex(x => x.kullaniciAdi == kullanici.kullaniciAdi)].adi = yeniKullaci.adi;
                if (yeniKullaci.dogumTarihi != null)
                    kullanicilar[kullanicilar.FindIndex(x => x.kullaniciAdi == kullanici.kullaniciAdi)].dogumTarihi = yeniKullaci.dogumTarihi;
                if (yeniKullaci.soyadi != null)
                    kullanicilar[kullanicilar.FindIndex(x => x.kullaniciAdi == kullanici.kullaniciAdi)].soyadi = yeniKullaci.soyadi;
                if (yeniKullaci.eMail != null)
                    kullanicilar[kullanicilar.FindIndex(x => x.kullaniciAdi == kullanici.kullaniciAdi)].eMail = yeniKullaci.eMail;
                JsonWriter(kullanicilar, true);
            }

        }

        //Bu fonksiyon like atılan fotoğrafları sayfada göstermek için

        public List<int> GetLikedContent(string nickName)
        {
            var kullanici = GetUserByNickname(nickName);
            List<int> likesId;

            if (kullanici != null)
            {
                likesId = kullanici.likesId;
            }
            else
            {
                likesId = null;
            }
            return likesId;
        }

        // kalp atmak için
        public void addToLikes(string kullaniciAdi, int photoId)
        {

            KullaniciModel kullanici = GetUserByNickname(kullaniciAdi);

            List<KullaniciModel> kullanicilar = GetUsers();
            List<int> likes = GetLikedContent(kullanici.kullaniciAdi);
            if (likes != null)
            {
                likes.Add(photoId);
                kullanici.likesId = likes;
                kullanicilar[kullanicilar.FindIndex(x => x.kullaniciAdi == kullanici.kullaniciAdi)].likesId = kullanici.likesId;
                JsonWriter(kullanicilar, true);
            }

        }


    }

}
