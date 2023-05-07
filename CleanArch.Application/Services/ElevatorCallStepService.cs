using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Elevator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ElevatorCallStepService : IElevatorCallStepService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ElevatorCallStepService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         
        public async Task<ElevatorCallStep> PostElevatorCallStep(ElevatorCallStep elevatorCallStep)
        {
            return await _unitOfWork.elevatorCallStep.Add(elevatorCallStep);
        }

        public async Task<IList<ElevatorCallStep>> GetStepsByElevatorId(int ElevatorId)
        {
            return await _unitOfWork.elevatorCallStep
                .Get()
                .Include(x => x.Floor)
                .Where(x => x.ElevatorId == ElevatorId)
                .ToListAsync();
        }

        public async Task<List<ElevatorCallStep>> GetStepsByElevatorIdAndFloorId(int ElevatorId, int floorId)
        {
            return await _unitOfWork.elevatorCallStep
                .Get()
                .Include(x => x.Floor)
                .Where(x => x.ElevatorId == ElevatorId && x.FloorId == floorId)
                .ToListAsync();
        }

        public async Task RemoveSteps(List<ElevatorCallStep> elevatorCallSteps)
        {
            await _unitOfWork.elevatorCallStep.DeleteRange(elevatorCallSteps);
        }

        public async Task<ElevatorCallStep> GetStepById(Guid id)
        {
            return  await _unitOfWork.elevatorCallStep
                .Get()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ElevatorCallStep> GetNextStep(int id)
        {
            return await _unitOfWork.elevatorCallStep
                .Get()
                .Include(x => x.Floor)
                .Where(x => x.ElevatorId == id)
                .OrderBy(x => x.Priority).ThenBy(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task Remove(ElevatorCallStep elevatorCallStep)
        {
            await _unitOfWork.elevatorCallStep.Delete(elevatorCallStep);
        }
    }
}
