using BaristaXpertControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BaristaXpertControl.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<MenuCategory> MenuCategories { get; set; }
    }
}
