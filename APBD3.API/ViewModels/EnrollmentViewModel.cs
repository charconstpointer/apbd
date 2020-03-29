using System;
using System.Collections.Generic;
using System.Linq;
using APBD3.API.Models;

namespace APBD3.API.ViewModels
{
    public static class EnrollmentMapper
    {
        public static EnrollmentViewModel ToViewModel(this Enrollment enrollment)
        {
            return new EnrollmentViewModel
            {
                IdEnrollment = enrollment.IdEnrollment,
                IdStudy = enrollment.IdStudy,
                Semester = enrollment.Semester,
                StartDate = enrollment.StartDate
            };
        }

        public static IEnumerable<EnrollmentViewModel> ToViewModel(this IEnumerable<Enrollment> source) =>
            source.Select(ToViewModel);
    }

    public class EnrollmentViewModel
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }
    }
}