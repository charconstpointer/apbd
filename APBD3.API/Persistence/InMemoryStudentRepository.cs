// using APBD3.API.Models;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
//
// namespace APBD3.API.Persistence
// {
//     public class InMemoryStudentRepository : IStudentRepository
//     {
//         private readonly ICollection<Student> _students;
//
//         public InMemoryStudentRepository(ICollection<Student> students)
//         {
//             _students = students;
//         }
//
//         public async Task Add(Student student)
//         {
//             _students.Add(student);
//         }
//
//         public async Task<IEnumerable<Student>> Find(Func<Student, bool> predicate)
//         {
//             return _students.Where(predicate);
//         }
//
//         public async Task Remove(int id)
//         {
//             var student = _students.FirstOrDefault(x => x.Id == id);
//             if (student is null)
//             {
//                 throw new Exception("Student not found");
//             }
//
//             _students.Remove(student);
//         }
//     }
// }