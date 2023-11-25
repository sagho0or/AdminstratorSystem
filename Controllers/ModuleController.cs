using AdministratorSystem.Data;
using AdministratorSystem.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministratorSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly DataContext _context;
        public ModuleController(DataContext context)
        {
            _context = context;
        }
        

        [HttpPost]
        public async Task<ActionResult<List<Module>>> AddModule(ModuleDto moduleDto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid based on data annotations, return a BadRequest
                return BadRequest(ModelState);
            }
            var module = new Module();
            module.ModuleId = GenerateUniqueModuleId();
            module.Title = moduleDto.Title;

            _context.Module.Add(module);
            await _context.SaveChangesAsync();

            return Ok(await _context.Module.ToListAsync());
        }

        [HttpPost("assign-assesment")]
        public async Task<ActionResult<Module>> AsssignAssesment(ModuleAssessmentDto moduleAssessmentDto)
        {

            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid based on data annotations, return a BadRequest
                return BadRequest(ModelState);
            }

            var module = await _context.Module
                .Include(m => m.ModuleAssessments)
                .FirstOrDefaultAsync(m => m.ModuleId == moduleAssessmentDto.ModuleId);

            if (module == null)
            {
                return NotFound("Module not found");
            }

            // Calculate the sum of existing maximum marks for assessments in this module
            int currentSumOfMaxMarks = module.ModuleAssessments.Sum(ma => ma.MaxMark);

            // Check if adding the new assessment will exceed the maximum limit of 100
            if (currentSumOfMaxMarks + moduleAssessmentDto.MaxMark > 100)
            {
                return BadRequest("Adding this assessment would exceed the maximum limit of 100 marks for this module");
            }


            var moduleAssessment = new ModuleAssessment
            {
                ModuleId = moduleAssessmentDto.ModuleId,
                AssessmentId = moduleAssessmentDto.AssessmentId,
                MaxMark = moduleAssessmentDto.MaxMark
            };

            _context.ModuleAssessment.Add(moduleAssessment);
            await _context.SaveChangesAsync();


            return Ok(await _context.ModuleAssessment.ToListAsync());

        }


        [HttpGet]
        public async Task<ActionResult<List<Module>>> GetModules()
        {
            return Ok(await _context.Module.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var module = await _context.Module
                .Include(c => c.ModuleAssessments) // Include the CourseModules
                    .ThenInclude(cm => cm.Assessment) // Include the Module entity in CourseModules
                .FirstOrDefaultAsync(c => c.ModuleId == id);
            if (module == null)
            {
                return BadRequest("student not found");
            }
            return Ok(module);
        }
        private int GenerateUniqueModuleId()
        {
            Random random = new Random();
            int maxAttempts = 10000; 
            int uniqueCode;

            for (int i = 0; i < maxAttempts; i++)
            {
                uniqueCode = random.Next(10000, 99999); 

               
                var existingModule = _context.Module.FirstOrDefault(m => m.ModuleId == uniqueCode);

                if (existingModule == null)
                {
                    return uniqueCode;
                }
            }

            throw new Exception("Failed to generate a unique ModuleId.");
        }
    }
}
