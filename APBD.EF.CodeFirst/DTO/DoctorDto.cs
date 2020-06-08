using System.Collections.Generic;


namespace APBD.EF.CodeFirst.DTO
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<PrescriptionDto> Prescriptions { get; set; }
    }
}