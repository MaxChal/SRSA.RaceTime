using System;
using System.Collections.Generic;

namespace RaceTime.Common.Models
{
    public partial class Session
    {
        public string SessionId { get; set; }
        public string EventId { get; set; }
        public string ServerName { get; set; }
        public string SessionName { get; set; }
        public int? SessionType { get; set; }
        public string SessionTrack { get; set; }
        public int? SessionLaps { get; set; }
        public int? SessionDuration { get; set; }
        public int? SessionWaitTime { get; set; }
        public string SessionTrackConfig { get; set; }
        public int? Version { get; set; }
        public int? AmbientTemp { get; set; }
        public int? RoadTemp { get; set; }
        public string Weather { get; set; }
        public long? ElapsedMs { get; set; }
        public int? Timestamp { get; set; }
        public bool? IsActive { get; set; }
        public int? SessionIndex { get; set; }     
        public int? CurrentSessionIndex { get; set; }        
        public int? SessionCount { get; set; }
    }
}
