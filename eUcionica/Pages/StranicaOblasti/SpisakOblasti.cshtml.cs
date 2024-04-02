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
    public class SpisakOblastiModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public SpisakOblastiModel(DB_Context_Class context)
        {
            _context = context;
            Oblast = new List<Oblast>(); 
            SearchText = ""; 
        }

        [BindProperty]
        public string SearchText { get; set; }

        public IList<Oblast> Oblast { get; set; }

        public async Task OnGetAsync()
        {
            await LoadOblastAsync();
        }

        public async Task OnPostAsync()
        {
            await LoadOblastAsync();
        }

        public async Task OnGetSortByPredmetAsync()
        {
            await LoadOblastAsync(true);
        }

        private async Task LoadOblastAsync(bool sortByPredmet = false)
        {
            IQueryable<Oblast> oblastQuery = _context.Oblast.Include(o => o.Predmet);

            if (!string.IsNullOrEmpty(SearchText))
            {
                oblastQuery = oblastQuery.Where(s => s.Predmet != null && s.Name != null && s.Predmet.NazivPredmeta != null &&
                                                    (EF.Functions.Like(s.Predmet.NazivPredmeta, $"%{SearchText}%") ||
                                                    EF.Functions.Like(s.Name, $"%{SearchText}%")));
            }

            if (sortByPredmet)
            {
                oblastQuery = oblastQuery.OrderBy(s => s.PredmetID);
            }

            Oblast = await oblastQuery.ToListAsync() ?? new List<Oblast>(); 
        }
    }
}
