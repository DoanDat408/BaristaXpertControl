using MediatR;
using BaristaXpertControl.Application.Common.Responses;

namespace BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Queries
{
    public class GetRoleQuery : IRequest<RoleResponse>
    {
        public int RoleId { get; set; }
    }
}
