using System;
using APBD.EF.Models;

namespace APBD.EF.DTO
{
    public class EnrollmentDto
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }
    }


    public static class EnrollmentExtensions
    {
        public static EnrollmentDto AsDto(this Enrollment enrollment)
            => new EnrollmentDto
            {
                Semester = enrollment.Semester!.Value,
                IdEnrollment = enrollment.IdEnrollment,
                IdStudy = enrollment.IdStudy!.Value,
                StartDate = enrollment.StartDate!.Value
            };
    }
}