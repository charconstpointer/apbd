using APBD3.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBD3.API.Persistence
{
    public interface IStudentRepository
    {
        Task<Student> FindById(string id);
        Task<IEnumerable<Enrollment>> FindEnrollments(string id);
        Task<IEnumerable<Student>> FindAll();
        Task Add(Student student);
    }
}