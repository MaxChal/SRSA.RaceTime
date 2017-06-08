using System;
using System.Collections.Generic;

namespace RaceTime.CoreAPI.Models
{
    public partial class Series
    {
        public string SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string SeriesDescription { get; set; }
        public string SeriesType { get; set; }
    }
}
