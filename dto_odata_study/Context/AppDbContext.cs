using Microsoft.EntityFrameworkCore;
using dto_odata_study.Models;

namespace dto_odata_study.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<MovieCategoryModel> MovieCategories { get; set; }
        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<MoviePersonRoleModel> MoviePersonRoles { get; set; }

        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Movie-Category (Many-to-Many)
            modelBuilder.Entity<MovieCategoryModel>()
                .HasKey(mc => new { mc.MovieId, mc.CategoryId });

            modelBuilder.Entity<MovieCategoryModel>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCategories)
                .HasForeignKey(mc => mc.MovieId);

            modelBuilder.Entity<MovieCategoryModel>()
                .HasOne(mc => mc.Category)
                .WithMany(c => c.MovieCategories)
                .HasForeignKey(mc => mc.CategoryId);

            // Movie-Person-Role (Many-to-Many with extra data)
            modelBuilder.Entity<MoviePersonRoleModel>()
                .HasKey(mpr => new { mpr.MovieId, mpr.PersonId, mpr.RoleId });

            modelBuilder.Entity<MoviePersonRoleModel>()
                .HasOne(mpr => mpr.Movie)
                .WithMany(m => m.MoviePersonRoles)
                .HasForeignKey(mpr => mpr.MovieId);

            modelBuilder.Entity<MoviePersonRoleModel>()
                .HasOne(mpr => mpr.Person)
                .WithMany(p => p.MoviePersonRoles)
                .HasForeignKey(mpr => mpr.PersonId);

            modelBuilder.Entity<MoviePersonRoleModel>()
                .HasOne(mpr => mpr.Role)
                .WithMany(r => r.MoviePersonRoles)
                .HasForeignKey(mpr => mpr.RoleId);
        }
    }
}
