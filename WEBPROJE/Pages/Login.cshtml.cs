using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;

namespace WebProjeleri2022.Pages
{
    public class LoginModel : PageModel
    {

        public UserService userService;
        public LoginModel(UserService UserService)
        {
            userService = UserService;
        }

        [BindProperty]
        public KullaniciModel user { get; set; }


        public void OnGet()
        {
        }


        public IActionResult OnPostForm()
        {
            List<KullaniciModel> Kullanici = userService.GetUsers();
            var kontrol = Kullanici.Where(a => a.kullaniciAdi == user.kullaniciAdi).FirstOrDefault();

            if (kontrol != null)
            {
                if (kontrol.sifre == user.sifre)
                {
                    //getLikesPhotos
                    return RedirectToPage("/Admin", new { Status = "True" });
                }
                else { Console.WriteLine("Þifre yanlýþ."); 
                    return RedirectToPage("/Login", new { Status = "True" }); }
            }
            else
            {
                var hataMesaji = "Þifre veya kullanýcý adý yanlýþ";
                Console.WriteLine(hataMesaji);
                return RedirectToPage("/Login", new { Status = "True" });
            }


        }




    }
}
