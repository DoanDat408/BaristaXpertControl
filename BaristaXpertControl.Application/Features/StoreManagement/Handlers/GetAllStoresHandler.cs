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
    public class GetAllStoresHandler : IRequestHandler<GetAllStoresQuery, IEnumerable<StoreResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStoresHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StoreResponse>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
        {
            var stores = await _unitOfWork.StoreRepository.GetAllAsync();

            // Mapping từ Store sang StoreResponse
            var storeResponses = stores.Select(store => new StoreResponse
            {
                Id = store.Id,
                Location = store.Location
            }).ToList();

            return storeResponses;
        }
    }
}
