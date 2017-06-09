using System;
using System.Collections.Generic;

namespace RaceTime.Common.Models
{
    public partial class Event
    {
        public string EventId { get; set; }
        public string SeriesId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventType { get; set; }
    }
}
