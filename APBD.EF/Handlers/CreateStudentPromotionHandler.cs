using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APBD.EF.Commands;
using APBD.EF.Data;
using APBD.EF.DTO;
using APBD3.API.Exceptions;
using APBD3.API.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.Handlers
{
    public class CreateStudentPromotionHandler : IRequestHandler<CreateStudentPromotion, EnrollmentDto>
    {
        private readonly ApdbContext _context;

        public CreateStudentPromotionHandler(ApdbContext context)
        {
            _context = context;
        }

        public async Task<EnrollmentDto> Handle(CreateStudentPromotion request,
            CancellationToken cancellationToken)
        {
            var enrollmentExists = _context.Enrollment.Any(e =>
                e.IdStudyNavigation.Name == request.Studies && e.Semester == request.Semester);
            if (!enrollmentExists)
            {
                throw new EnrollmentNotFoundException();
            }

            var idOut = new SqlParameter("updatedEnrollmentId", -1) {Direction = ParameterDirection.Output};
            await _context.Database.ExecuteSqlRawAsync("sp_promote_students @studies, @semester, @updatedEnrollmentId",
                new SqlParameter("studies", request.Semester),
                new SqlParameter("semester", request.Semester),
                idOut
            );
            if (!int.TryParse(idOut.Value.ToString(), out var id))
            {
                throw new Exception("Enrollment not found");
            }

            var enrollment =
                await _context.Enrollment.FirstOrDefaultAsync(e => e.IdEnrollment == id, cancellationToken);
            return enrollment.AsDto();
        }
    }
}