using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaPredmeti 
{
    public class MenjanjePredmetaModel : PageModel 
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public MenjanjePredmetaModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
            Predmet = new Predmet();
        }

        [BindProperty]
        public Predmet Predmet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Predmet = await _context.Predmet.FirstOrDefaultAsync(m => m.ID == id) ?? new Predmet();

            if (Predmet == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Predmet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PredmetExists(Predmet.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./SpisakPredmeta");
        }

        private bool PredmetExists(int id)
        {
            return _context.Predmet.Any(e => e.ID == id);
        }
    }
}
