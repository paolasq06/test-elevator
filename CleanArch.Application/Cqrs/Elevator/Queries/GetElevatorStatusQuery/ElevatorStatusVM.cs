using Domain.Models.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Elevator.Queries.GetElevatorStatusQuery
{
    public class ElevatorStatusVM
    {
        public int? CurrentFloor { get; set; }
        public IList<ElevatorCallStep>  elevatorCallSteps{ get; set; }
    }
}
