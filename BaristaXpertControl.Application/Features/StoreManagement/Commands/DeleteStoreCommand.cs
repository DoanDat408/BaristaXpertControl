using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Commands
{
    public class DeleteStoreCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
