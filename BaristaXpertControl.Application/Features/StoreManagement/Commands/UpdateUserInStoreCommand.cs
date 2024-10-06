using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.StoreManagement.Commands
{
    public class UpdateUserInStoreCommand : IRequest<bool>
    {
        public string CurrentUsername { get; set; }
        public string NewUsername { get; set; }
        public string CurrentLocation { get; set; }  
        public string NewLocation { get; set; }
    }
}
