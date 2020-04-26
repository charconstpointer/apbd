using System;
using System.Threading.Tasks;
using APBD3.API.Exceptions;
using APBD3.API.Models;
using APBD3.API.Persistence.Interfaces;
using APBD3.API.Services.Interfaces;

namespace APBD3.API.Services
{
    public class StudiesService : IStudiesService
    {
        private readonly IStudiesRepository _studiesRepository;

        public StudiesService(IStudiesRepository studiesRepository)
        {
            _studiesRepository = studiesRepository;
        }

        public Task<Enrollment> EnrollStudent(string indexNumber, string firstName, string lastName, DateTime birthDate,
            string studies)
        {
            var student = new Student(firstName, lastName, indexNumber, birthDate);
            return _studiesRepository.CreateStudentEnrollment(student, studies);
        }

        public async Task<Enrollment> PromoteStudents(string studies, int semester)
        {
            var enrollmentExists = await _studiesRepository.EnrollmentExists(studies, semester);
            if (!enrollmentExists)
            {
                throw new EnrollmentNotFoundException();
            }

            return await _studiesRepository.PromoteStudents(studies, semester);
        }
    }
}