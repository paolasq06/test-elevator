using Application.Interfaces;
using AutoMapper;
using Core.Enums;
using Domain.Models.Elevator;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cqrs.Elevator.Commands.PostCallElevatorCommand
{
    public class PostCallElevatorCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
    }
    public class PostCallElevatorCommandHandler : IRequestHandler<PostCallElevatorCommand, bool>
    {
        private readonly IElevatorService _elevatorService;
        private readonly IElevatorCallStepService _elevatorCallStepService;
        private readonly IMapper _mapper;
        public PostCallElevatorCommandHandler(IElevatorService elevatorService,IElevatorCallStepService elevatorCallStepService, IMapper mapper)
        {
            _elevatorService = elevatorService;
            _elevatorCallStepService = elevatorCallStepService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(PostCallElevatorCommand request, CancellationToken cancellationToken)
        {
            ElevatorCallStep elevatorCallStep = _mapper.Map<ElevatorCallStep>(request);
            elevatorCallStep.Priority = (int)ElevatorCallPriority.InSide;

            await _elevatorCallStepService.PostElevatorCallStep(elevatorCallStep);
            await _elevatorService.Move(request.Id);
            
            return true;
        }

    }
}
