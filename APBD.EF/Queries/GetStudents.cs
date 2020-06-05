using System.Collections;
using System.Collections.Generic;
using APBD.EF.DTO;
using MediatR;

namespace APBD.EF.Queries
{
    public class GetStudents : IRequest<IEnumerable<StudentDto>>
    {
        
    }
}