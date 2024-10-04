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
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, RoleResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleEntity = await _roleRepository.GetRoleByIdAsync(request.Id);
            if (roleEntity == null)
            {
                return null; // Handle trường hợp role không tồn tại
            }

            roleEntity.RoleName = request.RoleName;
            var updatedRole = await _roleRepository.UpdateRoleAsync(roleEntity);
            return _mapper.Map<RoleResponse>(updatedRole);
        }
    }
}
