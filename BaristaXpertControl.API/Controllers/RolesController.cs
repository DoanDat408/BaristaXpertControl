using BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands;
using BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaristaXpertControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var result = await _mediator.Send(new GetRoleQuery { RoleId = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleCommand command)
        {
            if (id != command.Id) return BadRequest("Id không khớp với Role cần cập nhật.");
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand { RoleId = id });
            return result ? Ok() : NotFound();
        }
    }
}
