using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MovieApp.Data.Model.Genre
{
    public class GenreConfiguring : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(g => g.Name).HasMaxLength(100);

            builder.Property(g => g.Id)
           .ValueGeneratedOnAdd();

        }
    }
}
