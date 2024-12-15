using BaristaXpertControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Common.Persistences
{
    public interface IUserRepository
    {
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByUsernameAsync(string username);
        Task<bool> AssignRoleAsync(ApplicationUser user, string role);
        Task<bool> UpdatePasswordAsync(ApplicationUser user, string newPassword);
    }


}
