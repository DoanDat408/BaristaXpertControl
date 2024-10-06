using BaristaXpertControl.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Queries
{
    public class GetStoreByIdQuery : IRequest<StoreResponse>
    {
        public int Id { get; set; }
    }
}
