using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using APBD.EF.Data;
using APBD.EF.DTO;
using APBD.EF.Extensions;
using APBD.EF.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APBD.EF.Handlers
{
    public class GetStudentsHandler : IRequestHandler<GetStudents, IEnumerable<StudentDto>>
    {
        private readonly ApdbContext _context;

        public GetStudentsHandler(ApdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDto>> Handle(GetStudents request, CancellationToken cancellationToken)
        {
            var students = await _context.Student.ToListAsync(cancellationToken: cancellationToken);
            return students.AsDto();
        }
    }
}