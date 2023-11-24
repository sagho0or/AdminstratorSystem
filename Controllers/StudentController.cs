using AdministratorSystem.Data;
using AdministratorSystem.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid based on data annotations, return a BadRequest
                return BadRequest(ModelState);
            }
            // Check if the cohort exists
            var cohort = _context.Cohort.Find(cohortId);
            if (cohort == null)
            {
                return NotFound("Cohort not found");
            }

            string uniqueDigits = GenerateUniqueDigits();

            string GenerateUniqueDigits()
            {
                Random rand = new Random();
                int randomNumber = rand.Next(100000, 999999); // Generates a random 6-digit number
                return randomNumber.ToString();
            }
            // Create a new student
            var newStudent = new Student
            {
                CohortId = cohortId,
                FullName = fullname,
                StundetIdentifier = $"{cohort.AcademicYear}{uniqueDigits}"
            };

            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();


            // Get the newly added student with Cohort, Course, Modules, and Assessments
            var addedStudent = await _context.Students
                .Include(s => s.Cohort)
                    .ThenInclude(c => c.Course)
                        .ThenInclude(course => course.CourseModules)
                            .ThenInclude(cm => cm.Module)
                                .ThenInclude(m => m.ModuleAssessments)
                .FirstOrDefaultAsync(s => s.StudentId == newStudent.StudentId);


            //Associate modules and assessments with the added student
            if (addedStudent.Cohort != null && addedStudent.Cohort.Course != null)
            {

                var studentCourse = new StudentCourse
                {
                    StudentId = addedStudent.StudentId,
                    CourseId = addedStudent.Cohort.Course.CourseId,
                    Mark = null, // Set as null initially
                    Result = "Undefined" // Set as undefined initially
                };
                _context.StudentCourses.Add(studentCourse);

                foreach (var courseModule in addedStudent.Cohort.Course.CourseModules)
                {
                    var studentModule = new StudentModule
                    {
                        StudentId = addedStudent.StudentId,
                        ModuleId = courseModule.ModuleId,
                        Mark = null, // Set as null initially
                        Result = "Undefined" // Set as undefined initially
                    };
                    _context.StudentModules.Add(studentModule);

                    // If the module has assessments, associate assessments with the student
                    foreach (var moduleAssessment in courseModule.Module.ModuleAssessments)
                    {
                        var studentAssessment = new StudentAssessment
                        {
                            StudentId = addedStudent.StudentId,
                            AssessmentId = moduleAssessment.AssessmentId,
                            Mark = null // Set as null initially
                        };
                        _context.StudentAssessments.Add(studentAssessment);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());

        }


        [HttpGet("getStudent")]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }


        [HttpGet("getStudent/cohort/{cohortId}")]
        public async Task<ActionResult<List<Student>>> GetStudents(int cohortId)
        {
             var cohort = await _context.Cohort
                .Include(c => c.Students) // Include the Students
                    .ThenInclude(s => s.StudentAssessments) // Include StudentAssessments
                        .ThenInclude(sa => sa.Assessment) // Include Assessments
                .Include(c => c.Students) // Include the Students
                    .ThenInclude(s => s.StudentModules) // Include StudentModules
                .Include(c => c.Students) // Include the Students
                    .ThenInclude(s => s.StudentCourses) // Include StudentCourses
                .FirstOrDefaultAsync(c => c.CohortId == cohortId);

            if (cohort == null)
            {
                return NotFound();
            }

            // Extract students
            var students = cohort.Students.ToList();

            // Map the retrieved data to a DTO or ViewModel to avoid circular reference issues
            var studentsDto = students.Select(student =>
            {
                // Map student details
                var studentDto = new
                {
                    student.StudentId,
                    student.FullName,
                    student.CohortId,
                    // Include assessment marks for each student
                    Assessments = student.StudentAssessments.Select(sa => new
                    {
                        sa.Assessment.Title,
                        sa.Mark
                    }).ToList(),
                    // Include module marks and results for each student
                    Modules = student.StudentModules.Select(sm => new
                    {
                        sm.Module.ModuleId,
                        sm.Module.Title,
                        sm.Mark,
                        sm.Result
                    }).ToList(),
                    // Include course marks and results for each student
                    Courses = student.StudentCourses.Select(sc => new
                    {
                        sc.Course.CourseId,
                        sc.Course.Title,
                        sc.Mark,
                        sc.Result
                    }).ToList()
                };
                return studentDto;
            }).ToList();

            return Ok(studentsDto);
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
            int? totalMarks = 0;
            int numberOfModules = 0;


            var existingModules = _context.StudentModule.Where(sa => sa.StudentId == student.StudentId);


            foreach (var module in existingModules)
            {
                var mark = module.Mark;
                var result = module.Result;

                if (mark != null)
                {
                    // Logic to calculate total marks from course
                    totalMarks += mark ?? 0; 
                }
                numberOfModules++;

            }
            // Calculate the average marks for the module
            if (numberOfModules > 0 && totalMarks != null)
            {
                double averageMark = numberOfModules > 0 ? (double)totalMarks / numberOfModules : 0.0;

                var studentCourse = await _context.StudentCourse
                    .FirstOrDefaultAsync(sp => sp.StudentId == student.StudentId);

                if (studentCourse != null)
                {
                    studentCourse.Mark = (int)Math.Round(averageMark);
                    _context.StudentCourse.Update(studentCourse);
                    await _context.SaveChangesAsync(); 
                }
            }

            var finalResult = await _context.Students
                .Include(c => c.Cohort) // Include the Students
                    .ThenInclude(c => c.Course) // Include the Course
                    .ThenInclude(c => c.CourseModules)
                    .ThenInclude(c => c.Module) 
                    .ThenInclude(c => c.ModuleAssessments) 
                    .ThenInclude(c => c.Assessment) 
                .FirstOrDefaultAsync(c => c.StudentId == id);
            return Ok(finalResult);
        }

        [HttpPost("AddMark/{studentId}/assessments/{assessmentId}")]
        public async Task<ActionResult> AddMarkToAssessment(int studentId, int assessmentId, int mark)
        {
            var student = await _context.Students
                .Include(s => s.StudentAssessments)
                .Include(s => s.StudentCourses)
                .Include(s => s.StudentModules)
                    .ThenInclude(sm => sm.Module)
                        .ThenInclude(m => m.ModuleAssessments)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            var assessment = student.StudentAssessments.FirstOrDefault(sa => sa.AssessmentId == assessmentId);

            if (assessment == null)
            {
                return NotFound("Assessment not found for this student");
            }

            var maxMarkForAssessment = await _context.ModuleAssessment
                .Where(ma => ma.AssessmentId == assessmentId)
                .Select(ma => ma.MaxMark)
                .FirstOrDefaultAsync();

            if (mark > maxMarkForAssessment)
            {
                return BadRequest($"The Maximum mark for this assessment is {maxMarkForAssessment}");
            }

            assessment.Mark = mark;

            await _context.SaveChangesAsync();

            var CalculateModuleMark = 0;

            // Calculate module marks and update results for modules
            foreach (var module in student.StudentModules.Select(cm => cm.Module))
            {
                var moduleAssessments = module.ModuleAssessments;

                if (moduleAssessments != null && moduleAssessments.Any())
                {
                    foreach (var asses in moduleAssessments)
                    {
                        var assesId = asses.AssessmentId;

                        var correspondingAssessment = student.StudentAssessments
                            .FirstOrDefault(sa => sa.AssessmentId == assesId);

                        if (correspondingAssessment != null)
                        {
                            CalculateModuleMark += correspondingAssessment.Mark ?? 0;
                        }
                    }

                    var studentModule = _context.StudentModule
                        .FirstOrDefault(sm => sm.StudentId == student.StudentId && sm.ModuleId == module.ModuleId);

                    if (studentModule == null)
                    {
                        studentModule = new StudentModule
                        {
                            StudentId = student.StudentId,
                            ModuleId = module.ModuleId
                        };
                        _context.StudentModule.Add(studentModule);
                    }

                    studentModule.Mark = CalculateModuleMark;
                    studentModule.Result = CalculateModuleMark >= 50 ? "Pass" : (CalculateModuleMark >= 45 ? "PassCompensation" : "Fail");

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var studentModule = _context.StudentModule
                        .FirstOrDefault(sm => sm.StudentId == student.StudentId && sm.ModuleId == module.ModuleId);

                    if (studentModule != null)
                    {
                        studentModule.Mark = null;
                        studentModule.Result = "Undefined";
                        await _context.SaveChangesAsync();
                    }
                }
            }

            // Calculate course marks and update results for the course
            var course = student.StudentCourses.FirstOrDefault();

            if (course != null)
            {
                var modulesWithMarksCount = student.StudentModules.Count(sm => sm.Mark != null);

                if (modulesWithMarksCount > 0)
                {
                    var courseModulesMarks = student.StudentModules.Sum(sm => sm.Mark ?? 0);
                    var averageMark = (double)courseModulesMarks / modulesWithMarksCount;
                    course.Mark = (int)Math.Round(averageMark);

                    if (course.Mark >= 70)
                    {
                        course.Result = "Distinction";
                    }
                    else if (course.Mark >= 50)
                    {
                        course.Result = "Pass";
                    }
                    else
                    {
                        course.Result = "Fail";
                    }
                }
                else
                {
                    course.Mark = null;
                    course.Result = "Undefined";
                }

                await _context.SaveChangesAsync();
            }

            return Ok("Mark added successfully");
        }

    }
}
