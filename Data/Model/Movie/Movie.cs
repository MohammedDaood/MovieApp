using System;

 namespace MovieApp.Data.Model.Movie
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public int Year { get; set; }
        public double Rate { get; set; }
        public string Storyline { get; set; } = string.Empty;
        public byte[] Poster { get; set; } 
        public byte GenreId { get; set; }
     public MovieApp.Data.Model.Genre.Genre Genre { get; set; }
      
    }
}