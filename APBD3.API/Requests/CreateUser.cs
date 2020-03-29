using System;
using System.ComponentModel.DataAnnotations;

namespace APBD3.API.Requests
{
    public class CreateUser
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string IndexName { get; set; }
        [Required] public DateTime BirthDate { get; set; }
        public int EnrollmentId { get; set; }
    }
}