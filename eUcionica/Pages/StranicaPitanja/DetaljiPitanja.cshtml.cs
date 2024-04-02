using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaPitanja
{
    public class DetaljiPitanjaModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetaljiPitanjaModel(DB_Context_Class context)
        {
            _context = context;
            Pitanje = null;
        }

        public Pitanje? Pitanje { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pitanje = await _context.Pitanje
                .Include(p => p.Predmet)
                .Include(p => p.Oblast)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Pitanje == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
