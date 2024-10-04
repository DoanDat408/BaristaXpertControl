using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Handlers
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.DeleteRoleAsync(request.RoleId);
        }
    }
}
