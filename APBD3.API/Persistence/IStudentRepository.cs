using APBD3.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBD3.API.Persistence
{
    public interface IStudentRepository
    {
        Task Add(Student student);
        Task<IEnumerable<Student>> Find(Func<Student, bool> predicate);
        Task Remove(int id);
    }
}