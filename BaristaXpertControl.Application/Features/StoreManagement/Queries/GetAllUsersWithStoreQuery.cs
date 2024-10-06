using BaristaXpertControl.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Queries
{
    public class GetAllUsersWithStoreQuery : IRequest<List<UserWithStoreResponse>>
    {
    }
}
