using AdministratorSystem.Data;
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
        public async Task<ActionResult<List<Course>>> AddAssessment(Assessment assessment)
        {
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
            var assessment =  await _context.Assessment.FindAsync(id);
            if(assessment == null) {
                return BadRequest("assessment not found");
            }
            return Ok(assessment);
        }



    }
}
