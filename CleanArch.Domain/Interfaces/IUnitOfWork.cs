using Domain.Models.Elevator;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IIntRepository<Elevator> elevator { get; }
        IIntRepository<Floor> floor { get; }
        IRepository<ElevatorCallStep> elevatorCallStep { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
