using CleanArchitecture.Application.Common.Interfaces;
using Core.Models.Common;
using Domain.Models.Elevator;
using Domain.Models.Rol;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Context
{
    public class ApplicationDBContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDBContext(
            DbContextOptions options,
            ICurrentUserService currentUserService
            ) : base(options)
        {
            _currentUserService = currentUserService;
        }




        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<UserRol> UserRols { get; set; }
        public DbSet<Rol> Rols { get; set; }

        #endregion

        #region Elevator
        public DbSet<Elevator> Elevators { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<ElevatorCallStep> ElevatorCallSteps { get; set; }
        #endregion

        
            



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Load all assemblies from configurations folder in infra.data
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var UserInfo = _currentUserService.GetUserInfo();
            var userName = string.Concat(UserInfo?.Name, " ", UserInfo?.LastName)??"anonimus";
            
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Id;
                        entry.Entity.CreatedByName = userName;
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.Id;
                        entry.Entity.UpdatedByName = userName;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedAt = DateTime.Now;
                        entry.Entity.UpdatedByName = userName;
                        entry.Entity.UpdatedBy = _currentUserService.Id;
                        break;
                }
            }

            
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<EntityWithIntId> entry in ChangeTracker.Entries<EntityWithIntId>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        break;
                    case EntityState.Deleted:
                        break;
                }
            }




            var result = await base.SaveChangesAsync(cancellationToken);


            return result;
        }


    }
}
