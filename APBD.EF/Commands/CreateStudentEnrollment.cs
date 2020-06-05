using System;
using MediatR;

namespace APBD.EF.Commands
{
    public class CreateStudentEnrollment : IRequest
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Studies { get; set; }
    }
}