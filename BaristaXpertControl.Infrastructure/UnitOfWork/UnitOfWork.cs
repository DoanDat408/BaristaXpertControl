using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Application.Persistences;
using BaristaXpertControl.Infrastructure.Data;
using BaristaXpertControl.Infrastructure.Repositories;
using System;

namespace BaristaXpertControl.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IStoreRepository _storeRepository;
        public IStoreRepository StoreRepository
        {
            get
            {
                return _storeRepository ??= new StoreRepository(_context); // Lazy initialization
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
