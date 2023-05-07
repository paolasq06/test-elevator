using Application.Core.Exceptions;
using Application.Interfaces;
using Core.Enums;
using Domain.Interfaces;
using Domain.Models.Elevator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ElevatorService :  IElevatorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElevatorCallStepService _elevatorCallStepService;
        private readonly ILogger _logger;
        private Elevator _elevator;

        public ElevatorService(IUnitOfWork unitOfWork, IElevatorCallStepService elevatorCallStepService, ILogger<ElevatorService> logger)
        {
            _unitOfWork = unitOfWork;
            _elevatorCallStepService = elevatorCallStepService;
            _logger = logger;
        }

        public async Task<Elevator> GetById(int Id)
        {
            return await _unitOfWork.elevator
                .Get()
                .FirstOrDefaultAsync(x => x.Id == Id)
                ?? throw new NotFoundException("The elevator is not found");
        }

        public async Task<bool> CheckById(int Id)
        {
            return await _unitOfWork.elevator
                .Get()
                .CountAsync(x => x.Id == Id) == 0 ?
                false : true;
        }


        public async Task Move(int Id)
        {
            _elevator = await this.GetById(Id);

            var step = await _elevatorCallStepService.GetNextStep(Id);
            if (step == null)
                return;
            int calledFloor = step.Floor.Number;

            if (_elevator.CurrentFloor == calledFloor)
            {
                await this.OpenDoors();
                await _elevatorCallStepService.Remove(step);
                await this.Move(Id);
            }
            else
            {
                await this.CloseDoors();
                int floorDistance = Math.Abs(((int)_elevator.CurrentFloor - calledFloor));
                for (int i = 0; i < floorDistance; i++)
                {
                    await this.Step(calledFloor);
                }

            }

        }

        private async Task Step(int NextCalledFloor)
        {
            _logger.LogError("Elevator  {Id} arrive  at floor {floor}:", _elevator.Id, _elevator.CurrentFloor);
            await Task.Delay(_elevator.Speed);
            _elevator.CurrentFloor = _elevator.CurrentFloor > NextCalledFloor ?
                 --_elevator.CurrentFloor : ++_elevator.CurrentFloor;

            await _unitOfWork.elevator.Put(_elevator);
            await CheckCallFromFloor((int)_elevator.CurrentFloor);

        }

        private async Task CheckCallFromFloor(int floorId)
        {
            _logger.LogError("Check for call from the floor: {floorId}", floorId);
            var callesFromFloor = await _elevatorCallStepService.GetStepsByElevatorIdAndFloorId(_elevator.Id, floorId);
            if(callesFromFloor.Count() > 0)
            {
                await _elevatorCallStepService.RemoveSteps(callesFromFloor);
                await this.OpenDoors();
            }


            _elevator.DoorStatus = (int)ElevatorDoorStatus.Closed;
            await _unitOfWork.elevator.Put(_elevator);
        }

        private async Task CloseDoors()
        {
            _logger.LogError("Elevator  {Id} door is Closed at floor {floor}:", _elevator.Id, _elevator.CurrentFloor);
            if (_elevator.DoorStatus == (int)ElevatorDoorStatus.Closed)
                return;

            _elevator.DoorStatus = (int)ElevatorDoorStatus.Closed;
            await _unitOfWork.elevator.Put(_elevator);
        }

        private async Task OpenDoors()
        {
            _logger.LogError("Elevator  {Id} door is open at floor {floor}:", _elevator.Id , _elevator.CurrentFloor);
            if (_elevator.DoorStatus == (int)ElevatorDoorStatus.Opened)
                return;

            _elevator.DoorStatus = (int)ElevatorDoorStatus.Opened;
            await _unitOfWork.elevator.Put(_elevator);
        }
    }
}
