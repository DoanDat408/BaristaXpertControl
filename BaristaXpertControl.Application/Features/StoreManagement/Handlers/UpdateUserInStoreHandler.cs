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
    public class UpdateUserInStoreHandler : IRequestHandler<UpdateUserInStoreCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserInStoreHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUserInStoreCommand request, CancellationToken cancellationToken)
        {
            // Lấy store hiện tại của người dùng
            var currentStore = await _unitOfWork.StoreRepository.GetStoreByLocationAsync(request.CurrentLocation);
            if (currentStore == null)
            {
                return false; 
            }

            // Lấy người dùng trong cửa hàng hiện tại
            var existingUser = currentStore.StoreUsers.FirstOrDefault(u => u.Username == request.CurrentUsername);
            if (existingUser == null)
            {
                return false;
            }

            // Kiểm tra nếu username mới giống username hiện tại, bỏ qua cập nhật username
            if (request.NewUsername != request.CurrentUsername)
            {
                existingUser.Username = request.NewUsername;
            }

            // Nếu NewLocation khác với CurrentLocation, thực hiện di chuyển người dùng sang cửa hàng mới
            if (!string.IsNullOrEmpty(request.NewLocation) && request.NewLocation != request.CurrentLocation)
            {
                var newStore = await _unitOfWork.StoreRepository.GetStoreByLocationAsync(request.NewLocation);
                if (newStore == null)
                {
                    return false;
                }

                currentStore.StoreUsers.Remove(existingUser);

                newStore.StoreUsers.Add(new StoreUser
                {
                    Username = request.NewUsername,
                    StoreId = newStore.Id
                });
            }

            await _unitOfWork.CompleteAsync(); 
            return true;
        }
    }
}
