using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.StranicaPitanja
{
    public class SpisakPitanjaModel : PageModel
    {
        private readonly DB_Context_Class _context;

        [BindProperty(SupportsGet = true)]
        public string? SearchText { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Sort { get; set; }

        public SpisakPitanjaModel(DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Pitanje>? Pitanje { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Pitanje> pitanjeQuery = _context.Pitanje
                .Include(p => p.Oblast)
                .Include(p => p.Predmet);

            if (!string.IsNullOrEmpty(SearchText))
            {
                pitanjeQuery = pitanjeQuery.Where(p =>
                    p.Predmet != null && p.Oblast != null && p.NivoTezine != null &&
                    p.Predmet.NazivPredmeta != null && p.Oblast.Name != null &&
                    (EF.Functions.Like(p.Predmet.NazivPredmeta, $"%{SearchText}%") ||
                    EF.Functions.Like(p.Oblast.Name, $"%{SearchText}%") ||
                    EF.Functions.Like(p.NivoTezine, $"%{SearchText}%")));
            }
            if (Sort == "oblast")
            {
                pitanjeQuery = pitanjeQuery.OrderBy(p => p.Oblast != null ? p.Oblast.Name : null);
            }
            else if (Sort == "predmet")
            {
                pitanjeQuery = pitanjeQuery.OrderBy(p => p.Predmet != null ? p.Predmet.NazivPredmeta : null);
            }
            else if (Sort == "tezina")
            {
                pitanjeQuery = pitanjeQuery.OrderBy(p => p.NivoTezine);
            }

            Pitanje = await pitanjeQuery.ToListAsync();
        }

        public async Task OnPostAsync()
        {
            IQueryable<Pitanje> pitanjeQuery = _context.Pitanje
                .Include(p => p.Oblast)
                .Include(p => p.Predmet);

            if (!string.IsNullOrEmpty(SearchText))
            {
                pitanjeQuery = pitanjeQuery.Where(p =>
                    p.Predmet != null && p.Oblast != null && p.NivoTezine != null &&
                    p.Predmet.NazivPredmeta != null && p.Oblast.Name != null &&
                    (EF.Functions.Like(p.Predmet.NazivPredmeta, $"%{SearchText}%") ||
                    EF.Functions.Like(p.Oblast.Name, $"%{SearchText}%") ||
                    EF.Functions.Like(p.NivoTezine, $"%{SearchText}%")));
            }

            Pitanje = await pitanjeQuery.ToListAsync();
        }
    }
}
