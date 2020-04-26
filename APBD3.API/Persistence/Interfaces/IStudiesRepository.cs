using System;
using System.Threading.Tasks;
using APBD3.API.Models;

namespace APBD3.API.Persistence.Interfaces
{
    public interface IStudiesRepository
    {
        Task<Enrollment> CreateStudentEnrollment(Student student, string studiesName);
        Task<bool> EnrollmentExists(string studies, int semester);
        Task<Enrollment> PromoteStudents(string studies, int semester);
    }
}