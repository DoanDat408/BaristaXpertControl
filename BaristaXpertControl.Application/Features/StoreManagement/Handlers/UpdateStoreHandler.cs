using BaristaXpertControl.Application.Common.Responses;
using BaristaXpertControl.Application.Features.StoreManagement.Commands;
using BaristaXpertControl.Application.Persistences;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Handlers
{
    public class UpdateStoreHandler : IRequestHandler<UpdateStoreCommand, StoreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStoreHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StoreResponse> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(request.Id);

            if (store == null)
            {
                return null; // Store không tồn tại
            }

            // Cập nhật thông tin Store
            store.Location = request.Location;

            await _unitOfWork.CompleteAsync(); // Lưu thay đổi

            return new StoreResponse
            {
                Id = store.Id,
                Location = store.Location
            };
        }
    }
}
