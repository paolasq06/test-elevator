using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cqrs.Elevator.Queries.GetElevatorStatusQuery
{
    public class GetElevatorStatusQuery : IRequest<ElevatorStatusVM>
    {
        public int Id { get; set; }
    }

    public class GetElevatorStatusQueryHandler : IRequestHandler<GetElevatorStatusQuery, ElevatorStatusVM>
    {
        private readonly IElevatorService _elevatorService;
        private readonly IElevatorCallStepService _elevatorCallStepService;

        private readonly IMapper _mapper;
        public GetElevatorStatusQueryHandler(IElevatorService elevatorService, IMapper mapper, IElevatorCallStepService elevatorCallStepService)
        {
            _elevatorService = elevatorService;
            _mapper = mapper;
            _elevatorCallStepService = elevatorCallStepService;
        }

        public async Task<ElevatorStatusVM> Handle(GetElevatorStatusQuery request, CancellationToken cancellationToken)
        {

            var elevator = await _elevatorService.GetById(request.Id);
            var steps = await _elevatorCallStepService.GetStepsByElevatorId(request.Id);

            return new ElevatorStatusVM

            {
                CurrentFloor = elevator.CurrentFloor,
                elevatorCallSteps = steps

            };

        }

    }
}
