using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using WebProjeleri2022.Models;


namespace WebProjeleri2022.Services
{
    public class PhotoService
    {
        public PhotoService(IWebHostEnvironment webHostEnvironment, UserService userService)
        {
            WebHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public IWebHostEnvironment WebHostEnvironment;
        private readonly UserService _userService;

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "photoinf.json"); }

        }


        public void JsonWriter(List<PhotoModel> fotograflar, bool status)
        {
            FileStream json;

            if (status)
                json = File.Create(JsonFileName);
            else
                json = File.OpenWrite(JsonFileName);

            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<PhotoModel>>(jsonwriter, fotograflar);
            json.Close();
        }

        public List<PhotoModel> GetPhotos()
        {
            using var json = File.OpenText(JsonFileName);

            return JsonSerializer.Deserialize<PhotoModel[]>(json.ReadToEnd()).ToList();

        }
        
        public PhotoModel GetPhotoModel(string url)
        {
            string url2 = string.Concat(url);
            var json = new WebClient().DownloadString(url2);
            return JsonSerializer.Deserialize<PhotoModel>(json);

        }

        public void AddPhoto(PhotoModel photo)
        {
            List<PhotoModel> fotolar = GetPhotos();
            PhotoModel query = fotolar.FirstOrDefault(x => x.url == photo.url);
            if(query == null)
            {
                photo.id = fotolar.Max(x => x.id) + 1;

                fotolar.Add(photo);
                using var json = File.OpenWrite(JsonFileName);
                Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
                JsonSerializer.Serialize<List<PhotoModel>>(jsonwriter, fotolar);
            }
        }


        public PhotoModel GetPhotoById(int photoId)
        {
            List<PhotoModel> photos = GetPhotos();

            var photo = photos.Where(a => a.id == photoId).FirstOrDefault();
            return photo;

        }

        //Fotoğrafa yapılan likeları getir

        public int GetLikesOfPhoto(int photoId)
        {
            var photo = GetPhotoById(photoId);

            var users = _userService.GetUsers();
            for(var i=0; i < _userService.GetUsers().Count; i++)
            {
                var user = _userService.GetUsers()[i];
                if(user != null)
                {
                    var likes = user.likesId;
                    if(likes != null)
                    {
                        if (likes.Contains(photoId))
                        {
                            photo.likes++;
                        }
                    }
                    
                }
                
            }
            return photo.likes;
        }

        
        public List<CommentModel> GetCommentsInfoByPhotoId(int photoId)
        {
            var comments = GetComments();
            List<CommentModel> commentOfPhoto = new List<CommentModel>();

            for (var i = 0; i < comments.Count; i++)
            {
                var commentsInfo = comments[i];
                    if (commentsInfo.photoId == photoId)
                    {
                        commentOfPhoto.Add(commentsInfo);
                }
            }

            return commentOfPhoto;
        }

        public List<string> GetCommentsByPhotoId(int photoId)
        {
            
            List<CommentModel> commentOfPhoto = GetCommentsInfoByPhotoId(photoId);
            List<string> comments = new List<string>();

            for (var i = 0; i < commentOfPhoto.Count; i++)
            {
                var commentsInfo = commentOfPhoto[i];
                if (commentsInfo.photoId == photoId)
                {
                    comments.Add(commentsInfo.comment);

                }
                
            }
            return comments;
        }


        public List<CommentModel> GetComments()
        {
            using var json = File.OpenText(Path.Combine(WebHostEnvironment.WebRootPath, "data", "comments.json"));

            return JsonSerializer.Deserialize<CommentModel[]>(json.ReadToEnd()).ToList();

        }

        public void AddComment(KullaniciModel kullanici, CommentModel comment)
        {
            List<KullaniciModel> kullanicilar = _userService.GetUsers();
            List<CommentModel> yorumlar = GetComments();

            KullaniciModel user = kullanicilar.FirstOrDefault(x => x.kullaniciAdi == kullanici.kullaniciAdi);

                if (user != null)
                {
                    comment.id = yorumlar.Max(x => x.id) + 1;
                    comment.KullaniciModel = user;

                    yorumlar.Add(comment);
                    using var json = File.OpenWrite(Path.Combine(WebHostEnvironment.WebRootPath, "data", "comments.json"));
                    Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
                    JsonSerializer.Serialize<List<CommentModel>>(jsonwriter, yorumlar);
                }
            }

    }
}
