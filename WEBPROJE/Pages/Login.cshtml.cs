using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;
using Microsoft.AspNetCore.Http;

namespace WebProjeleri2022.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService userService;
        public LoginModel(UserService UserService, IHttpContextAccessor httpContextAccessor)
        {
            userService = UserService;
            _httpContextAccessor = httpContextAccessor;
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
                    _httpContextAccessor.HttpContext.Session.SetString("KullaniciAdi", user.kullaniciAdi);
                    return RedirectToPage("/Admin", new { Status = "True" });
                }
                else
                {
                    Console.WriteLine("Þifre yanlýþ.");
                    return RedirectToPage("/Login", new { Status = "True" });
                }
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
