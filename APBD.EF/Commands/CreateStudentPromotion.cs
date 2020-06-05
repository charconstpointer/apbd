using MediatR;

namespace APBD.EF.Commands
{
    public class CreateStudentPromotion : IRequest
    {
        public string Studies { get; set; }
        public int Semester { get; set; }
    }
}