using BaristaXpertControl.Application.Common.Responses;
using BaristaXpertControl.Application.Features.StoreManagement.Queries;
using BaristaXpertControl.Application.Persistences;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Handlers
{
    public class GetAllUsersWithStoreHandler : IRequestHandler<GetAllUsersWithStoreQuery, List<UserWithStoreResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersWithStoreHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserWithStoreResponse>> Handle(GetAllUsersWithStoreQuery request, CancellationToken cancellationToken)
        {
            var stores = await _unitOfWork.StoreRepository.GetAllAsync();

            // Lấy danh sách tất cả người dùng cùng với vị trí của cửa hàng mà họ thuộc
            var usersWithStores = stores
                .SelectMany(store => store.StoreUsers.Select(user => new UserWithStoreResponse
                {
                    Username = user.Username,
                    Location = store.Location
                }))
                .ToList();

            return usersWithStores;
        }
    }
}
