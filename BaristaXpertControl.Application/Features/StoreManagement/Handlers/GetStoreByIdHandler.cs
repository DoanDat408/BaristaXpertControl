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
    public class GetStoreByIdHandler : IRequestHandler<GetStoreByIdQuery, StoreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStoreByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StoreResponse> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(request.Id);

            if (store == null)
            {
                return null; // Trả về null nếu không tìm thấy Store
            }

            return new StoreResponse
            {
                Id = store.Id,
                Location = store.Location
            };
        }
    }
}
