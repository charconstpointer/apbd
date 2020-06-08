using System.Linq;
using System.Threading.Tasks;
using APBD.EF.CodeFirst.Commands;
using APBD.EF.CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD.EF.CodeFirst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService _doctorsService;

        public DoctorsController(IDoctorsService doctorsService)
        {
            _doctorsService = doctorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _doctorsService.GetDoctors(new GetDoctorsCommand());
            if (!doctors.Any())
            {
                return NotFound();
            }

            return Ok(doctors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDoctor(int doctorId)
        {
            var doctor = await _doctorsService.GetDoctor(new GetDoctorCommand {DoctorId = doctorId});
            if (doctor is null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorCommand createDoctorCommand)
        {
            await _doctorsService.CreateDoctor(createDoctorCommand);
            return Created("", null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDoctor(int doctorId, UpdateDoctorCommand updateDoctorCommand)
        {
            updateDoctorCommand.DoctorId = doctorId;
            await _doctorsService.UpdateDoctor(updateDoctorCommand);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDoctor(int doctorId)
        {
            await _doctorsService.DeleteDoctor(new DeleteDoctorCommand {DoctorId = doctorId});
            return NoContent();
        }
    }
}