using CleanArch.Infra.Data.Context;
using Domain.Interfaces;
using Domain.Models.Elevator;
using System;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDBContext _ctx;
        public IIntRepository<Elevator> elevator => new BaseIntRepository<Elevator>(_ctx);
        public IIntRepository<Floor> floor => new BaseIntRepository<Floor>(_ctx);
        public IRepository<ElevatorCallStep>  elevatorCallStep => new BaseRepository<ElevatorCallStep>(_ctx);

        public UnitOfWork(ApplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
