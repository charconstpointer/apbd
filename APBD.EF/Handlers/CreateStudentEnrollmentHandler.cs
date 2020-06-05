using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APBD.EF.Commands;
using APBD.EF.Data;
using APBD.EF.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.Handlers
{
    public class CreateStudentEnrollmentHandler : IRequestHandler<CreateStudentEnrollment>
    {
        private readonly ApdbContext _context;

        public CreateStudentEnrollmentHandler(ApdbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateStudentEnrollment request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                FirstName = request.FirstName, LastName = request.LastName, IndexNumber = request.IndexNumber,
                BirthDate = request.BirthDate
            };
            await _context.Database.BeginTransactionAsync(cancellationToken);
            var studies = await _context.Studies.FirstOrDefaultAsync(s => s.Name == request.Studies,
                cancellationToken);
            if (studies is null)
            {
                throw new Exception("Provided studies no dont exist");
            }

            var firstSemesterEnrollments = studies.Enrollment.Where(e => e.Semester == 1).ToList();
            if (!firstSemesterEnrollments.Any())
            {
                var enrollment = new Enrollment
                {
                    Semester = 1,
                    IdStudyNavigation = studies,
                    StartDate = DateTime.UtcNow
                };
                await _context.Enrollment.AddAsync(enrollment, cancellationToken);
            }

            await _context.Student.AddAsync(student, cancellationToken);
            _context.Database.CommitTransaction();
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}