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

                string uniqueDigits = GenerateUniqueDigits();

                // Create a new student
                var newStudent = new Student
                    {
                        CohortId = cohortId,
                        FullName = fullname,
                        StundetIdentifier = $"{cohort.AcademicYear}{uniqueDigits}"
                };

                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();

                return Ok(await _context.Students.ToListAsync());
            
        }

        [HttpPost("{studentId}/{cohortId}")]
        public async Task<ActionResult<Student>> RegisterStudentForCourse(int studentId, int cohortId)
        {
            try
            {
                var student = await _context.Students.FindAsync(studentId);
                if (student == null)
                {
                    return NotFound("Student not found");
                }

                var cohort = await _context.Cohorts.Include(c => c.Students).FirstOrDefaultAsync(c => c.CohortId == cohortId);
                if (cohort == null)
                {
                    return NotFound("Cohort not found");
                }

                student.CohortId = cohortId;
                cohort.Students.Add(student);

                await _context.SaveChangesAsync();

                return Ok(student);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error registering student: {ex.Message}");
            }
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

            if (student == null)
            {
                return BadRequest("student not found");
            }

            // Calculate the average of modules marks
            double totalMarks = 0;
            int numberOfModules = 0;

            foreach (var module in student.Cohort.Course.Modules)
            {
                var moduleTitle = module.Title;
                var assesments = module.Assessments.Select(x => x.AssessmentId);
                var mark = module.Mark;
                var result = module.Result;

                if (mark)
                {
                    // Logic to calculate total marks from course
                    totalMarks += mark; 
                }
                numberOfModules++;

            }
            // Calculate the average marks for the module
            if (numberOfModules > 0 && totalMarks !== null)
            {
                double averageMark = totalMarks / numberOfModules;

                var studentCourse = await _context.StudentCourse
                    .FirstOrDefaultAsync(sp => sp.StudentId == studentId);

                // Update the existing StudentProgram entity
                studentCourse.Mark = (int)Math.Round(averageMark); // Set the program mark
                _context.StudentCourse.Update(studentCourse);


                await _context.SaveChangesAsync(); // Save changes to the database
            }

            return Ok(student);
        }

        [HttpPost("student/{studentId}/modules/{moduleId}/assessments/{assessmentId}/addMark")]
        public async Task<ActionResult> AddMarkToAssessment(int moduleId, int assessmentId, int studentId, int mark)
        {
            var module = await _context.Modules.Include(m => m.Assessments)
                                               .FirstOrDefaultAsync(m => m.ModuleId == moduleId);

            if (module == null)
            {
                return NotFound("Module not found");
            }

            var assessment = module.Assessments.FirstOrDefault(a => a.AssessmentId == assessmentId);

            if (assessment == null)
            {
                return NotFound("Assessment not found in this module");
            }

            var existingMarks = _context.StudentAssessments.Where(sa => sa.AssessmentId == assessmentId)
                                                          .Sum(sa => sa.Mark);

            var moduleMark = existingMarks + mark;
            // Check if adding this mark would exceed the maximum allowed for this assessment
            if (moduleMark > assessment.MaximumMark)
            {
                return BadRequest("Mark exceeds the maximum allowed for this assessment");
            }
            else
            {
                var moduleResult;

                if (!moduleMark.HasValue)
                {
                    moduleResult = "Undefined";
                }

                if (moduleMark >= 50)
                {
                    moduleResult = "Pass";
                }
                else if (moduleMark >= 45)
                {
                    moduleResult = "PassCompensation";
                }
                else
                {
                    moduleResult = "Fail";
                }

                var studentModule = new StudentModule
                {
                    ModuleId = moduleId,
                    studentId = studentId,
                    Mark = moduleMark,
                    Results = moduleResult
                }
            }

            // Add the mark for the student's assessment
            var studentAssessment = new StudentAssessment
            {
                Mark = mark,
                AssessmentId = assessmentId,
                StudentId = studentId
            };

            _context.StudentAssessments.Add(studentAssessment);
            await _context.SaveChangesAsync();

            return Ok("Mark added successfully");
        }


    }
}
