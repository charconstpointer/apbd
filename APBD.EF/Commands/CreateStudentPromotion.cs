using APBD.EF.DTO;
using APBD3.API.ViewModels;
using MediatR;

namespace APBD.EF.Commands
{
    public class CreateStudentPromotion : IRequest<EnrollmentDto>
    {
        public string Studies { get; set; }
        public int Semester { get; set; }
    }
}