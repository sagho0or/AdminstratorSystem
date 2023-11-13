using AdministratorSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministratorSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            foreach (var module in student.Course.Modules)
            {
                var moduleTitle = module.Title;
                var assesments = module.Assessments.Select(x => x.AssessmentId);
                var score = _context.StudentAssesments.Where(item => assesments.Contains(item.Assessment.AssessmentId)).Sum(item => item.Score);
            }


            if (student == null)
            {
                return BadRequest("student not found");
            }
            return Ok(student);
        }



    }
}
