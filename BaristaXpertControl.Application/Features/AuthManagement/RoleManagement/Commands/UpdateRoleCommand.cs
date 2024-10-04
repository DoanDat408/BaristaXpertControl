using MediatR;
using BaristaXpertControl.Application.Common.Responses;

namespace BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands
{
    public class UpdateRoleCommand : IRequest<RoleResponse>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
