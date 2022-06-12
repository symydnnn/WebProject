using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;


namespace WebProjeleri2022.Pages
{
    public class FotograflarModel : PageModel
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<FotograflarModel> _logger;

        public FotograflarModel(PhotoService PhotoService, UserService UserService, IHttpContextAccessor httpContextAccessor)
        {
            photoService = PhotoService;
            userService = UserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public PhotoService photoService { get; set; }

        public UserService userService { get; set; }

        [BindProperty]
        public CommentModel comment { get; set; }


        public List<PhotoModel> Photos = null;

        public void OnGet()
        {
            Photos = photoService.GetPhotos();
            
        }

        [BindProperty]
        public string photoId { get; set; }

        [BindProperty]
        public string commentText { get; set; }

        [BindProperty]
        public int likes { get; set; }
        


        public IActionResult OnPostForm()
        {
            if (comment.comment != null && comment.KullaniciModel.id != null)
            {
                photoService.AddComment(comment.KullaniciModel, comment);
                return RedirectToPage("/Fotograflar", new { Status = "True" });
            }
            return null;

        }

        public IActionResult OnPostAddLikeOrComment()
        {
            string kullaniciAdi = _httpContextAccessor.HttpContext.Session.GetString("KullaniciAdi");
            
            if (comment.comment == null)
            {
                if (photoId != null)
                {
                    likes = (photoService.GetLikesOfPhoto(Convert.ToInt32(photoId)));
                    userService.addToLikes(kullaniciAdi, Convert.ToInt32(photoId));
                }
            }
            if(comment.comment != null)
            {
                comment.photoId = Convert.ToInt32(photoId);
                photoService.AddComment(userService.GetUserByNickname(kullaniciAdi), comment);
            }


            return RedirectToPage("/Fotograflar", new { Status = "True" }); ;

        }
        
        public IActionResult OnComments(int photoId)
        {
            List<String> comments = photoService.GetCommentsByPhotoId(photoId);
            Console.WriteLine(comments);
            return JsonResult(comments);
        }

        private IActionResult JsonResult(List<string> comments)
        {
            throw new NotImplementedException();
        }



    }
}
