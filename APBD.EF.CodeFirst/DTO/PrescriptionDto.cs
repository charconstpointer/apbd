using System;
using System.Collections.Generic;

namespace APBD.EF.CodeFirst.DTO
{
    public class PrescriptionDto
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string Patient { get; set; }
        public IEnumerable<string> Medicament { get; set; }
    }
}