using Application.Cqrs.Elevator.Commands.PostCallElevatorCommand;
using Application.Cqrs.Elevator.Commands.PostCallElevatorFromFloorCommand;
using Application.Cqrs.Elevator.Queries.GetElevatorStatusQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Masiv.Api.Controllers
{
    [Route("api/Elevator")]
    [ApiController]
    public class ElevatorController : ApiControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetElevatorStatus(int id)
        {
            return Ok(await Mediator.Send(new GetElevatorStatusQuery() { Id = id }));
        }


        [HttpPost("Call/{id}")]
        public async Task<IActionResult> PostCallElevatorInside([FromBody] PostCallElevatorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CallFromFloor/{id}")]
        public async Task<IActionResult> PostCallElevatorFloor([FromBody] PostCallElevatorFromFloorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


    }
}
