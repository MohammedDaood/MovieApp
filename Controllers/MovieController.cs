using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Model.Genre;
using MovieApp.Data.Model.Genre.GenreDTO;
using MovieApp.Data.Model.Movie;
using MovieApp.Data.Model.Movie.MoviesDTO;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private new List<string> _allpwedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

        private long _maxFileSize = 1024 * 1024; // 1MB

        private readonly AppDbContext _Context;

        public MoviesController(AppDbContext DbContext)
        {
            _Context = DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            //Include(m => m.Genre) to load the related Genre data for each Movie,
            var Movies = await _Context.Movies.Include(m => m.Genre)
            // .Select(m => new // we can create a new anonymous object to return only the necessary fields, including the Genre name
            // {
            //     m.Id,
            //     m.Title,
            //     m.Year,
            //     m.Rate,
            //     m.Storyline,
            //     m.GenreId,
            //     m.Genre.Name
            // })
            .OrderBy(m => m.Title).ToListAsync();
            return Ok(Movies);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMovieById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Invalid Id");
            // if we want to include the Genre data for the specific Movie, we can use Include(m => m.Genre) as well
            // var movie = await _Context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == Id);
            var movie = await _Context.Movies.FindAsync(Id);
            if (movie == null)
                return NotFound($"No Movie with id {Id}");

            return Ok(movie);
        }


        [HttpGet("GenreId")]
        public async Task<IActionResult> GetAllMoviesByGenre(byte GenreId)
        {
            //Include(m => m.Genre) to load the related Genre data for each Movie,
            var Movies = await _Context.Movies.Where(m => m.GenreId == GenreId).Include(m => m.Genre)
            // .Select(m => new // we can create a new anonymous object to return only the necessary fields, including the Genre name
            // {
            //     m.Id,
            //     m.Title,
            //     m.Year,
            //     m.Rate,
            //     m.Storyline,
            //     m.GenreId,
            //     m.Genre.Name
            // })
            .OrderBy(m => m.Title).ToListAsync();
            return Ok(Movies);
        }


        [HttpPost]
        public async Task<IActionResult> AddMovie([FromForm] AddMovieDTO dto)
        {
            if (!_allpwedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg, .jpeg, .png files are allowed.");

            if (dto.Poster.Length > _maxFileSize)
                return BadRequest("File size exceeds the 1MB limit.");


            var isGenreExists = await _Context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isGenreExists)
                return BadRequest("Invalid Genre Id.");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                Storyline = dto.Storyline,
                GenreId = dto.GenreId,
                Poster = dataStream.ToArray()
            };
            await _Context.Movies.AddAsync(movie);
            await _Context.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateMovie(int Id, [FromForm] UpdateMovieDTO dto)
        {
            // using SingleOrDefaultAsync because we have .Genre id is byte;
            var movie = await _Context.Movies.FindAsync(Id);

            if (movie == null)
                return NotFound($"No Movie with id {Id}");


                
            var isGenreExists = await _Context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isGenreExists)
                return BadRequest("Invalid Genre Id.");

            if (dto.Poster != null)
            {
             if (!_allpwedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                 return BadRequest("Only .jpg, .jpeg, .png files are allowed.");

            if (dto.Poster.Length > _maxFileSize)
                return BadRequest("File size exceeds the 1MB limit.");
                
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            movie.Poster =  dataStream.ToArray() ;
            }
            else
            {
                // If no new poster is uploaded, keep the existing poster
                movie.Poster = movie.Poster;
            }

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.Storyline = dto.Storyline;
            movie.GenreId = dto.GenreId;

            await _Context.SaveChangesAsync();
            return Ok(movie);
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> UpdateGenres(int Id)
        {
            // using SingleOrDefaultAsync because we have .Genre id is byte;
            var movie = await _Context.Movies.FindAsync(Id);

            if (movie == null)
                return NotFound($"No Movie with id {Id}");

            _Context.Movies.Remove(movie);
            await _Context.SaveChangesAsync();
            return Ok(movie);
        }
    }
}
