
using AutoMapper;
using AutoMapper.QueryableExtensions;
using dto_odata_study.Context;
using dto_odata_study.DTOs;
using dto_odata_study.DTOs.dto_odata_study.DTOs;
using dto_odata_study.Models;
using dto_odata_study.Services;
using Microsoft.EntityFrameworkCore;



public interface IMovieService : IGeneralService<MovieModel, MovieDto>
{
    Task<IEnumerable<MovieDto>> GetMoviesWithCategoriesAsync();
}

public class MovieService : GeneralService<MovieModel, MovieDto>, IMovieService
{

    private readonly IMapper _mapper;
    public MovieService(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    

    public async Task<IEnumerable<MovieDto>> GetMoviesWithCategoriesAsync()
    {
        // ProjectTo kullanarak dönüşüm ve veritabanı sorgusunu optimize etme 
        var movies = await _dbSet
            .Include(m => m.MovieCategories)
            .ThenInclude(mc => mc.Category)
            .Include(m => m.MoviePersonRoles)
            .ThenInclude(mpr => mpr.Person)
            .Include(m => m.MoviePersonRoles)
            .ThenInclude(mpr => mpr.Role)
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider) // AutoMapper dönüşümü
            .ToListAsync();

        return movies;
    }
}