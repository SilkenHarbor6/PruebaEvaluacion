using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PruebaEvaluacion.Models;
using PruebaEvaluacion.Repository;

namespace PruebaEvaluacion.Pages.AreaList
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Area> Areas { get; set; }
        private readonly ICrudGeneral<Area> db;
        public IndexModel()
        {
            db = new RArea();

        }
        public async void OnGet()
        {
            Areas = await db.GetAll();
        }
        public IActionResult OnPostDelete(int id)
        {
            var result = db.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToPage("Index");
        }
    }
}
