using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Commands
{
    public class AssignUserToStoreCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Location { get; set; }
    }
}
