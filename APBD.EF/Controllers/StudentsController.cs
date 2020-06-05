using System.Threading.Tasks;
using APBD.EF.Commands;
using APBD.EF.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APBD.EF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
            => Ok(await _mediator.Send(new GetStudents()));

        [HttpPut("{index:int}")]
        public async Task<IActionResult> UpdateStudent(int index, EditStudent editStudent)
        {
            editStudent.Index = $"s{index}";
            await _mediator.Send(editStudent);
            return NoContent();
        }

        [HttpDelete("{index:int}")]
        public async Task<IActionResult> DeleteStudent(int index)
        {
            var studentIndex = $"s{index}";
            await _mediator.Send(new DeleteStudent {Index = studentIndex});
            return NoContent();
        }
    }
}