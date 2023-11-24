using AdministratorSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace AdministratorSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DataContext _context;
        public CourseController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("addCourse/{title}/{durationInYears}")]
        public async Task<ActionResult<List<Course>>> AddCourse(string title, int durationInYears)
        {
            if (durationInYears < 1 && durationInYears > 3)
            {
                return BadRequest($"The durationInYears should be a number between 1 to 3");
            }
            var course = new Course();
            course.DurationInYears = durationInYears;
            course.Title = title;


            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return Ok(await _context.Course.ToListAsync());
        }


        [HttpPost("assign-modules")]
        public async Task<ActionResult<Course>> assignModule(int courseId, int moduleId, bool IsRequired)
        {

            var course = _context.Course.FirstOrDefault(p => p.CourseId == courseId);
            if (course != null)
            {

                if (course.CourseModules != null)
                {
                    // Check the duration of the course to determine the module limit
                    int moduleLimit = course.DurationInYears * 6; // Each year allows 6 modules
                    if (course.CourseModules.Count >= moduleLimit)
                    {
                        return BadRequest($"The course can't accept more than {moduleLimit} modules.");
                    }
                }
                
                var module = _context.Module.Find(moduleId);
                if (module != null)
                {
                    course.CourseModules.Add(new CourseModule { 
                        CourseId = courseId, 
                        ModuleId = module.ModuleId,
                        IsRequired = module.IsRequired
                    });
                    _context.SaveChanges();
                    return Ok("Module added to the course successfully");

                }else
                {
                    return NotFound("Module not found");
                }
            }

            return NotFound("Course not found");
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            return Ok(await _context.Course.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {

            var course = await _context.Course
                .Include(c => c.Cohorts) // Include the Cohorts
                .Include(c => c.CourseModules) // Include the CourseModules
                    .ThenInclude(cm => cm.Module) // Include the Module entity in CourseModules
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
            {
                return BadRequest("Course not found");
            }

            return Ok(course);
        }


    }
}
