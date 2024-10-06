using BaristaXpertControl.Application.Common.Responses;
using MediatR;

namespace BaristaXpertControl.Application.Features.StoreManagement.Commands
{
    public class CreateStoreCommand : IRequest<StoreResponse>
    {
        public string Location { get; set; }
    }
}
