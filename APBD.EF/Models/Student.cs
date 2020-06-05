using System;

namespace APBD.EF.Models
{
    public class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? IdEnrollement { get; set; }

        public virtual Enrollment IdEnrollementNavigation { get; set; }
    }
}
