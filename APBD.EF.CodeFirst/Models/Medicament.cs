using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD.EF.CodeFirst.Models
{
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        [Column(TypeName = "varchar(100)")] public string Name { get; set; }
        [Column(TypeName = "varchar(100)")] public string Description { get; set; }
        [Column(TypeName = "varchar(100)")] public string Type { get; set; }
        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}