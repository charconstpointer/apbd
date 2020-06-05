using System;
using APBD.EF.Models;

namespace APBD.EF.DTO
{
    public class StudentDto
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}