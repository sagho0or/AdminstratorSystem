using AdministratorSystem.Data;
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



        [HttpPost]
        public async Task<ActionResult<List<Cohort>>> AddCohort(Cohort cohort)
        {
            _context.Cohort.Add(cohort);
            await _context.SaveChangesAsync();

            return Ok(await _context.Cohort.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<Cohort>> AddCohort(int courseId, string academicYear)
        {
            try
            {
                // Check if the course exists
                var coursexists = await _context.Cohort.FindAsync(courseId);
                if (coursexists == null)
                {
                    return NotFound("Course not found");
                }

                // Create a new cohort
                var newCohort = new Cohort
                {
                    CourseId = courseId,
                    AcademicYear = academicYear
                };
                // Add and save the new cohort to the database
                _context.Cohort.Add(newCohort);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetCohort), new { id = newCohort.CohortId }, newCohort);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding cohort: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Cohort>>> GetCohorts()
        {
            return Ok(await _context.Cohort.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cohort>> GetCohort(int id)
        {
            var cohort = await _context.Cohort.FindAsync(id);

            if (cohort == null)
            {
                return BadRequest("cohort not found");
            }
            return Ok(cohort);
        }



    }
}
