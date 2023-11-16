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
        public async Task<ActionResult<List<Student>>> AddStudent(int cohortId, string fullname)
        {
                // Check if the cohort exists
                var cohort = _context.Cohort.Find(cohortId);
                if (cohort == null)
                {
                    return NotFound("Cohort not found");
                }

                // Create a new student
                var newStudent = new Student
                {
                    CohortId = cohortId,
                    FullName = fullname
                };


                _context.Students.Add(newStudent);
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

            foreach (var module in student.Cohort.Course.Modules)
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
