using AdministratorSystem.Data;
using AdministratorSystem.Migrations;
using AdministratorSystem.NewFolder;
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
                StudentId = int.Parse($"{cohort.AcademicYear}{uniqueDigits}")
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
                                .ThenInclude(m => m.Assessment)
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
                            Module = moduleAssessment.Module,
                            ModuleId = moduleAssessment.ModuleId,
                            Mark = null // Set as null initially
                        };
                        _context.StudentAssessments.Add(studentAssessment);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(addedStudent);

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
                .FirstOrDefaultAsync(c => c.CohortId == cohortId);

            if (cohort == null)
            {
                return NotFound();
            }

            return Ok(cohort);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var isStudent = await _context.Students.FindAsync(id);

            if (isStudent == null)
            {
                return BadRequest("student not found");
            }

            var student = await _context.Students
                .Include(s => s.Cohort)
                    .ThenInclude(m => m.Course)
                .Include(s => s.StudentCourses)
                .Include(s => s.StudentModules)
                    .ThenInclude(sm => sm.Module)
                    .ThenInclude(m => m.ModuleAssessments)
                    .ThenInclude(m => m.Assessment)
                .Include(s => s.StudentAssessments)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            var modulesForCourse = new List<StudentModuleDto>();


            // Calculate module marks and update results for modules
            foreach (var module in student.StudentModules.Select(cm => cm.Module))
            {
                var moduleAssessments = module.ModuleAssessments;
                var moduleAssessmentsDto = new List<StudentAssessmentDto>();

                if (moduleAssessments != null && moduleAssessments.Any())
                {
                    foreach (var asses in moduleAssessments)
                    {
                        var assesId = asses.AssessmentId;

                        var correspondingAssessment = asses.Assessment.StudentAssessments.FirstOrDefault(sm => sm.ModuleId == module.ModuleId);

                        if (correspondingAssessment != null)
                        {

                            moduleAssessmentsDto.Add(new StudentAssessmentDto
                            {
                                AssessmentId = correspondingAssessment.AssessmentId,
                                Title = asses.Assessment.Title,
                                Mark = correspondingAssessment.Mark
                            });
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

                    var studentModuleDto = new StudentModuleDto
                    {
                        ModuleId = module.ModuleId,
                        Mark = (int)studentModule.Mark,
                        Result = studentModule.Result,
                        Title = module.Title,
                        Assessments = moduleAssessmentsDto
                    };

                    modulesForCourse.Add(studentModuleDto);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var studentModule = _context.StudentModule
                        .FirstOrDefault(sm => sm.StudentId == student.StudentId && sm.ModuleId == module.ModuleId);

                    if (studentModule != null)
                    {

                        var studentModuleDto = new StudentModuleDto
                        {
                            ModuleId = module.ModuleId,
                            Mark = (int)studentModule.Mark,
                            Result = studentModule.Result,
                            Title = module.Title,
                            Assessments = moduleAssessmentsDto
                        };

                        modulesForCourse.Add(studentModuleDto);
                    }


                }
            }
            var course = student.StudentCourses.FirstOrDefault();
            var finalResult = new StudentDto
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Course = new StudentCourseDto
                {
                    CourseId = student.Cohort.Course.CourseId,
                    Title = student.Cohort.Course.Title,
                    Mark = course.Mark,
                    Result = course.Result,
                    Modules = modulesForCourse
                }
            };


            return Ok(finalResult);
        }

        [HttpPost("AddMark/{studentId}/assessments/{assessmentId}")]
        public async Task<ActionResult> AddMarkToAssessment(int studentId, int assessmentId, int moduleId, int mark)
        {
            var student = await _context.Students
                .Include(s => s.Cohort)
                    .ThenInclude(m => m.Course)
                .Include(s => s.StudentCourses)
                .Include(s => s.StudentModules)
                    .ThenInclude(sm => sm.Module)
                    .ThenInclude(m => m.ModuleAssessments)
                    .ThenInclude(m => m.Assessment)
                .Include(s => s.StudentAssessments)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);


            if (student == null)
            {
                return NotFound("Student not found");
            }
            var specificModule = student.StudentModules.FirstOrDefault(sm => sm.ModuleId == moduleId)?.Module;

            if (specificModule == null)
            {
                return BadRequest("Student is not associated with the specified module");
            }
            var assessment = student.StudentAssessments.FirstOrDefault(sa => sa.AssessmentId == assessmentId);

            if (assessment == null)
            {
                return NotFound("Assessment not found for this student");
            }


            var moduleAssessment = specificModule.ModuleAssessments.FirstOrDefault(ma => ma.AssessmentId == assessmentId);

            if (moduleAssessment == null)
            {
                return BadRequest("Assessment not found for this module");
            }

            var maxMarkForAssessment = moduleAssessment.MaxMark;

            if (mark > maxMarkForAssessment)
            {
                return BadRequest($"The Maximum mark for this assessment in this module is {maxMarkForAssessment}");
            }

            moduleAssessment.Assessment.StudentAssessments.FirstOrDefault(sm => sm.AssessmentId == assessmentId && sm.ModuleId == moduleId).Mark = mark;


            await _context.SaveChangesAsync();

            var modulesForCourse = new List<StudentModuleDto>();
            

            // Calculate module marks and update results for modules
            foreach (var module in student.StudentModules.Select(cm => cm.Module))
            {
                var CalculateModuleMark = 0;
                var moduleAssessments = module.ModuleAssessments;
                var moduleAssessmentsDto = new List<StudentAssessmentDto>();

                if (moduleAssessments != null && moduleAssessments.Any())
                {
                    foreach (var asses in moduleAssessments)
                    {
                        var assesId = asses.AssessmentId;

                        var correspondingAssessment = asses.Assessment.StudentAssessments.FirstOrDefault(sm => sm.ModuleId == module.ModuleId);

                        if (correspondingAssessment != null)
                        {
                            CalculateModuleMark += correspondingAssessment.Mark ?? 0;

                            moduleAssessmentsDto.Add(new StudentAssessmentDto
                            {
                                AssessmentId = correspondingAssessment.AssessmentId,
                                Title = asses.Assessment.Title,
                                Mark = correspondingAssessment.Mark
                            });
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

                    var studentModuleDto = new StudentModuleDto
                    {
                        ModuleId = module.ModuleId,
                        Mark = (int)studentModule.Mark,
                        Result = studentModule.Result,
                        Title = module.Title,
                        Assessments = moduleAssessmentsDto
                    };

                    modulesForCourse.Add(studentModuleDto);

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

           
            var finalResult = new StudentDto
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Course = new StudentCourseDto
                {
                    CourseId = course.CourseId,
                    Title = student.Cohort.Course.Title,
                    Mark = course.Mark,
                    Result = course.Result,
                    Modules = modulesForCourse
                }
            };


            return Ok(finalResult);
        }

    }
}
