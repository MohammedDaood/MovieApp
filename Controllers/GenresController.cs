using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Model.Genre;
using MovieApp.Data.Model.Genre.GenreDTO;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public GenresController(AppDbContext DbContext)
        {
            _Context = DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var Genres = await _Context.Genres.OrderBy(g=>g.Name).ToListAsync();
            return Ok(Genres);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenres([FromBody]AddGenresDTO dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };
            await _Context.Genres.AddAsync(genre);
            await _Context.SaveChangesAsync();
            return Ok(genre);
        }



        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateGenres( int Id , [FromBody] UpdateGenresDTO dto)
        {
            // using SingleOrDefaultAsync because we have .Genre id is byte;
            var genre = await _Context.Genres.SingleOrDefaultAsync(g=>g.Id==Id);

            if (genre == null)
                return NotFound($"No Genre with id {Id}");

            genre.Name = dto.Name;
            await _Context.SaveChangesAsync();
            return Ok(genre);
        }




        [HttpDelete("{Id}")]
        public async Task<IActionResult> UpdateGenres(int Id)
        {
            // using SingleOrDefaultAsync because we have .Genre id is byte;
            var genre = await _Context.Genres.SingleOrDefaultAsync(g => g.Id == Id);

            if (genre == null)
                return NotFound($"No Genre with id {Id}");

            _Context.Genres.Remove(genre);
            await _Context.SaveChangesAsync();
            return Ok(genre);
        }
    }
}
