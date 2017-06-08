using System;
using System.Collections.Generic;

namespace RaceTime.CoreAPI.Models
{
    public partial class Competitors
    {
        public string CompetitorId { get; set; }
        public string SessionId { get; set; }
        public string DriverId { get; set; }
        public string DriverGuid { get; set; }
        public string DriverTeam { get; set; }
        public int? CarId { get; set; }
        public string CarModel { get; set; }
        public string CarSkin { get; set; }
        public int? BallastKg { get; set; }
        public int? BestLap { get; set; }
        public int? TotalTime { get; set; }
        public int? LapCount { get; set; }
        public int? LapsLead { get; set; }
        public int? StartPosition { get; set; }
        public int? Position { get; set; }
        public string Gap { get; set; }
        public int? Incidents { get; set; }
        public double? Distance { get; set; }
        public double? CurrentSpeed { get; set; }
        public double? CurrentAcceleration { get; set; }
        public DateTime? CurrentLapStart { get; set; }
        public string CurrentTyreCompound { get; set; }
        public double? TopSpeed { get; set; }
        public double? StartSplinePos { get; set; }
        public double? EndSplinePos { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsConnected { get; set; }
        public bool? IsOnOutLap { get; set; }
        public int? ConnectionId { get; set; }
        public int? ConnectedTimestamp { get; set; }
        public int? DisconnectedTimestamp { get; set; }
    }
}
