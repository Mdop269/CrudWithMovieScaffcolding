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
    public class MovieController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public MovieController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieResponseDto>>> GetMovie()
        {
            var movie = await _context.Movies.ToListAsync();

            return Ok(movie);
        }


        [HttpPost]
        public async Task<ActionResult<List<MovieCreateDto>>> CreateMovie(MovieCreateDto request)
        {
            var newMovie = new Movie
            {

                MovTitle = request.MovTitle,
                MovYear = request.MovYear,
                MovTime = request.MovYear,
                MovLang = request.MovLang,
                MovRlDt = request.MovRlDt,
                MovRelCountry = request.MovRelCountry
            };

            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();
            return Ok(await _context.Movies.ToListAsync());

        }

        [HttpGet("{MovId}")]
        public async Task<ActionResult<MovieResponseDto>> GetMovie(int MovId)
        {
            var movie = await _context.Movies
                .FindAsync(MovId);
            if (movie == null)
            {
                return BadRequest("Not found");
            };
            return Ok(movie);

        }

        [HttpPut]
        public async Task<ActionResult<List<MovieCreateDto>>> UpdateMovie(MovieCreateDto request, int MovId)
        {
            var dbMovie = await _context.Movies.FindAsync(MovId);
            if (dbMovie == null)
            {
                return BadRequest("Not found");
            };

            dbMovie.MovTitle = request.MovTitle;
            dbMovie.MovYear = request.MovYear;
            dbMovie.MovTime = request.MovYear;
            dbMovie.MovLang = request.MovLang;
            dbMovie.MovRlDt = request.MovRlDt;
            dbMovie.MovRelCountry = request.MovRelCountry;

            _context.Entry(dbMovie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.Movies.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<MovieResponseDto>> DeleteMovie(int MovId)
        {
            var dbMovie = await _context.Movies.FindAsync(MovId);
            if (dbMovie == null)
            {
                return BadRequest("Not found");
            };
            _context.Movies.Remove(dbMovie);
            await _context.SaveChangesAsync();
            return Ok(await _context.Movies.ToListAsync());
        }
    }
}
