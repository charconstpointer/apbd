using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD3.API.Persistence;
using APBD3.API.ViewModels;

namespace APBD3.API.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentViewModel>> GetAll();
        Task<StudentViewModel> GetById(string studentId);
        Task<IEnumerable<EnrollmentViewModel>> FindStudentEnrollments(string studentId);
    }

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
    }
}