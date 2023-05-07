using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cqrs.Elevator.Commands.PostCallElevatorCommand
{
    public class PostCallElevatorCommandValidator : AbstractValidator<PostCallElevatorCommand>
    {
        private readonly IElevatorService _elevatorService;
        private readonly IFloorService _floorService;

        public PostCallElevatorCommandValidator(IElevatorService elevatorService, IFloorService floorService)
        {
            _elevatorService = elevatorService;
            _floorService = floorService;
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FloorId).NotNull();
            RuleFor(x => x.Id).MustAsync(checkElevatorId).WithMessage("The ElevatorId is not valid");
            RuleFor(x => x.FloorId).MustAsync(checkFloorId).WithMessage("The Floor is not valid");

        }

        private async Task<bool> checkElevatorId(int elevatorId, CancellationToken cancellation)
        {
                return await _elevatorService.CheckById(elevatorId);
        }
        private async Task<bool> checkFloorId(PostCallElevatorCommand x, int floorId,CancellationToken cancellationToken)
        {
                return await _floorService.CheckByFloorIdAndElevatorId(x.FloorId, x.Id);
        }

    }

    
}
