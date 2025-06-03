using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movie_Database.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IReadOnlyList<MovieInfo> TopRatedMovies { get; set; } = new List<MovieInfo>();

        public async Task<IActionResult> OnGetAsync()
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.GetTopRatedAsync();

            if (response is not null)
            {
                TopRatedMovies = response.Results;

                return Page();
            }

            return NotFound();
        }
    }
}
