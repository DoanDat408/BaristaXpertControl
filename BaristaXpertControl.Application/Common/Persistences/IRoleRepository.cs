using BaristaXpertControl.Domain.Entities;

namespace BaristaXpertControl.Application.Common.Persistences
{
    public interface IRoleRepository
    {
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
