using System;
using System.Collections.Generic;

namespace RaceTime.CoreAPI.Models
{
    public partial class Pitstops
    {
        public string PitstopId { get; set; }
        public string CompetitorId { get; set; }
        public int? LapNumber { get; set; }
        public int? Timestamp { get; set; }
    }
}
