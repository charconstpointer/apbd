using System.Collections;
using System.Collections.Generic;
using System.Linq;
using APBD.EF.DTO;
using APBD.EF.Models;

namespace APBD.EF.Extensions
{
    public static class StudentExtensions
    {
        public static StudentDto AsDto(this Student student)
            => new StudentDto
            {
                BirthDate = student.BirthDate,
                FirstName = student.FirstName,
                IndexNumber = student.IndexNumber,
                LastName = student.LastName
            };

        public static IEnumerable<StudentDto> AsDto(this IEnumerable<Student> students)
            => students.Select(AsDto);
    }
}