
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
    Task<IQueryable> GetMoviesWithCategoriesQuery();
}

public class MovieService : GeneralService<MovieModel, MovieDto>, IMovieService
{

    private readonly IMapper _mapper;
    public MovieService(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IQueryable> GetMoviesWithCategoriesQuery()
    {
        // Sadece ProjectTo kullanımı
        return _dbSet.ProjectTo<MovieDto>(_mapper.ConfigurationProvider);
    }

    //sadece projecTo ile halledilebildiği için ınclude yi bıraktım 
    //ayrıca join yapısını da ef core navigasyon olduğu ve project to ile daha kolay halledebildiğim için tercih etmedim 

    //public async Task<IEnumerable<MovieDto>> GetMoviesWithCategoriesAsync()
    //{
    //    var movies = await _dbSet
    //        .Include(m => m.MovieCategories)
    //        .ThenInclude(mc => mc.Category)
    //        .Include(m => m.MoviePersonRoles)
    //        .ThenInclude(mpr => mpr.Person)
    //        .Include(m => m.MoviePersonRoles)
    //        .ThenInclude(mpr => mpr.Role)
    //        .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
    //        .ToListAsync();

    //    return movies;
    //}



}