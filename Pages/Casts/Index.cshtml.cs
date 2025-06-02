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
    public class IndexModel : PageModel
    {
        private readonly Movie_Database.Data.ApplicationDbContext _context;

        public IndexModel(Movie_Database.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cast> Cast { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Cast = await _context.Casts
                .Include(c => c.Movie).ToListAsync();
        }
    }
}
