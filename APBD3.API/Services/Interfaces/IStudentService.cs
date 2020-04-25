using System.Collections.Generic;
using System.Threading.Tasks;
using APBD3.API.Requests;
using APBD3.API.ViewModels;

namespace APBD3.API.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentViewModel>> GetAll();
        Task<StudentViewModel> GetById(string studentId);
        Task<IEnumerable<EnrollmentViewModel>> FindStudentEnrollments(string studentId);
        Task CreateStudent(CreateUser command);
    }
}