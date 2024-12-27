using CrudWithMovieScaffcolding.DTOs_create;
using CrudWithMovieScaffcolding.Dtos_Response;
using CrudWithMovieScaffcolding.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudWithMovieScaffcolding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public GenreController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreResponseDto>>> GetGenre()
        {
            var genre = await _context.Genres

                .ToListAsync();

            return Ok(genre);
        }

        [HttpPost]

        public async Task<ActionResult<List<GenreCreateDto>>> CreateGenre(GenreCreateDto request)
        {
            var newGenre = new Genre
            {
                GenTitle = request.GenTitle

            };

            _context.Genres.Add(newGenre);
            await _context.SaveChangesAsync();
            return Ok(await _context.Genres.ToListAsync());

        }

        [HttpGet("{GenId}")]
        public async Task<ActionResult<GenreResponseDto>> GetGenre(int GenId)
        {
            var genre = await _context.Genres
                .FindAsync(GenId);
            if (genre == null)
            {
                return BadRequest("Not found");
            };
            return Ok(genre);

        }

        [HttpPut]
        public async Task<ActionResult<List<GenreCreateDto>>> UpdateGenre(GenreCreateDto request, int GenId)
        {
            var dbGenre = await _context.Genres.FindAsync(GenId);
            if (dbGenre == null)
            {
                return BadRequest("Not found");
            };

            dbGenre.GenTitle = request.GenTitle;

            _context.Entry(dbGenre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.Genres.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<GenreResponseDto>> DeleteGenre(int Genid)
        {
            var dbGenre = await _context.Genres.FindAsync(Genid);
            if (dbGenre == null)
            {
                return BadRequest("Not found");
            };
            _context.Genres.Remove(dbGenre);
            await _context.SaveChangesAsync();
            return Ok(await _context.Genres.ToListAsync());
        }
    }
}
