using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;


namespace WebProjeleri2022.Pages
{
    public class FotograflarModel : PageModel
    {

        private readonly ILogger<FotograflarModel> _logger;

        public FotograflarModel(ILogger<FotograflarModel> logger, PhotoService PhotoService)
        {
            _logger = logger;

            photoService = PhotoService;   
        }

        [BindProperty]
        public PhotoService photoService { get; set; }

        [BindProperty]
        public CommentModel comment { get; set; }


        public List<PhotoModel> Photos = null;

        public void OnGet()
        {
            Photos = photoService.GetPhotos();
            
        }

        [FromQuery]
        public int photoId { get; set; }


        [BindProperty]
        public string url { get; set; }


        public IActionResult OnPostForm()
        {
            if (comment.comment != null && comment.KullaniciModel.id != null)
            {
                photoService.AddComment(comment.KullaniciModel, comment);
                return RedirectToPage("/Fotograflar", new { Status = "True" });
            }
            return null;

        }


        public void OnPost(PhotoService photoService)
        {
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
