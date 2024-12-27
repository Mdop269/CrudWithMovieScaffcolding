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
    public class RatingController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public RatingController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RatingResponseDto>>> GetRating()
        {
            var ratings = await _context.Ratings
                .ToListAsync();

            return Ok(ratings);
        }

        [HttpPost]

        public async Task<ActionResult<List<RatingCreateDto>>> CreateRating(RatingCreateDto request)
        {
            // Check if the Movie and director objects exist
            var movie = await _context.Movies.FindAsync(request.MovId);
            var reviewer = await _context.Directors.FindAsync(request.RevId);

            if (movie == null || reviewer == null)
            {
                return BadRequest("Invalid Movie or Rating ID");
            }


            var newRating = new Rating
            {
                MovId = request.MovId,
                RevId = request.RevId,
                RevStars = request.RevStars,
                NumORatings = request.NumORatings,

            };

            _context.Ratings.Add(newRating);
            await _context.SaveChangesAsync();

            var RatingResponseDto = new RatingCreateDto
            {
                MovId = request.MovId,
                RevId = request.RevId,
                RevStars = request.RevStars,
                NumORatings = request.NumORatings,

            };
            return Ok(RatingResponseDto);
        }

        [HttpGet("{RatingId}")]
        public async Task<ActionResult<RatingCreateDto>> GetRating(int RatingId)
        {
            var dbRating = await _context.Ratings
                .FindAsync(RatingId);
            if (dbRating == null)
            {
                return BadRequest("Not found");
            };
            return Ok(dbRating);

        }

        [HttpPut("{RatingId}")]
        public async Task<ActionResult<List<RatingCreateDto>>> UpdateRating(RatingCreateDto request, int RatingId)
        {
            var dbRating = await _context.Ratings.FindAsync(RatingId);
            if (dbRating == null)
            {
                return BadRequest("Not found");
            };

            dbRating.MovId = request.MovId;
            dbRating.RevId = request.RevId;
            dbRating.RevStars = request.RevStars;
            dbRating.NumORatings = request.NumORatings;

            _context.Entry(dbRating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.Ratings.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<MovieDirectionResponseDto>> DeleteRating(int RatingId)
        {
            var dbRating = await _context.Ratings.FindAsync(RatingId);
            if (dbRating == null)
            {
                return BadRequest("Not found");
            };
            _context.Ratings.Remove(dbRating);
            await _context.SaveChangesAsync();
            return Ok(await _context.Ratings.ToListAsync());

        }
    }
}
