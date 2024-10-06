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
    public class DeleteStoreHandler : IRequestHandler<DeleteStoreCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStoreHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(request.Id);

            if (store == null)
            {
                return false; 
            }

            await _unitOfWork.StoreRepository.DeleteAsync(store);
            await _unitOfWork.CompleteAsync(); 

            return true;
        }
    }
}
