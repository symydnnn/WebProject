using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;

namespace WebProjeleri2022.Pages
{
    public class AddPhotoModel : PageModel
    {

        public PhotoService photoService;
        public AddPhotoModel(PhotoService PhotoService)
        {
            photoService = PhotoService;
        }

        [BindProperty]
        public PhotoModel photo { get; set; }


        public void OnGet()
        {
        }


        public IActionResult OnPostForm()
        {
            if(photo.url != null && photo.photoName != null)
            {
                photoService.AddPhoto(photo);
                return RedirectToPage("/AddPhoto", new { Status = "True" });

            }

            return null;

        }
    }
}
