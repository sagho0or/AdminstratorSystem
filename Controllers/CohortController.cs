using AdministratorSystem.Data;
using AdministratorSystem.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdministratorSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CohortController : ControllerBase
    {
        private readonly DataContext _context;
        public CohortController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("add/{courseId}")]
        public ActionResult AddCohort(int courseId, [FromBody] CohortDto cohortDto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid based on data annotations, return a BadRequest
                return BadRequest(ModelState);
            }
            var existingCourse = _context.Course.Find(courseId);
            if (existingCourse == null)
            {
                return NotFound("Course not found");
            }

            var cohort = new Cohort {
                AcademicYear = cohortDto.AcademicYear,
                CourseId = courseId
            };
            
            _context.Cohort.Add(cohort);
            _context.SaveChanges();

            return Ok("Cohort added to the course successfully");
        }


        [HttpGet]
        public async Task<ActionResult<List<Cohort>>> GetCohorts()
        {

            var cohorts = await _context.Cohort
                .Include(c => c.Course) // Include the Course navigation property
                .ToListAsync();

            return Ok(cohorts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cohort>> GetCohort(int id)
        {
            var cohort = await _context.Cohort
                .Include(c => c.Students) // Include the Students
                .Include(c => c.Course) // Include the Course
                    .ThenInclude(cm => cm.CourseModules) // Include the course module
                    .ThenInclude(cms => cms.Module) // Include the module
                    .ThenInclude(ma => ma.ModuleAssessments) // Include the module assessment
                    .ThenInclude(s => s.Assessment) // Include the assessment
                .FirstOrDefaultAsync(c => c.CohortId == id);

            if (cohort == null)
            {
                return BadRequest("cohort not found");
            }
            return Ok(cohort);
        }



    }
}
