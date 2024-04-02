using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaOblasti
{
    public class MenjanjeOblastiModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public MenjanjeOblastiModel(DB_Context_Class context)
        {
            _context = context;
            Predmeti = new List<Predmet>(); 
            Oblast = new Oblast(); 
        }

        [BindProperty]
        public Oblast Oblast { get; set; }

        [BindProperty]
        public int NoviPredmetID { get; set; }

        public List<Predmet> Predmeti { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            Oblast = await _context.Oblast.FindAsync(id) ?? new Oblast();
            Predmeti = await _context.Predmet.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NoviPredmetID != Oblast.PredmetID)
            {
                Oblast.PredmetID = NoviPredmetID;
            }

            _context.Attach(Oblast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OblastExists(Oblast.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./SpisakOblasti");
        }

        private bool OblastExists(int id)
        {
            return _context.Oblast.Any(e => e.ID == id);
        }
    }
}
