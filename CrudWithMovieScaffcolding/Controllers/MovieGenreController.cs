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
    public class MovieGenreController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public MovieGenreController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieGenreResponseDto>>> GetMovieGenre()
        {
            var movieGenres = await _context.MovieGenres
                .ToListAsync();

            return Ok(movieGenres);
        }

        [HttpPost]

        public async Task<ActionResult<List<MovieGenreCreateDto>>> CreateMovieGenre(MovieGenreCreateDto request)
        {
            // Check if the Movie and director objects exist
            var movie = await _context.Movies.FindAsync(request.MovId);
            var genre = await _context.Directors.FindAsync(request.GenId);

            if (movie == null || genre == null)
            {
                return BadRequest("Invalid Movie or Genre ID");
            }


            var newMovieGenre = new MovieGenre
            {
                MovId = request.MovId,
                GenId = request.GenId,
            };

            _context.MovieGenres.Add(newMovieGenre);
            await _context.SaveChangesAsync();

            var movieGenreResponseDto = new MovieGenreCreateDto
            {
                MovId = request.MovId,
                GenId = request.GenId
            };
            return Ok(movieGenreResponseDto);
        }

        [HttpGet("{MovGenId}")]
        public async Task<ActionResult<MovieGenreCreateDto>> GetMovieGenre(int MovGenId)
        {
            var dbMovieGenre = await _context.MovieGenres
                .FindAsync(MovGenId);
            if (dbMovieGenre == null)
            {
                return BadRequest("Not found");
            };
            return Ok(dbMovieGenre);

        }

        [HttpPut("{MovGenId}")]
        public async Task<ActionResult<List<MovieGenreCreateDto>>> UpdateMovieGenre(MovieGenreCreateDto request, int MovGenId)
        {
            var dbMovieGenre = await _context.MovieGenres.FindAsync(MovGenId);
            if (dbMovieGenre == null)
            {
                return BadRequest("Not found");
            };

            dbMovieGenre.MovId = request.MovId;
            dbMovieGenre.GenId = request.GenId;

            _context.Entry(dbMovieGenre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.MovieGenres.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<MovieDirectionResponseDto>> DeleteMovieGenre(int GenId)
        {
            var dbMovieGenre = await _context.MovieGenres.FindAsync(GenId);
            if (dbMovieGenre == null)
            {
                return BadRequest("Not found");
            };
            _context.MovieGenres.Remove(dbMovieGenre);
            await _context.SaveChangesAsync();
            return Ok(await _context.MovieGenres.ToListAsync());

        }
    }
}
