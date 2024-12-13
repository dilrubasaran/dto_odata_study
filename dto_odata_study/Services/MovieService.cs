using dto_odata_study.Context;
using dto_odata_study.Models;
using Microsoft.EntityFrameworkCore;

namespace dto_odata_study.Services
{
    public interface IMovieService : IGeneralService<MovieModel>
    {
        Task<IEnumerable<MovieModel>> GetMoviesWithCategoriesAsync();
    }

    public class MovieService : GeneralService<MovieModel>, IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieModel>> GetMoviesWithCategoriesAsync()
        {
            return await _context.Movies
                .Include(m => m.MovieCategories)
                .ThenInclude(mc => mc.Category)
                .ToListAsync();
        }
    }

}
