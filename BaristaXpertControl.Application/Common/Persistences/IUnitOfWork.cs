using BaristaXpertControl.Application.Common.Persistences;

namespace BaristaXpertControl.Application.Persistences
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IStoreRepository StoreRepository { get; }
        Task<int> CompleteAsync();
    }
}
