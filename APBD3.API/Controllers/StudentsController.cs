using APBD3.API.Models;
using APBD3.API.Persistence;
using APBD3.API.Requests;
using APBD3.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(string orderBy)
        {
            // var students = await _studentRepository.Find(x => true);
            var students = await _studentRepository.FindAll();
            return Ok(new {Students = students.ToViewModel(), OrderBy = orderBy});
        }

        [HttpGet("{studentId}/enrollments")]
        public async Task<IActionResult> Get(string studentId)
        {
            var enrollments = (await _studentRepository.FindEnrollments(studentId)).ToList();
            if (enrollments.Any())
            {
                return Ok(enrollments.ToViewModel());
            }

            return NotFound();
        }

        [HttpPost("{studentId}")]
        public async Task<IActionResult> GetStudent(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                return BadRequest();
            }

            var student = await _studentRepository.FindById(studentId);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateUser command)
        {
            var student = new Student(command.FirstName, command.LastName, command.IndexName);
            await _studentRepository.Add(student);
            return Created($"/{student.Id}", null);
        }

        [HttpDelete("{studentId:int}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            return Ok(new {Message = $"{studentId} zostal usuniety"});
        }

        [HttpPut("{studentId:int}")]
        public async Task<IActionResult> UpdateStudent(int studentId)
        {
            return Ok(new {Message = $"{studentId} zostal uaktualniony"});
        }
    }
}