using System.Linq;
using System.Threading.Tasks;
using APBD.EF.CodeFirst.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.CodeFirst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private readonly ApbdContext _context;

        public AppController(ApbdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var patient = await _context.Patients.Include(x => x.Prescriptions).ThenInclude(x => x.Doctor)
                .FirstOrDefaultAsync();
            var prescriptions = patient.Prescriptions.ToList();
            return Ok(new
            {
                Patient = patient.FirstName,
                Prescriptions = prescriptions.Select(p => new {p.Date, p.DueDate, p.Doctor.FirstName})
            });
        }
    }
}