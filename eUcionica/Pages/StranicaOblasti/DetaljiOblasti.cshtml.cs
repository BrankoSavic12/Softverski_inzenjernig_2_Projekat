using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eUcionica.Pages.StranicaOblasti
{
    public class DetaljiOblastiModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetaljiOblastiModel(DB_Context_Class context)
        {
            _context = context;
        }

        public Oblast? Oblast { get; set; }
        public List<Pitanje>? Pitanja { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Oblast = await _context.Oblast
                .Include(o => o.Predmet)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Oblast == null)
            {
                return NotFound();
            }

            Pitanja = await _context.Pitanje
                .Where(p => p.OblastID == id)
                .ToListAsync();

            return Page();
        }
    }
}
