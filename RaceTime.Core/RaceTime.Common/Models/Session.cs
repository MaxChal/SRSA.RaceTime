using System;
using System.Collections.Generic;
using System.Text;
using RaceTime.Common.Common;

namespace RaceTime.Common.Models
{
    public class Session
    {
        public string SessionID { get; set; }
        public string SessionName { get; set; }
        public eSessionType SessionType { get; set; }
        public int NumberOfLaps { get; set; }
        public DateTime SessionLength  { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionEndTime { get; set; }
        public List<Competitor> Competitors { get; set; }
    }
}
