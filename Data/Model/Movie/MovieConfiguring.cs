using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MovieApp.Data.Model.Movie
{
    public class MovieConfiguring : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(g => g.Title).HasMaxLength(250);

            builder.Property(g => g.Id)
           .ValueGeneratedOnAdd();

           builder.Property(m=>m.Storyline).HasMaxLength(2500);

        }
    }
}
