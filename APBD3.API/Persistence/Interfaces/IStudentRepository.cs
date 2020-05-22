using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APBD3.API.Models;

namespace APBD3.API.Persistence.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> FindById(string id);
        Task<IEnumerable<Enrollment>> FindEnrollments(string id);
        Task<IEnumerable<Student>> FindAll();
        Task Add(Student student);
        Task<bool> Exists(string studentIndex);
        Task SetPassword(string index, string password, string salt, Guid refreshToken);
    }
}