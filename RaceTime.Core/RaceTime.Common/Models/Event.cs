using System;
using System.Collections.Generic;
using System.Text;

namespace RaceTime.Common.Models
{
    public class Event
    {
        public string EventID { get; set; }
        public string EventName { get; set; }
        public List<Session> EventSessions { get; set; }
    }
}
