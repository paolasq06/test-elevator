using Domain.Models.Elevator;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Masiv.Elevator.Application.Integration.Test
{
    using static Testing;
    public class TestBase
    {

        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();

            await AddAsync(new Domain.Models.Elevator.Elevator
            {
                Name = "Main elevator",
                Status = true,
                Speed = 1,
                DoorStatus = 0,
                CurrentFloor = 1
            });


            await AddAsync(new Floor
            {
                Name = "Main elevator",
                Status = true,
                ElevatorId = 1,
                Number = 1
            });
        }
    }
}
