using System;
using System.Collections.Generic;

namespace RaceTime.Common.Models
{
    public partial class Pitstop
    {
        public string PitstopId { get; set; }
        public string CompetitorId { get; set; }
        public int? LapNumber { get; set; }
        public int? Timestamp { get; set; }
    }
}
