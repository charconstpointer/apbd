using System.Threading.Tasks;
using APBD.EF.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APBD.EF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudent(CreateStudentEnrollment createStudentEnrollment)
            => Created("", await _mediator.Send(createStudentEnrollment));

        [HttpPost("promotions")]
        public async Task<IActionResult> Promote(CreateStudentPromotion createStudentPromotion)
            =>  Created("", await _mediator.Send(createStudentPromotion));
    }
}