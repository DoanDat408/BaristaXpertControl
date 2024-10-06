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
    public class AssignUserToStoreHandler : IRequestHandler<AssignUserToStoreCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignUserToStoreHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AssignUserToStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _unitOfWork.StoreRepository.GetStoreByLocationAsync(request.Location);

            if (store == null)
            {
                return false; 
            }

            // Kiểm tra nếu Username đã được gán vào Store này chưa
            var existingUser = store.StoreUsers.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return false; 
            }

            // Thêm Username vào Store
            store.StoreUsers.Add(new StoreUser
            {
                Username = request.Username,
                StoreId = store.Id
            });

            await _unitOfWork.CompleteAsync(); 
            return true;
        }
    }
}
