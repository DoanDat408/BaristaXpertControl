using BaristaXpertControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Application.Common.Persistences
{
    public interface IStoreRepository
    {
        Task<Store> AddAsync(Store store);
        Task<Store> GetByIdAsync(int id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task<Store> GetStoreByLocationAsync(string location);
        Task DeleteAsync(Store store);
    }
}
