
namespace dto_odata_study.Models
{
    public class CategoryModel
    {
       
            public int Id { get; set; }
            public string? Name { get; set; }
            public  List<MovieCategoryModel>? MovieCategories { get; set; }
        
    }
}
