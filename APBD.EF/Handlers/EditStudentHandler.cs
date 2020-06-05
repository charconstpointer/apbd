using System;
using System.Threading;
using System.Threading.Tasks;
using APBD.EF.Commands;
using APBD.EF.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.Handlers
{
    public class EditStudentHandler : IRequestHandler<EditStudent>
    {
        private readonly ApdbContext _context;

        public EditStudentHandler(ApdbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditStudent request, CancellationToken cancellationToken)
        {
            var student = await _context.Student.FirstOrDefaultAsync(s => s.IndexNumber == request.Index, cancellationToken: cancellationToken);
            if (student is null)
            {
                throw new Exception("Student not found");
            }

            if (!string.IsNullOrEmpty(request.FirstName)) student.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName)) student.LastName = request.LastName;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}