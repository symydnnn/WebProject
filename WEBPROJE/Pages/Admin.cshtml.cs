using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;

namespace WebProjeleri2022.Pages
{
    public class AdminModel : PageModel
    {
        public JsonProjectService jsonProjectService;
        public AdminModel(JsonProjectService JsonProjectService)
        {
            jsonProjectService = JsonProjectService;
        }

        [BindProperty]
        public ProjectModel proje { get; set; }



        public void OnGet()
        {
        }

        public IActionResult OnPostForm()
        {       
            jsonProjectService.AddProject(proje);

            return RedirectToPage("/Index", new {Status = "True"});

        }
    }
}
