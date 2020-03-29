using APBD3.API.Models;
using APBD3.API.Persistence;
using APBD3.API.Requests;
using APBD3.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APBD3.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;

        public StudentsController(IStudentRepository studentRepository, IStudentService studentService)
        {
            _studentRepository = studentRepository;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(string orderBy)
        {
            return Ok(new {Studens = await _studentService.GetAll(), OrderBy = orderBy});
        }

        [HttpGet("{studentId}/enrollments")]
        public async Task<IActionResult> Get(string studentId)
        {
            return Ok(await _studentService.FindStudentEnrollments(studentId));
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudent(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                return BadRequest();
            }

            var student = await _studentService.GetById(studentId);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateUser command)
        {
            await _studentService.CreateStudent(command);
            return Created("", null);
        }

        [HttpDelete("{studentId:int}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            return Ok(new {Message = "Usuwanie ukończone"});
        }

        [HttpPut("{studentId:int}")]
        public async Task<IActionResult> UpdateStudent(int studentId)
        {
            return Ok(new {Message = "Aktualizacja dokończona"});
        }
    }
}