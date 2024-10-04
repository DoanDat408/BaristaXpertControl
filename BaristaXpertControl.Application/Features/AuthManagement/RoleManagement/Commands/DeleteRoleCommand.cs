using MediatR;

namespace BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public int RoleId { get; set; }
    }
}
