using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Elevator
{
    public class ElevatorCallStep : Entity
    {
        public int ElevatorId { get; set; }
        public int FloorId { get; set; }
        public int Priority { get; set; }
        public Floor Floor { get; set; }
        public bool CompleteFloor { get; set; }
    }
}
