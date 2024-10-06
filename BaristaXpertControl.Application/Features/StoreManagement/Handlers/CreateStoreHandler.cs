using BaristaXpertControl.Application.Common.Responses;
using BaristaXpertControl.Application.Features.StoreManagement.Commands;
using BaristaXpertControl.Application.Persistences;
using BaristaXpertControl.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Handlers
{
    public class CreateStoreHandler : IRequestHandler<CreateStoreCommand, StoreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStoreHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StoreResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = new Store
            {
                Location = request.Location
            };

            var result = await _unitOfWork.StoreRepository.AddAsync(store);

            return new StoreResponse
            {
                Id = result.Id,
                Location = result.Location
            };
        }
    }
}
