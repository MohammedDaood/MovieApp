using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Model.Genre;

namespace MovieApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new GenreConfiguring().Configure(modelBuilder.Entity<Genre>());
        }

        public DbSet<Genre> Genres{ get; set; }

    }
}
