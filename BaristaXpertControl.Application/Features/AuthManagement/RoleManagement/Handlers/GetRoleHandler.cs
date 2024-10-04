using AutoMapper;
using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Application.Common.Responses;
using BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.AuthManagement.RoleManagement.Handlers
{
    public class GetRoleHandler : IRequestHandler<GetRoleQuery, RoleResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleResponse> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleByIdAsync(request.RoleId);
            return _mapper.Map<RoleResponse>(role);
        }
    }
}
