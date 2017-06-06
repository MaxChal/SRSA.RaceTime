using System;
using System.Collections.Generic;
using System.Text;

namespace RaceTime.Common.Models
{
    public class Series
    {
        public string SeriesID { get; set; }
        public string SeriesName { get; set; }
        public List<Event> SeriesEvents { get; set; }
    }
}
