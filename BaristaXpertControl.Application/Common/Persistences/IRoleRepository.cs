using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Common.Persistences
{
    public interface IRoleRepository
    {
        Task<bool> RoleExistsAsync(string roleName);
    }

}
