using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaOblasti
{
    public class DodavanjeOblastiModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public DodavanjeOblastiModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
            NewOblast = new Oblast();
        }

        [BindProperty]
        public Oblast NewOblast { get; set; }

        public List<Predmet> Predmeti { get; set; } = new List<Predmet>();

        public void OnGet()
        {
            Predmeti = _context.Predmet.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Predmeti = _context.Predmet.ToList();
                return Page();
            }

            _context.Oblast.Add(NewOblast);
            await _context.SaveChangesAsync();

            return RedirectToPage("./SpisakOblasti");
        }
    }
}
