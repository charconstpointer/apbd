using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD.EF.CodeFirst.Commands;
using APBD.EF.CodeFirst.Data;
using APBD.EF.CodeFirst.DTO;
using APBD.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.CodeFirst.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly ApbdContext _context;

        public DoctorsService(ApbdContext context)
        {
            _context = context;
        }

        public async Task CreateDoctor(CreateDoctorCommand createDoctorCommand)
        {
            var firstName = createDoctorCommand.FirstName;
            var lastName = createDoctorCommand.LastName;
            var email = createDoctorCommand.Email;
            var doctor = new Doctor(firstName, lastName, email);
            await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctor(UpdateDoctorCommand updateDoctorCommand)
        {
            var doctorId = updateDoctorCommand.DoctorId;
            var updatedFirstName = updateDoctorCommand.FirstName;
            var updatedLastName = updateDoctorCommand.LastName;
            var updatedEmail = updateDoctorCommand.Email;
            var doctor = await FindDoctor(doctorId);

            doctor.FirstName = updatedFirstName;
            doctor.LastName = updatedLastName;
            doctor.Email = updatedEmail;
            await _context.SaveChangesAsync();
        }

        private async Task<Doctor> FindDoctor(int doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == doctorId);
            if (doctor is null)
            {
                throw new ApplicationException("Doc not found 😭😭😭😭");
            }

            return doctor;
        }

        public async Task DeleteDoctor(DeleteDoctorCommand deleteDoctorCommand)
        {
            var doctorId = deleteDoctorCommand.DoctorId;
            var doctor = await FindDoctor(doctorId);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctors(GetDoctorsCommand getDoctorsCommand)
        {
            var doctors = await _context.Doctors
                .Include(d => d.Prescriptions)
                .ThenInclude(p => p.PrescriptionMedicaments)
                .Include(d => d.Prescriptions)
                .ThenInclude(p => p.Patient)
                .ToListAsync();
            return doctors.Select(x => new DoctorDto
            {
                Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, Prescriptions = x.Prescriptions.Select(
                    p => new PrescriptionDto
                    {
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Patient = string.Join(", ", p.Patient.FirstName, p.Patient.LastName),
                        Medicament = p.PrescriptionMedicaments.Select(pm => pm.Medicament.Name)
                    })
            });
        }

        public async Task<DoctorDto> GetDoctor(GetDoctorCommand getDoctorCommand)
        {
            var doctor = await FindDoctor(getDoctorCommand.DoctorId);
            return new DoctorDto
            {
                Email = doctor.Email, FirstName = doctor.FirstName, LastName = doctor.LastName, Prescriptions =
                    doctor.Prescriptions.Select(
                        p => new PrescriptionDto
                        {
                            Date = p.Date,
                            DueDate = p.DueDate,
                            Patient = string.Join(", ", p.Patient.FirstName, p.Patient.LastName),
                            Medicament = p.PrescriptionMedicaments.Select(pm => pm.Medicament.Name)
                        })
            };
        }
    }
}