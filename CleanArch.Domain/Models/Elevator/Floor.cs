using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Elevator
{
    public class Floor : EntityWithIntId
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public bool Status { get; set; }
        public int ElevatorId { get; set; }
        public Elevator Elevator { get; set; }
        

    }
}
