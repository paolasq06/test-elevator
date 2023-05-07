using Application.Core.Exceptions;
using Application.Cqrs.Elevator.Queries.GetElevatorStatusQuery;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masiv.Elevator.Application.Integration.Test.Elevator.Queries
{
    using static Testing;

    public class GetElevatorStatusTest : TestBase
    {
        [Test]
        public async Task ShouldReturnElevatorStatus()
        {
            var query = new GetElevatorStatusQuery() { Id = 1 };

            var result = await SendAsync(query);

            result.Should().NotBeNull();

        }

        [Test]
        public async Task ShouldTheElevatorIsNotFound()
        {
            var query = new GetElevatorStatusQuery() { 
                Id = 3
            };

            await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<NotFoundException>();
        }

    }
}

