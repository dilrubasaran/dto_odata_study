using dto_odata_study.Models;
using dto_odata_study.Services;
using Microsoft.AspNetCore.Mvc;

namespace dto_odata_study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMoviesWithCategoriesAsync();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieModel movie)
        {
            await _movieService.AddAsync(movie);
            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movie);
        }
    }

}
