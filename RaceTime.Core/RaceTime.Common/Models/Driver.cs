using System;
using System.Collections.Generic;

namespace RaceTime.Common.Models
{
    public partial class Driver
    {
        public string DriverId { get; set; }
        public string DriverName { get; set; }
        public int? DriverAge { get; set; }
        public string DriverLevel { get; set; }
        public string DriverSeries { get; set; }
    }
}
