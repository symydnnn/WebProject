using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProjeleri2022.Models;
using WebProjeleri2022.Services;

namespace WebProjeleri2022.Pages
{
    public class IndexModel : PageModel
    {

        public PhotoService photoService;

        public IndexModel(PhotoService PhotoService)
        {
            photoService = PhotoService;
        }

        [BindProperty]
        public PhotoModel Model { get; set; }

        [BindProperty]
        public int photoId { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPostForm()
        {
            return null;

        }


        [BindProperty]
        public string PokemonName { get; set; }

        [BindProperty]
        public string photoPath { get; set; }

        public void OnPostProcessRequestAsync()
        {
            Console.WriteLine(PokemonName);

            photoPath = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + PokemonName + ".png";

        }


        






    }
}