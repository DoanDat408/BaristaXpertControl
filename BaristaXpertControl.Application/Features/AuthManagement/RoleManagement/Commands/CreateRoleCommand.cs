using MediatR;
using BaristaXpertControl.Application.Common.Responses;

namespace BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands
{
    public class CreateRoleCommand : IRequest<RoleResponse>
    {
        public string RoleName { get; set; }
    }
}
