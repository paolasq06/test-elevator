using Application.Interfaces;
using AutoMapper;
using Core.Enums;
using Domain.Models.Elevator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cqrs.Elevator.Commands.PostCallElevatorFromFloorCommand
{
    public class PostCallElevatorFromFloorCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
    }
    public class PostCallElevatorFromFloorCommandHandler : IRequestHandler<PostCallElevatorFromFloorCommand, bool>
    {
        private readonly IElevatorService _elevatorService;
        private readonly IElevatorCallStepService _elevatorCallStepService;
        private readonly IMapper _mapper;
        public PostCallElevatorFromFloorCommandHandler(IElevatorService elevatorService, IElevatorCallStepService elevatorCallStepService, IMapper mapper)
        {
            _elevatorService = elevatorService;
            _elevatorCallStepService = elevatorCallStepService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(PostCallElevatorFromFloorCommand request, CancellationToken cancellationToken)
        {
            ElevatorCallStep elevatorCallStep = _mapper.Map<ElevatorCallStep>(request);
            elevatorCallStep.Priority = (int)ElevatorCallPriority.Outside;

            var data = await _elevatorCallStepService.PostElevatorCallStep(elevatorCallStep);
            return true;
        }

    }
}

