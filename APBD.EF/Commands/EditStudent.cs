using MediatR;

namespace APBD.EF.Commands
{
    public class EditStudent : IRequest
    {
        public string Index { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}