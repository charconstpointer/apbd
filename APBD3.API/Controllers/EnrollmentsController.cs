using System.Threading.Tasks;
using APBD3.API.Requests;
using APBD3.API.Services.Interfaces;
using APBD3.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APBD3.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IStudiesService _studiesService;

        public EnrollmentsController(IStudiesService studiesService)
        {
            _studiesService = studiesService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateStudentEnrollment command)
        {
            if (!ModelState.IsValid) return BadRequest();
            var enrollment = await _studiesService.EnrollStudent(command.IndexNumber, command.FirstName, command.LastName,
                command.BirthDate, command.Studies);
            return Created("", enrollment.ToViewModel());
        }

        [HttpPost("promotions")]
        public async Task<IActionResult> Promote(PromoteStudents command)
        {
            if(!ModelState.IsValid) return BadRequest();
            var enrollment = await _studiesService.PromoteStudents(command.Studies, command.Semester);
            return Created("",enrollment.ToViewModel());
        }
    }
}