using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using System.Threading.Tasks;

namespace eUcionica.Pages.StranicaOblasti
{
    public class BrisanjeOblastiModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public BrisanjeOblastiModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Oblast? Oblast { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Oblast = await _context.Oblast.Include(o => o.Predmet).FirstOrDefaultAsync(m => m.ID == id);

            if (Oblast == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oblast = await _context.Oblast.FindAsync(id);

            if (oblast != null)
            {
                _context.Oblast.Remove(oblast);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./SpisakOblasti");
        }
    }
}
