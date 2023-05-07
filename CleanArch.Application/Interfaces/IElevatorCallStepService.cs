using Domain.Models.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IElevatorCallStepService
    {
        Task<ElevatorCallStep> PostElevatorCallStep(ElevatorCallStep elevatorCallStep);
        Task<IList<ElevatorCallStep>> GetStepsByElevatorId(int ElevatorId);
        Task Remove(ElevatorCallStep elevatorCallStep);
        Task<ElevatorCallStep> GetNextStep(int id);
        Task<List<ElevatorCallStep>> GetStepsByElevatorIdAndFloorId(int ElevatorId, int floorId);
        Task RemoveSteps(List<ElevatorCallStep> elevatorCallSteps);
    }
}
