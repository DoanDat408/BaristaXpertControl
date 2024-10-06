using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Domain.Entities;
using BaristaXpertControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Store> AddAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> GetByIdAsync(int id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Stores
                .Include(s => s.StoreUsers) 
                .ToListAsync();
        }

        public async Task<Store> GetStoreByLocationAsync(string location)
        {
            return await _context.Stores
               .Include(s => s.StoreUsers)
               .FirstOrDefaultAsync(s => s.Location == location);
        }

        public async Task DeleteAsync(Store store)
        {
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
        }
    }
}
