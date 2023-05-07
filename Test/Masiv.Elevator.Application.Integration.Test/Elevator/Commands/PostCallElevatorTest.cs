using Application.Cqrs.Elevator.Commands.PostCallElevatorCommand;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Exceptions;

namespace Masiv.Elevator.Application.Integration.Test.Elevator.Commands
{
    using static Testing;
    public class PostCallElevatorTest : TestBase
    {
        [Test]
        public void ShouldRequiredAvalidElevator()
        {
            var command = new PostCallElevatorCommand() { 
                FloorId = 1,
                Id = 2
            };
            FluentActions.Invoking(() =>
                            SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequiredAvalidFloor()
        {
            var command = new PostCallElevatorCommand()
            {
                FloorId = 3,
                Id = 1
            };
            FluentActions.Invoking(() =>
                            SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCallElevatorFromInside()
        {
            var command = new PostCallElevatorCommand() { 
                FloorId = 1,
                Id = 1
            };
                   var Result = await SendAsync(command);

            Result.Should().BeTrue();

        }
    }
}
