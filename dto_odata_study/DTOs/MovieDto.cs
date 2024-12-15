namespace dto_odata_study.DTOs
{
    namespace dto_odata_study.DTOs
    {
        public class MovieDto
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public int ReleaseYear { get; set; }
            public string? Description { get; set; }
            public string? PosterUrl { get; set; }

            public List<string?>? Categories { get; set; }

            // Rol bilgilerinin detayları
            public List<MoviePersonRoleDto>? Roles { get; set; }
        }

        
    }

}
