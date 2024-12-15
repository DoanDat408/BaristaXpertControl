using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Common.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool RequiresPasswordChange { get; set; }
    }

}
