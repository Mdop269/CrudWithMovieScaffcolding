using Azure.Core;
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
    public class MovieDirectionController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public MovieDirectionController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDirectionResponseDto>>> GetMovieDirection()
        {
            var movieDirections = await _context.MovieDirections
                .ToListAsync();

            return Ok(movieDirections);
        }

        [HttpPost]

        public async Task<ActionResult<List<MovieDirectionCreateDto>>> CreateMovieDirection(MovieDirectionCreateDto request)
        {
            // Check if the Movie and director objects exist
            var movie = await _context.Movies.FindAsync(request.MovId);
            var director = await _context.Directors.FindAsync(request.DirId);

            if (movie == null || director == null)
            {
                return BadRequest("Invalid Movie or director ID");
            }


            var newMovieDirection = new MovieDirection
            {
                MovId = request.MovId,
                DirId = request.DirId,
            };

            _context.MovieDirections.Add(newMovieDirection);
            await _context.SaveChangesAsync();

            var movieDirectionResponseDto = new MovieDirectionCreateDto
            {
                MovId = request.MovId,
                DirId = request.DirId
            };
            return Ok(movieDirectionResponseDto);
        }

        [HttpGet("{MovDirId}")]
        public async Task<ActionResult<MovieDirectionCreateDto>> GetMovieDirection(int MovDirId)
        {
            var dbMovieDirection = await _context.MovieDirections
                .FindAsync(MovDirId);
            if (dbMovieDirection == null)
            {
                return BadRequest("Not found");
            };
            return Ok(dbMovieDirection);

        }

        [HttpPut("{MovDirId}")]
        public async Task<ActionResult<List<MovieDirectionCreateDto>>> UpdateMovieDirection(MovieDirectionCreateDto request, int MovDirId)
        {
            var dbMovieDirection = await _context.MovieDirections.FindAsync(MovDirId);
            if (dbMovieDirection == null)
            {
                return BadRequest("Not found");
            };

            dbMovieDirection.MovId = request.MovId;
            dbMovieDirection.DirId = request.DirId;

            _context.Entry(dbMovieDirection).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.MovieDirections.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<MovieDirectionResponseDto>> DeleteMovieDirection(int DirId)
        {
            var dbMovieDirection = await _context.MovieDirections.FindAsync(DirId);
            if (dbMovieDirection == null)
            {
                return BadRequest("Not found");
            };
            _context.MovieDirections.Remove(dbMovieDirection);
            await _context.SaveChangesAsync();
            return Ok(await _context.MovieDirections.ToListAsync());

        }
    }
}
