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
    public class DeleteModel : PageModel
    {
        private readonly Movie_Database.Data.ApplicationDbContext _context;

        public DeleteModel(Movie_Database.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cast Cast { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cast = await _context.Casts.Include(c => c.Movie).FirstOrDefaultAsync(m => m.Id == id);

            if (cast is not null)
            {
                Cast = cast;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cast = await _context.Casts.FindAsync(id);
            if (cast != null)
            {
                Cast = cast;
                _context.Casts.Remove(Cast);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
