using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_Database.Data;
using Movie_Database.Models;

namespace Movie_Database.Pages.Casts
{
    public class DetailsModel : PageModel
    {
        private readonly Movie_Database.Data.ApplicationDbContext _context;

        public DetailsModel(Movie_Database.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Cast Cast { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cast = await _context.Casts.FirstOrDefaultAsync(m => m.Id == id);

            if (cast is not null)
            {
                Cast = cast;

                return Page();
            }

            return NotFound();
        }
    }
}
