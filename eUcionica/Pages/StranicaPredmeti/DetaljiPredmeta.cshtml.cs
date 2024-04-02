using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaPredmet 
{
    public class DetaljiPredmetaModel : PageModel 
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public DetaljiPredmetaModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
            Predmet = new Predmet();
            Oblasti = new List<Oblast>();
        }

        public Predmet? Predmet { get; set; }
        public IList<Oblast>? Oblasti { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Predmet = await _context.Predmet.FirstOrDefaultAsync(m => m.ID == id);

            if (Predmet == null)
            {
                return NotFound();
            }

            Oblasti = await _context.Oblast.Where(o => o.PredmetID == Predmet.ID).ToListAsync();

            return Page();
        }
    }
}
