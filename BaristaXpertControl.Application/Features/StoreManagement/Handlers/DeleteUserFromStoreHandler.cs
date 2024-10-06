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
    public class DeleteUserFromStoreHandler : IRequestHandler<DeleteUserFromStoreCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserFromStoreHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserFromStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _unitOfWork.StoreRepository.GetStoreByLocationAsync(request.Location);

            if (store == null)
                return false; 

            var existingUser = store.StoreUsers.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser == null)
                return false; 
         
            store.StoreUsers.Remove(existingUser);
            await _unitOfWork.CompleteAsync(); 

            return true;
        }
    }
}
