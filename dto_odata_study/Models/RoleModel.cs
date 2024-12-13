namespace dto_odata_study.Models
{
    public class RoleModel
    {
            public int Id { get; set; }
            public string? Name { get; set; }
            public List<MoviePersonRoleModel>? MoviePersonRoles { get; set; }
        
    }
}
