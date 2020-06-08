using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD.EF.CodeFirst.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key] public int IdPatient { get; set; }
        [Column(TypeName = "varchar(100)")] public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)")] public string LastName { get; set; }
        public DateTime Brithdate { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}