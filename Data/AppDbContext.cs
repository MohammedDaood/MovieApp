using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Model.Genre;
using MovieApp.Data.Model.Movie;

namespace MovieApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new GenreConfiguring().Configure(modelBuilder.Entity<Genre>());
            new MovieConfiguring().Configure(modelBuilder.Entity<Movie>());

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }


    }
}
