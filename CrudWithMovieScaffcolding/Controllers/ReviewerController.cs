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
    public class ReviewerController : ControllerBase
    {
        private readonly FilmStudioContext _context;

        public ReviewerController(FilmStudioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewerResponseDto>>> GetReviewer()
        {
            var reviewers = await _context.Reviewers

                .ToListAsync();

            return Ok(reviewers);
        }

        [HttpPost]

        public async Task<ActionResult<List<ReviewerCreateDto>>> CreateReviewer(ReviewerCreateDto request)
        {
            var newReviewer = new Reviewer
            {
                RevName = request.RevName

            };

            _context.Reviewers.Add(newReviewer);
            await _context.SaveChangesAsync();
            return Ok(await _context.Reviewers.ToListAsync());

        }

        [HttpGet("{RevId}")]
        public async Task<ActionResult<ReviewerResponseDto>> GetReviewer(int RevId)
        {
            var reviewers = await _context.Reviewers
                .FindAsync(RevId);
            if (reviewers == null)
            {
                return BadRequest("Not found");
            };
            return Ok(reviewers);

        }

        [HttpPut]
        public async Task<ActionResult<List<ReviewerCreateDto>>> UpdateReviewer(ReviewerCreateDto request, int RevId)
        {
            var dbReviewer = await _context.Reviewers.FindAsync(RevId);
            if (dbReviewer == null)
            {
                return BadRequest("Not found");
            };

            dbReviewer.RevName = request.RevName;

            _context.Entry(dbReviewer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(await _context.Reviewers.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<ReviewerResponseDto>> DeleteReviewer(int RevId)
        {
            var dbReviewer = await _context.Reviewers.FindAsync(RevId);
            if (dbReviewer == null)
            {
                return BadRequest("Not found");
            };
            _context.Reviewers.Remove(dbReviewer);
            await _context.SaveChangesAsync();
            return Ok(await _context.Reviewers.ToListAsync());
        }
    }
}
