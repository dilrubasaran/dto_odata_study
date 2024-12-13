
namespace dto_odata_study.Models
{
    public class MoviePersonRoleModel
    {
        
        public int MovieId { get; set; }
        public MovieModel? Movie { get; set; }

        public int PersonId { get; set; }
        public PersonModel? Person { get; set; }

        public int RoleId { get; set; }
        public RoleModel? Role { get; set; }
    
}
}
