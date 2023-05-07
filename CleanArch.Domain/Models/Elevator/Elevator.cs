using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Elevator
{

    public class Elevator : EntityWithIntId
    {
        public bool Status { get; set; }
        public bool HasQueue { get; set; }
        public int DoorStatus { get; set; }
        public int? CurrentFloor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Speed { get; set; }
    }
}
