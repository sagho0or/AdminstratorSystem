using AdministratorSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<ActionResult<List<Course>>> AddCourse(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid based on data annotations, return a BadRequest
                return BadRequest(ModelState);
            }

            var course = new Course();
            course.DurationInYears = courseDto.DurationInYears;
            course.Title = courseDto.Title;
            

            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return Ok(await _context.Course.ToListAsync());
        }

        [HttpPost("addModuleToCourse/{courseId}/{moduleId}")]
        public async Task<ActionResult<Course>> assignModule(int courseId, int moduleId, bool IsRequired)
        {
            

            var course = _context.Courses.FirstOrDefault(p => p.CourseId == courseId);
            if (course != null)
            {
                // Check the duration of the program to determine the module limit
                int moduleLimit = program.DurationInYears * 6; // Each year allows 6 modules
                if (program.Modules.Count >= moduleLimit)
                {
                    return BadRequest($"The course can't accept more than {moduleLimit} modules.");
                }

                course.Modules.Add(_context.Module.Find(moduleId));
                _context.SaveChanges();
                return Ok("Module added to the course successfully");
            }

            return NotFound("Program not found");
        }
        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            return Ok(await _context.Course.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return BadRequest("student not found");
            }
            return Ok(course);
        }


    }
}
