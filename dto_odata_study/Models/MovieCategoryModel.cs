
namespace dto_odata_study.Models
{
    public class MovieCategoryModel
    {
            public int MovieId { get; set; }
            public MovieModel Movie { get; set; }
            public int CategoryId { get; set; }
            public CategoryModel  Category { get; set; }
        
    }
}
