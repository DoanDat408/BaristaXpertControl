namespace BaristaXpertControl.Application.Persistences
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        void Dispose();
    }
}
