using GymManagment.Application.Common.Interfaces;
using GymManagment.Domain.Admins;
using GymManagment.Domain.Gyms;
using GymManagment.Domain.Subscription;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Infrastructure.Common.Presistence
{
    public class GymManagementDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Admin> Admins {  get; set; }
        public DbSet<Gym> Gyms { get; set; }

        public GymManagementDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
