using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Application.Persistences;
using BaristaXpertControl.Domain.Entities;
using BaristaXpertControl.Infrastructure.Data;
using BaristaXpertControl.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System;

namespace BaristaXpertControl.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IStoreRepository StoreRepository { get; private set; } // Thêm StoreRepository vào

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

            Users = new UserRepository(_userManager, _roleManager);
            Roles = new RoleRepository(_roleManager);
            StoreRepository = new StoreRepository(_context); // Khởi tạo StoreRepository
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
