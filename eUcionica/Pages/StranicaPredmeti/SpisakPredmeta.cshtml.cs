using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaPremeti
{
    public class SpisakPredmetaModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public SpisakPredmetaModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Predmet> Predmet { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Predmet != null)
            {
                Predmet = await _context.Predmet.ToListAsync();
            }
        }
    }
}