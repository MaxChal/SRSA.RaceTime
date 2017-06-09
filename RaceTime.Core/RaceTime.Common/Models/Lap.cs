using System;
using System.Collections.Generic;

namespace RaceTime.Common.Models
{
    public partial class Lap
    {
        public string LapId { get; set; }
        public string CompetitorId { get; set; }
        public int? LapTime { get; set; }
        public int? Sector1 { get; set; }
        public int? Sector2 { get; set; }
        public int? Sector3 { get; set; }
        public double? LapLength { get; set; }
        public int? LapNo { get; set; }
        public int? Position { get; set; }
        public int? Cuts { get; set; }
        public double? GripLevel { get; set; }
        public bool? IsValid { get; set; }
        public string TyreCompound { get; set; }
        public int? ConnectionId { get; set; }
        public int? Timestamp { get; set; }
    }
}
