using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;

namespace WebProjeleri2022.Pages
{
    public class AdminModel : PageModel
    {
        public UserService userService;

        private readonly IHttpContextAccessor _httpContextAccessor;



        public AdminModel(UserService UserService, IHttpContextAccessor httpContextAccessor)
        {
            userService = UserService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public KullaniciModel kullanici { get; set; }

        [BindProperty]
        public string kullaniciId { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostForm()
        {
            List<KullaniciModel> Kullanici = userService.GetUsers();
            var kontrol = Kullanici.Where(a => a.kullaniciAdi == _httpContextAccessor.HttpContext.Session.GetString("KullaniciAdi")).FirstOrDefault();

            if (kontrol != null)
            {
                    userService.UpdateUser(userService.GetUserByNickname(kontrol.kullaniciAdi), kullanici);

                return RedirectToPage("/Admin", new { Status = "True" });
            }

            return RedirectToPage("/ChangePassword", new { Status = "True" });
        }

        public IActionResult OnPostDeleteUser()
        {
            List<KullaniciModel> Kullanici = userService.GetUsers();
            var kontrol = Kullanici.Where(a => a.kullaniciAdi == _httpContextAccessor.HttpContext.Session.GetString("KullaniciAdi")).FirstOrDefault();

            if (kontrol != null)
            {
                userService.DeleteUserByNickname(kontrol);

                return RedirectToPage("/Admin", new { Status = "True" });
            }

            return RedirectToPage("/ChangePassword", new { Status = "True" });
        }

    }
    }

