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
            var module = new Module();
            module.ModuleCode = moduleDto.Code;
            module.Title = moduleDto.Title;

            _context.Module.Add(module);
            await _context.SaveChangesAsync();

            return Ok(await _context.Module.ToListAsync());
        }

        [HttpPost("{moduleId}/{assesmentId}")]
        public async Task AsssignAssement(int moduleId, int assessmentId)
        {
            var module = await _context.Module.FindAsync(moduleId);
            if (module.Assessments.Count < 2)
            {
                module.Assessments.Add(_context.Assessment.Find(assessmentId));
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Module>>> GetModules()
        {
            return Ok(await _context.Module.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var module = await _context.Module.FindAsync(id);
            if (module == null)
            {
                return BadRequest("student not found");
            }
            return Ok(module);
        }
    }
}
