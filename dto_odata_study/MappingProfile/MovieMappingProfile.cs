using AutoMapper;
using dto_odata_study.DTOs;
using dto_odata_study.DTOs.dto_odata_study.DTOs;
using dto_odata_study.Models;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<MovieModel, MovieDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.MovieCategories.Select(mc => mc.Category.Name)))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.MoviePersonRoles.Select(mpr => new MoviePersonRoleDto
            {
                PersonName = mpr.Person.Name,
                RoleName = mpr.Role.Name
            })));

        CreateMap<MovieDto, MovieModel>()
            .ForMember(dest => dest.MovieCategories, opt => opt.MapFrom(src => src.Categories.Select(categoryName => new MovieCategoryModel
            {
                Category = new CategoryModel { Name = categoryName }
            })))
            .ForMember(dest => dest.MoviePersonRoles, opt => opt.MapFrom(src => src.Roles.Select(roleDto => new MoviePersonRoleModel
            {
                Person = new PersonModel { Name = roleDto.PersonName },
                Role = new RoleModel { Name = roleDto.RoleName }
            })));
    }
}