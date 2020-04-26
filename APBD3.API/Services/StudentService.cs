using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD3.API.Models;
using APBD3.API.Persistence;
using APBD3.API.Persistence.Interfaces;
using APBD3.API.Requests;
using APBD3.API.Services.Interfaces;
using APBD3.API.ViewModels;

namespace APBD3.API.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentViewModel>> GetAll()
        {
            var students = await _studentRepository.FindAll();
            return students.ToViewModel();
        }

        public async Task<StudentViewModel> GetById(string studentId)
        {
            var student = await _studentRepository.FindById(studentId);
            if (student is null)
            {
                throw new Exception("Could not find student for a given id");
            }

            return student.ToViewModel();
        }

        public async Task<IEnumerable<EnrollmentViewModel>> FindStudentEnrollments(string studentId)
        {
            var enrollments = (await _studentRepository.FindEnrollments(studentId)).ToList();
            if (enrollments.Any())
            {
                return enrollments.ToViewModel();
            }

            throw new Exception("Could not find any enrollments for a given student");
        }

        public async Task CreateStudent(CreateUser command)
        {
            var student = new Student(command.FirstName, command.LastName, command.IndexName, command.BirthDate,
                command.EnrollmentId);
            await _studentRepository.Add(student);
        }

        public Task<bool> StudendExists(string studentIndex)
        {
            return _studentRepository.Exists(studentIndex);
        }
    }
}