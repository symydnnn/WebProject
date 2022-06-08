using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebProjeleri2022.Pages
{
    public class CikisModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CikisModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            LoginModel.isInside = false;
            if (_httpContextAccessor.HttpContext.Session.GetString("KullaniciAdi") != null)
                _httpContextAccessor.HttpContext.Session.SetString("KullaniciAdi", "");
            RedirectToPage("/Login", new { Status = "True" });
            
        }
    }
}
