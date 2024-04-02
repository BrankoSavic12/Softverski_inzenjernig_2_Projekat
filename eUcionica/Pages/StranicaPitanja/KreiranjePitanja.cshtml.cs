using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaPitanja
{
    public class KreiranjePitanjaModel : PageModel
    {
        private readonly DB_Context_Class _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public KreiranjePitanjaModel(DB_Context_Class context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;

            
            Predmeti = new List<Predmet>();
            Oblasti = new List<Oblast>();
            NovoPitanje = new Pitanje();
            NoviPredmetID = 0; 
            NovaOblastID = 0; 
        }

        [BindProperty]
        public Pitanje NovoPitanje { get; set; }

        [BindProperty]
        public int NoviPredmetID { get; set; }

        [BindProperty]
        public int NovaOblastID { get; set; }

        [BindProperty]
        public IFormFile? NovaSlikaPitanja { get; set; } // Nullable IFormFile

        [BindProperty]
        public IFormFile? NovaSlikaOdgovora { get; set; } // Nullable IFormFile

        public List<Predmet> Predmeti { get; set; }

        public List<Oblast> Oblasti { get; set; }

        public void OnGet()
        {
            Predmeti = _context.Predmet.ToList();
        }

        public JsonResult OnGetOblastiByPredmet(int predmetId)
        {
            var oblasti = _context.Oblast.Where(o => o.PredmetID == predmetId).ToList();
            return new JsonResult(oblasti);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NovoPitanje.PredmetID = NoviPredmetID;
            NovoPitanje.OblastID = NovaOblastID;

            if (NovaSlikaPitanja != null)
            {
                NovoPitanje.PitanjeSlika = await SaveImage(NovaSlikaPitanja);
            }

            if (NovaSlikaOdgovora != null)
            {
                NovoPitanje.OdgovorSlika = await SaveImage(NovaSlikaOdgovora);
            }

            _context.Pitanje.Add(NovoPitanje);
            await _context.SaveChangesAsync();

            return RedirectToPage("./SpisakPitanja");
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = image.FileName;
            string imagePath = Path.Combine(wwwRootPath, "images", fileName);

            if (!Directory.Exists(Path.Combine(wwwRootPath, "images")))
            {
                Directory.CreateDirectory(Path.Combine(wwwRootPath, "images"));
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
