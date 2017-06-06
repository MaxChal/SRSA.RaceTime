using System;
using System.Collections.Generic;
using System.Text;

namespace RaceTime.Common.Models
{
    public class Lap
    {
        public int LapNumber { get; set; }
        public DateTime LapTime { get; set; }
        public DateTime Sector1 { get; set; }
        public DateTime Sector2 { get; set; }
        public DateTime Sector3 { get; set; }
        public bool IsValid { get; set; }
        public string TyreCompound { get; set; }
    }
}
