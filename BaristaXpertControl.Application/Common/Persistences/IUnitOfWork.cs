using BaristaXpertControl.Application.Common.Persistences;

namespace BaristaXpertControl.Application.Persistences
{
    public interface IUnitOfWork
    {
        IStoreRepository StoreRepository { get; }
        Task<int> CompleteAsync();
        void Dispose();
    }
}
