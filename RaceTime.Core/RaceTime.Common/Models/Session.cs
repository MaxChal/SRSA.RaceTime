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
        public string ServerName { get; set; }
        public string Track { get; set; }
        public eSessionType SessionType { get; set; }
        public int SessionLaps { get; set; }
        public int SessionTime  { get; set; }
        public List<Competitor> Competitors { get; set; }
        
    }
}
