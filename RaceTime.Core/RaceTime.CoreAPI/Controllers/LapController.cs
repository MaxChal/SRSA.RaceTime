using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceTime.Common.Models;
using RaceTime.CoreAPI.Hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace RaceTime.CoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Lap")]
    public class LapController : ApiHubController<TestHub>
    {
        RaceTimeContext db = new RaceTimeContext();

        public LapController(IConnectionManager signalRConnectionManager)
           : base(signalRConnectionManager)
        {

        }


        // POST api/lap/addlap
        [HttpPost]
        [Route("AddLap")]
        public Lap AddLap([FromBody]Lap value)
        {
            //new sector 1
            if (value.LapTime == null && value.Sector2 == null)
            {
                while (db.Laps.Any(lap => lap.CompetitorId == value.CompetitorId && lap.LapTime == null))
                {
                    var delLap = db.Laps.FirstOrDefault(lap => lap.CompetitorId == value.CompetitorId && lap.LapTime == null);
                    db.Laps.Remove(delLap);
                    db.SaveChanges();
                }
            }

            SendLapAsync(value);
            db.Laps.Add(value);
            
            db.SaveChanges();
            return value;
        }


        // POST api/lap/editlap
        [HttpPost]
        [Route("EditLap")]
        public Lap EditLap([FromBody]Lap value)
        {
            SendLapAsync(value);
            db.Entry(db.Laps.FirstOrDefault(lap => lap.LapId == value.LapId)).CurrentValues.SetValues(value);
            db.SaveChanges();
            return value;
        }

        public async Task SendLapAsync(Lap lap)
        {
            await Clients.All.Test(lap);
        }
    }
}