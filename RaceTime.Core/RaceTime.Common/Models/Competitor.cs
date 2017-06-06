using System;
using System.Collections.Generic;
using System.Text;

namespace RaceTime.Common.Models
{
    public class Competitor
    {
        public string CompetitorName { get; set; }
        public int LapsLead { get; set; }
        public int Position { get; set; }
        public List<Lap> Laps { get; set; }
    }
}
