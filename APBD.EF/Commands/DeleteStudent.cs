using MediatR;

namespace APBD.EF.Commands
{
    public class DeleteStudent : IRequest
    {
        public string Index { get; set; }
    }
}