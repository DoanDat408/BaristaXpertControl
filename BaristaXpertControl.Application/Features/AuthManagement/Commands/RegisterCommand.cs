using BaristaXpertControl.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Features.AuthManagement.Commands
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

}
