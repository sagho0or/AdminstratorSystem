using AdministratorSystem.Data;
using AdministratorSystem.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministratorSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly DataContext _context;
        public AssessmentController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Assessment>>> AddAssessment(AssessmentDto assessmentDto)
        {

            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid based on data annotations, return a BadRequest
                return BadRequest(ModelState);
            }

            var assessment = new Assessment {
                Title = assessmentDto.Title,
                Description = assessmentDto.Description,
                AssessmentId = new Random().Next(1000, 9999),
            };


            _context.Assessment.Add(assessment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Assessment.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Assessment>>> GetAssessments()
        {
            return Ok(await _context.Assessment.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetAssessment(int id)
        {
            var assessment = await _context.Assessment.FindAsync(id);
            if (assessment == null)
            {
                return BadRequest("assessment not found");
            }
            return Ok(assessment);
        }



    }
}
