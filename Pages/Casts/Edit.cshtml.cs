using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Database.Data;
using Movie_Database.Models;

namespace Movie_Database.Pages.Casts
{
    public class EditModel : PageModel
    {
        private readonly Movie_Database.Data.ApplicationDbContext _context;

        public EditModel(Movie_Database.Data.ApplicationDbContext context)
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

            var cast =  await _context.Casts.FirstOrDefaultAsync(m => m.Id == id);
            if (cast == null)
            {
                return NotFound();
            }
            Cast = cast;
           ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CastExists(Cast.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CastExists(Guid? id)
        {
            return _context.Casts.Any(e => e.Id == id);
        }
    }
}
