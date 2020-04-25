using System;
using System.ComponentModel.DataAnnotations;

namespace APBD3.API.Requests
{
    public class CreateStudentEnrollment
    {
        [Required] public string IndexNumber { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public DateTime BirthDate { get; set; }
        [Required] public string Studies { get; set; }
    }
}