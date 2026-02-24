using System.ComponentModel.DataAnnotations;

namespace MovieApp.Data.Model.Genre.GenreDTO
{
    public class AddGenresDTO
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
