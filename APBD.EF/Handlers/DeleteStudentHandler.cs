using System;
using System.Threading;
using System.Threading.Tasks;
using APBD.EF.Commands;
using APBD.EF.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.Handlers
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudent>
    {
        private readonly ApdbContext _context;

        public DeleteStudentHandler(ApdbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStudent request, CancellationToken cancellationToken)
        {
            var student = await _context.Student.FirstOrDefaultAsync(s => s.IndexNumber == request.Index,
                cancellationToken: cancellationToken);
            if (student is null)
            {
                throw new Exception("Student not found");
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}