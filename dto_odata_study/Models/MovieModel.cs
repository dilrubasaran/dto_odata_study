using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace dto_odata_study.Models
{
    public class MovieModel
    {
            public int Id { get; set; }
            public string? Title { get; set; }
            public int ReleaseYear { get; set; }
            public string? Description { get; set; }
            public string? PosterUrl { get; set; }
            public List<MovieCategoryModel> MovieCategories { get; set; }
            public List<MoviePersonRoleModel> MoviePersonRoles { get; set; }

    }
}
