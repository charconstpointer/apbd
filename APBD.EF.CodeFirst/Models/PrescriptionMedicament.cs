using System.ComponentModel.DataAnnotations.Schema;

namespace APBD.EF.CodeFirst.Models
{
    [Table("Prescription_Medicament")]
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public Medicament Medicament { get; set; }
        public Prescription Prescription { get; set; }
        public int Dose { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Details { get; set; }
    }
}