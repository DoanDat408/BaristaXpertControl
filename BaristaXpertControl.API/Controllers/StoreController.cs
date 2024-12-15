using BaristaXpertControl.Application.Features.StoreManagement.Commands;
using BaristaXpertControl.Application.Features.StoreManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaristaXpertControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // API để tạo mới Store
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result); // Trả về StoreResponse
        }

        // API để lấy tất cả các Store
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStores()
        {
            var result = await _mediator.Send(new GetAllStoresQuery());
            return Ok(result); // Trả về danh sách StoreResponse
        }

        // API để lấy thông tin của một Store theo Id
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            var result = await _mediator.Send(new GetStoreByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound($"Store with ID {id} not found.");
            }
            return Ok(result); // Trả về StoreResponse
        }

        // API để cập nhật Store
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] UpdateStoreCommand command)
        {
            command.Id = id; // Gán Id cho command
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound($"Store with ID {id} not found.");
            }
            return Ok(result); // Trả về StoreResponse đã được cập nhật
        }

        // API để xóa Store
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _mediator.Send(new DeleteStoreCommand { Id = id });
            if (!result)
            {
                return NotFound($"Store with ID {id} not found.");
            }
            return Ok($"Store with ID {id} deleted successfully.");
        }

        // API để gán Username vào Store dựa trên Location
        [HttpPost("assign-user-to-store")]
        public async Task<IActionResult> AssignUserToStore([FromBody] AssignUserToStoreCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest($"Failed to assign user '{command.Username}' to store at location '{command.Location}'.");
            }
            return Ok($"User '{command.Username}' assigned to store at location '{command.Location}'.");
        }

        // Cập nhật người dùng trong cửa hàng
        [HttpPut("update-user-in-store")]
        public async Task<IActionResult> UpdateUserInStore([FromBody] UpdateUserInStoreCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest($"Failed to update user '{command.CurrentUsername}' in store at location '{command.CurrentLocation}'.");
            }
            return Ok($"User '{command.CurrentUsername}' updated to '{command.NewUsername}' and moved to store at location '{command.NewLocation}'.");
        }


        // Xóa người dùng khỏi cửa hàng
        [HttpDelete("delete-user-from-store")]
        public async Task<IActionResult> DeleteUserFromStore([FromBody] DeleteUserFromStoreCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound($"User '{command.Username}' not found in store at location '{command.Location}'.");
            }
            return Ok($"User '{command.Username}' removed from store at location '{command.Location}'.");
        }

        // API để lấy tất cả người dùng và vị trí của họ
        [HttpGet("get-all-users-with-store")]
        public async Task<IActionResult> GetAllUsersWithStore()
        {
            var result = await _mediator.Send(new GetAllUsersWithStoreQuery());
            return Ok(result); 
        }
    }
}
