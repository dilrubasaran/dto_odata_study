namespace dto_odata_study.Models
{
    public class PersonModel
    {
            public int Id { get; set; }
            public string?  Name { get; set; }
            public string? Title { get; set; }
            public List<MoviePersonRoleModel> MoviePersonRoles { get; set; }
    }
}
