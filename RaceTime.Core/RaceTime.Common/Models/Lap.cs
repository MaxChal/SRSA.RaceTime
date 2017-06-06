using System;
using System.Collections.Generic;
using System.Text;

namespace RaceTime.Common.Models
{
    public class Lap
    {
        public int LapNumber { get; set; }
        public TimeSpan LapTime { get; set; }
        public TimeSpan Sector1 { get; set; }
        public TimeSpan Sector2 { get; set; }
        public TimeSpan Sector3 { get; set; }
        public bool IsValid { get; set; }
        public string TyreCompound { get; set; }
    }
}
