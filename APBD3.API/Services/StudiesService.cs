using System;
using System.Threading.Tasks;
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

        public async Task EnrollStudent(string indexNumber, string firstName, string lastName, DateTime birthDate,
            string studies)
        {
            var student = new Student(firstName, lastName, indexNumber, birthDate);
            await _studiesRepository.CreateStudentEnrollment(student, studies);
        }
    }
}