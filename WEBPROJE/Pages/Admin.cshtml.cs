using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;

namespace WebProjeleri2022.Pages
{
    public class AdminModel : PageModel
    {
        public UserService userService;
        public AdminModel(UserService UserService)
        {
            userService = UserService;
        }

        [BindProperty]
        public KullaniciModel kullanici { get; set; }

        ISession session { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostForm()
        {
            List<KullaniciModel> Kullanici = userService.GetUsers();
            var kontrol = Kullanici.Where(a => a.kullaniciAdi == kullanici.kullaniciAdi).FirstOrDefault();

            if (kontrol != null)
            {
                    userService.UpdateUser(kullanici);

                return RedirectToPage("/Admin", new { Status = "True" });
            }

            return RedirectToPage("/ChangePassword", new { Status = "True" });
        }

    }
    }

