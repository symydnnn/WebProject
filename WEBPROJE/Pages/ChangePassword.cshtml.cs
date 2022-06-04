using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;


namespace WebProjeleri2022.Pages
{
    public class ChangePasswordModel : PageModel
    {
        public UserService userService;
        public ChangePasswordModel(UserService UserService)
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
                if (user.sifre != null)

                    userService.UpdateUser(user);

                    return RedirectToPage("/Admin", new { Status = "True" });
            }

            return RedirectToPage("/ChangePassword", new { Status = "True" });
        }

            

    }

}
