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
    public class DirectorController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public DirectorController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DirectorResponseDto>>> GetDirector()
        {
            var director = await _context.Directors.ToListAsync();

            return Ok(director);
        }

        [HttpPost]

        public async Task<ActionResult<List<DirectorCreateDto>>> CreateDirector(DirectorCreateDto request)
        {
            var newDirector = new Director
            {
                DirFname = request.DirFname,
                DirLname = request.DirLname

            };

            _context.Directors.Add(newDirector);
            await _context.SaveChangesAsync();
            return Ok(await _context.Directors.ToListAsync());
        }

        [HttpGet("{DirId}")]
        public async Task<ActionResult<DirectorResponseDto>> GetDirector(int DirId)
        {
            var director = await _context.Directors
                .FindAsync(DirId);
            if (director == null)
            {
                return BadRequest("Not found");
            };
            return Ok(director);

        }

        [HttpPut]
        public async Task<ActionResult<List<DirectorCreateDto>>> UpdateDirector(DirectorCreateDto request, int DirId)
        {
            var dbDirector = await _context.Directors.FindAsync(DirId);
            if (dbDirector == null)
            {
                return BadRequest("Not found");
            };

            dbDirector.DirFname = request.DirFname;
            dbDirector.DirLname = request.DirLname;

            _context.Entry(dbDirector).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.Directors.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<DirectorResponseDto>> DeleteDirector(int Actid)
        {
            var dbDirector = await _context.Directors.FindAsync(Actid);
            if (dbDirector == null)
            {
                return BadRequest("Not found");
            };
            _context.Directors.Remove(dbDirector);
            await _context.SaveChangesAsync();
            return Ok(await _context.Directors.ToListAsync());

        }
    }
}
