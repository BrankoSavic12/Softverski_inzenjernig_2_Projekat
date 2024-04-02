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
    public class MenjanjePredmetaModel : PageModel
    {
        private readonly DB_Context_Class _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        
        public MenjanjePredmetaModel(DB_Context_Class context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            Predmeti = new List<Predmet>();
            Pitanje = new Pitanje();
            Oblasti = new List<Oblast>();
            NovaSlikaPitanja = new FormFile(Stream.Null, 0, 0, "", "");
            NovaSlikaOdgovora = new FormFile(Stream.Null, 0, 0, "", "");


        }

        [BindProperty]
        public Pitanje Pitanje { get; set; }

        [BindProperty]
        public int NoviPredmetID { get; set; }

        [BindProperty]
        public int NovaOblastID { get; set; }

        [BindProperty]
        public IFormFile NovaSlikaPitanja { get; set; }

        [BindProperty]
        public IFormFile NovaSlikaOdgovora { get; set; }

        public List<Predmet> Predmeti { get; set; }

        public List<Oblast> Oblasti { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pitanje = await _context.Pitanje.FindAsync(id);

            if (Pitanje == null)
            {
                return NotFound();
            }

            
            Predmeti = await _context.Predmet.ToListAsync();
            Oblasti = await _context.Oblast.ToListAsync();

          
            NoviPredmetID = Pitanje.PredmetID;
            NovaOblastID = Pitanje.OblastID;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
            Predmeti = await _context.Predmet.ToListAsync();
            Oblasti = await _context.Oblast.ToListAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NoviPredmetID != Pitanje.PredmetID)
            {
                Pitanje.PredmetID = NoviPredmetID;
            }

            if (NovaOblastID != Pitanje.OblastID)
            {
                Pitanje.OblastID = NovaOblastID;
            }

            if (NovaSlikaPitanja != null)
            {
                Pitanje.PitanjeSlika = await SaveImage(NovaSlikaPitanja, "Pitanje");
            }

            if (NovaSlikaOdgovora != null)
            {
                Pitanje.OdgovorSlika = await SaveImage(NovaSlikaOdgovora, "Odgovor");
            }

            _context.Attach(Pitanje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PitanjeExists(Pitanje.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./SpisakPitanja");
        }

        private async Task<string> SaveImage(IFormFile image, string type)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = image.FileName ?? throw new ArgumentException("File name cannot be null");

            string imagePath = Path.Combine(wwwRootPath, "images", fileName);

            
            if (!System.IO.File.Exists(imagePath))
            {
              
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            return fileName;
        }

        private bool PitanjeExists(int id)
        {
            return _context.Pitanje.Any(e => e.ID == id);
        }
    }
}
