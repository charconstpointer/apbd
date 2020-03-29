using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD3.API.Models;
using APBD3.API.Persistence;
using APBD3.API.Requests;
using APBD3.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var students = await _studentRepository.Find(x => true);
            return Ok(new {Students = students, OrderBy = orderBy});
        }

        [HttpGet("{studentId:int}")]
        public async Task<IActionResult> Get(int studentId)
        {
            var student = (await _studentRepository.Find(x => x.Id == studentId)).FirstOrDefault();
            if (student is null)
            {
                return NotFound();
            }

            return Ok(student.ToViewModel());
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