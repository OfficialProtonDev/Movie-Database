using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_Database.Data;
using Movie_Database.Models;
using DM.MovieApi.ApiResponse;

namespace Movie_Database.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly Movie_Database.Data.ApplicationDbContext _context;

        public DetailsModel(Movie_Database.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Movie Movie { get; set; } = default!;
        public List<Cast> CastMembers { get; set; } = default!;
        public MovieInfo MovieInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie is not null)
            {
                Movie = movie;
                CastMembers = await _context.Casts.Where(c => c.MovieId == id).ToListAsync();

                var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(Movie.Title);

                foreach (MovieInfo info in response.Results)
                {
                    if (info.ReleaseDate.Date == Movie.ReleaseDate?.Date)
                    {
                        MovieInfo = info;
                        break;
                    }
                }

                return Page();
            }

            return NotFound();
        }
    }
}
