using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Services;
using WebProjeleri2022.Models;


namespace WebProjeleri2022.Pages
{
    public class SýgnInModel : PageModel
    {
        public UserService userService;
        public SýgnInModel(UserService UserService)
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
            
            if (kontrol == null)
            {
                kontrol = Kullanici.Where(a => a.eMail == user.eMail).FirstOrDefault();
                
                if (kontrol == null)
                {
                    userService.AddUser(user);

                    return RedirectToPage("/Login", new { Status = "True" });
                }
                else
                {
                    var hataMesaji = "Email kullanýlmakta mevcut.";
                    Console.WriteLine(hataMesaji);
                    return RedirectToPage("/SignIn", new { Status = "True" }); ;
                }

            }
            else
            {
                var hataMesaji = "Kullanici adi mevcut.";
                Console.WriteLine(hataMesaji);
                return RedirectToPage("/SignIn", new { Status = "True" }); ;
            }


        }

    }
}
