using System.Collections.Generic;
using System.Linq;
using APBD3.API.Models;

namespace APBD3.API.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexName { get; set; }
    }

    public static class Mapper
    {
        public static StudentViewModel ToViewModel(this Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                IndexName = student.IndexName
            };
        }

        public static IEnumerable<StudentViewModel> ToViewModel(this IEnumerable<Student> students) =>
            students.Select(ToViewModel);
    }
}