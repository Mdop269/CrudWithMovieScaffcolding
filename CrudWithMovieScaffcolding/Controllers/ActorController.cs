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
    public class ActorController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public ActorController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorResponseDto>>> GetActor()
        {
            var actor = await _context.Actors

                .ToListAsync();

            return Ok(actor);
        }

        [HttpPost]

        public async Task<ActionResult<List<ActorCreateDto>>> CreateActor(ActorCreateDto request)
        {
            var newActor = new Actor
            {
                ActFname = request.ActFname,
                ActLname = request.ActLname,
                ActGender = request.ActGender,
            };

            _context.Actors.Add(newActor);
            await _context.SaveChangesAsync();
            return Ok(await _context.Actors.ToListAsync());
        }

        [HttpGet("{ActId}")]
        public async Task<ActionResult<ActorResponseDto>> GetActor(int ActId)
        {
            var actor = await _context.Actors
                .FindAsync(ActId);
            if (actor == null)
            {
                return BadRequest("Not found");
            };
            return Ok(actor);

        }

        [HttpPut("{ActId}")]
        public async Task<ActionResult<List<ActorCreateDto>>> UpdateActor(ActorCreateDto request, int ActId)
        {
            var dbActor = await _context.Actors.FindAsync(ActId);
            if (dbActor == null)
            {
                return BadRequest("Not found");
            };

            dbActor.ActFname = request.ActFname;
            dbActor.ActLname = request.ActLname;
            dbActor.ActGender = request.ActGender;

            _context.Entry(dbActor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //var actorResponseDto = new ActorResponseDto
            //{
            //    ActId = dbActor.ActId,
            //    ActFname = dbActor.ActFname,
            //    ActLname = dbActor.ActLname,
            //    ActGender = dbActor.ActGender
            //};

            //return Ok(actorResponseDto);
            return Ok(await _context.Actors.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<ActorResponseDto>> DeleteActor(int Actid)
        {
            var dbActor = await _context.Actors.FindAsync(Actid);
            if (dbActor == null)
            {
                return BadRequest("Not found");
            };
            _context.Actors.Remove(dbActor);
            await _context.SaveChangesAsync();
            return Ok(await _context.Actors.ToListAsync());

        }
    }
}
 