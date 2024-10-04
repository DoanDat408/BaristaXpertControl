using AutoMapper;
using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Application.Common.Responses;
using BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Commands;
using BaristaXpertControl.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Handlers
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, RoleResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleEntity = _mapper.Map<Role>(request);
            var createdRole = await _roleRepository.CreateRoleAsync(roleEntity);
            return _mapper.Map<RoleResponse>(createdRole);
        }
    }
}
