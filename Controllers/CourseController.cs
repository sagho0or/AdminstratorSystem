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
        public async Task<ActionResult<List<Course>>> AddCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return Ok(await _context.Course.ToListAsync());
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
