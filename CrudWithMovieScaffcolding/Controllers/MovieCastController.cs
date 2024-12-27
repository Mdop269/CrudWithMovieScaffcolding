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
    public class MovieCastController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public MovieCastController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieCastResponseDto>>> GetCasts()
        {
            var casts = await _context.MovieCasts
                .Include(a => a.Mov)
                .Include(a => a.Act)
                .ToListAsync();

            var result = casts.Select(mc => new MovieCastResponseDto
            {
                CastId = mc.CastId,
                MovId = mc.MovId,
                ActId = mc.ActId,
                Role = mc.Role,
                Movie = new MovieResponseDto
                {
                    MovId = mc.Mov.MovId,
                    MovTitle = mc.Mov.MovTitle,
                    MovYear = mc.Mov.MovYear,
                    MovTime = mc.Mov.MovYear,
                    MovLang = mc.Mov.MovLang,
                    MovRlDt = mc.Mov.MovRlDt,
                    MovRelCountry = mc.Mov.MovRelCountry,
                },
                Actor = new ActorResponseDto
                {
                    ActId = mc.Act.ActId,
                    ActFname = mc.Act.ActFname,
                    ActLname = mc.Act.ActLname,
                    ActGender = mc.Act.ActGender
                }
            }).ToList();

            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult<List<MovieCastCreateDto>>> CreateCasts(MovieCastCreateDto request)
        {
            // Check if the Movie and Actor objects exist
            var movie = await _context.Movies.FindAsync(request.MovId);
            var actor = await _context.Actors.FindAsync(request.ActId);

            if (movie == null || actor == null)
            {
                return BadRequest("Invalid Movie or Actor ID");
            }


            var newMovieCast = new MovieCast
            {
                MovId = request.MovId,
                ActId = request.ActId,
                Role = request.Role,
            };

            _context.MovieCasts.Add(newMovieCast);
            await _context.SaveChangesAsync();
            return Ok(await _context.MovieCasts.ToListAsync());
        }

        [HttpGet("{CastId}")]
        public async Task<ActionResult<DirectorResponseDto>> GetDirector(int CastId)
        {
            var dbMovieCast = await _context.MovieCasts
                .FindAsync(CastId);
            if (dbMovieCast == null)
            {
                return BadRequest("Not found");
            };
            return Ok(dbMovieCast);

        }

        [HttpPut("{CastId}")]
        public async Task<ActionResult<List<MovieCastCreateDto>>> UpdateActor(MovieCastCreateDto request, int CastId)
        {
            var dbMovieCast = await _context.MovieCasts.FindAsync(CastId);
            if (dbMovieCast == null)
            {
                return BadRequest("Not found");
            };

            dbMovieCast.MovId = request.MovId;
            dbMovieCast.ActId = request.ActId;
            dbMovieCast.Role = request.Role;

            _context.Entry(dbMovieCast).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.MovieCasts.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<MovieResponseDto>> DeleteActor(int CastId)
        {
            var dbMovieCast = await _context.MovieCasts.FindAsync(CastId);
            if (dbMovieCast == null)
            {
                return BadRequest("Not found");
            };
            _context.MovieCasts.Remove(dbMovieCast);
            await _context.SaveChangesAsync();
            return Ok(await _context.MovieCasts.ToListAsync());

        }

    }
}
