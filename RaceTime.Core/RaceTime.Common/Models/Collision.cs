using System;
using System.Collections.Generic;

namespace RaceTime.Common.Models
{
    public partial class Collision
    {
        public string CollisionId { get; set; }
        public string SessionId { get; set; }
        public int? Type { get; set; }
        public int? Timestamp { get; set; }
        public string Competitor1 { get; set; }
        public string Competitor2 { get; set; }
        public double? ImpactSpeed { get; set; }
        public string WorldPostion { get; set; }
        public string RelPosition { get; set; }
    }
}
