using System;
using System.Collections.Generic;
using APBD.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.CodeFirst.Data
{
    public class ApbdContext : DbContext
    {
        public ApbdContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription>()
                .HasOne(x => x.Doctor)
                .WithMany(x => x.Prescriptions)
                .HasForeignKey(x => x.IdDoctor);

            modelBuilder.Entity<Prescription>()
                .HasOne(x => x.Patient)
                .WithMany(x => x.Prescriptions)
                .HasForeignKey(x => x.IdPatient);
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(x => x.Medicament)
                .WithMany(x => x.PrescriptionMedicaments)
                .HasForeignKey(x => x.IdMedicament);
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(x => x.Prescription)
                .WithMany(x => x.PrescriptionMedicaments)
                .HasForeignKey(x => x.IdPrescription);
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasKey(x => new {x.IdMedicament, x.IdPrescription});
            var patient = new Patient
            {
                IdPatient = 1, Brithdate = DateTime.Now.AddYears(-10), FirstName = "Bar", LastName = "Farski"
            };
            var doctor = new Doctor
            {
                Email = "fds@dsa.com", FirstName = "Doctor", LastName = "Disrespect", IdDoctor = 1
            };
            var prescription = new Prescription
            {
                Date = DateTime.UtcNow, IdPrescription = 1, DueDate = DateTime.Now.AddMilliseconds(1), IdDoctor = 1,
                IdPatient = 1
            };
            modelBuilder.Entity<Patient>().HasData(patient);
            modelBuilder.Entity<Doctor>().HasData(doctor);
            modelBuilder.Entity<Doctor>().HasMany(x => x.Prescriptions)
                .WithOne(x => x.Doctor).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Prescription>().HasData(prescription);
            var medicament = new Medicament
                {Description = "wanna meet god?", Name = "🎢", Type = "good stuff", IdMedicament = 1};
            modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
            {
                medicament
            });
            var pm = new PrescriptionMedicament
            {
                Details = "Skada", Dose = 44, IdMedicament = medicament.IdMedicament, IdPrescription = prescription.IdPrescription
            };
            modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
            {
                pm
            });
        }
    }
}