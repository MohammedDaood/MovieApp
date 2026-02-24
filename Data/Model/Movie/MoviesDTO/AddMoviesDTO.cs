
namespace MovieApp.Data.Model.Movie.MoviesDTO
{
    public class AddMovieDTO
{
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Rate { get; set; }
        public string Storyline { get; set; } = string.Empty;
        public IFormFile Poster { get; set; } 
        public byte GenreId { get; set; }
}
}