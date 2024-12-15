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
    public MovieService(AppDbContext context) : base(context)
    {
    }

    protected override MovieDto MapToDto(MovieModel movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            Description = movie.Description,
            PosterUrl = movie.PosterUrl,
            Categories = movie.MovieCategories?.Select(mc => mc.Category.Name).ToList(),
            Roles = movie.MoviePersonRoles?.Select(mpr => new MoviePersonRoleDto
            {
                PersonName = mpr.Person?.Name ?? "",
                RoleName = mpr.Role?.Name ?? ""
            }).ToList()
        };
    }

    protected override MovieModel MapToEntity(MovieDto dto)
    {
        return new MovieModel
        {
            Id = dto.Id,
            Title = dto.Title,
            ReleaseYear = dto.ReleaseYear,
            Description = dto.Description,
            PosterUrl = dto.PosterUrl,
            MovieCategories = dto.Categories?.Select(categoryName => new MovieCategoryModel
            {
                Category = new CategoryModel { Name = categoryName }
            }).ToList(),
            MoviePersonRoles = dto.Roles?.Select(roleDto => new MoviePersonRoleModel
            {
                Person = new PersonModel { Name = roleDto.PersonName },
                Role = new RoleModel { Name = roleDto.RoleName }
            }).ToList()
        };
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesWithCategoriesAsync()
    {
        var movies = await _dbSet
            .Include(m => m.MovieCategories)
                .ThenInclude(mc => mc.Category)
            .Include(m => m.MoviePersonRoles)
                .ThenInclude(mpr => mpr.Person)
            .Include(m => m.MoviePersonRoles)
                .ThenInclude(mpr => mpr.Role)
            .ToListAsync();

        return movies.Select(MapToDto);
    }
}
