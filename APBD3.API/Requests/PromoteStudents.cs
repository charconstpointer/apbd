using System.ComponentModel.DataAnnotations;

namespace APBD3.API.Requests
{
    public class PromoteStudents
    {
        [Required]
        public string Studies { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}