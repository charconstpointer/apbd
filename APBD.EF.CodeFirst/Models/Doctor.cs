using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD.EF.CodeFirst.Models
{
    public class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }
        [Column(TypeName = "varchar(100)")] public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)")] public string LastName { get; set; }
        [Column(TypeName = "varchar(100)")] public string Email { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }

        public Doctor()
        {
            
        }
        public Doctor( string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}