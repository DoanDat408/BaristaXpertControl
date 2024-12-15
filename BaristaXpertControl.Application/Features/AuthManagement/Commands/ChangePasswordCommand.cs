using BaristaXpertControl.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.AuthManagement.Commands
{
    public class ChangePasswordCommand : IRequest<ChangePasswordResponse>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

}
