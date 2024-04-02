using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eUcionica.Pages.StranicaTestovi
{
    public class FormiranTestZnanjaModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public FormiranTestZnanjaModel(DB_Context_Class context)
        {
            _context = context;
            SelectedQuestions = new List<Pitanje>();
        }

        public List<Pitanje>? SelectedQuestions { get; set; }

        public void OnGet()
        {
            var selectedQuestionsJson = TempData["SelectedQuestions"] as string;
            if (selectedQuestionsJson != null)
            {
                SelectedQuestions = JsonConvert.DeserializeObject<List<Pitanje>>(selectedQuestionsJson);
            }
            else
            {
                SelectedQuestions = new List<Pitanje>();
            }
        }
    }
}

