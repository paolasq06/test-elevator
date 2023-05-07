using Domain.Models.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IElevatorService
    {
        Task<Elevator> GetById(int Id);
        Task<bool> CheckById(int Id);
        Task Move(int Id);
    }
}
