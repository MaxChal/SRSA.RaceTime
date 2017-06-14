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
    [Route("api/Competitor")]
    public class CompetitorController : ApiHubController<AssettoCorsaHub>
    {
        RaceTimeContext db = new RaceTimeContext();

        public CompetitorController(IConnectionManager signalRConnectionManager)
           : base(signalRConnectionManager)
        {

        }

        // POST api/Competitor/addcompetitor
        [HttpPost]
        [Route("AddCompetitor")]
        public Competitor AddCompetitor([FromBody]Competitor value)
        {
            db.Competitors.Where(comp => comp.DriverGuid == value.DriverGuid && comp.IsConnected == true).ToList().ForEach(comp => comp.IsConnected = false);

            if (db.Competitors.Any(comp => comp.DriverGuid == value.DriverGuid && comp.ConnectionId == value.ConnectionId && comp.SessionId == value.SessionId))
            {
                var competitor = db.Competitors.FirstOrDefault(comp => comp.DriverGuid == value.DriverGuid && comp.ConnectionId == value.ConnectionId && comp.SessionId == value.SessionId);
                competitor.IsConnected = true;
                value.CompetitorId = competitor.CompetitorId;
            }
            else
                db.Competitors.Add(value);

            SendNewCompetitorAsync(value);

            db.SaveChanges();
            return value;
        }

        // POST api/Competitor/disconnectcompetitor
        [HttpPost]
        [Route("DisconnectCompetitor")]
        public Competitor DisconnectCompetitor([FromBody]Competitor value)
        {
            SendDisconnectedCompetitorAsync(value);
            db.Competitors.FirstOrDefault(comp => comp.CompetitorId == value.CompetitorId).IsConnected = false;

            while (db.Laps.Any(lap => lap.CompetitorId == value.CompetitorId && lap.LapTime == null))
            {
                var delLap = db.Laps.FirstOrDefault(lap => lap.CompetitorId == value.CompetitorId && lap.LapTime == null);
                db.Laps.Remove(delLap);
                db.SaveChanges();
            }
            
            db.SaveChanges();
            return value;
        }

        // POST api/Competitor/editcompetitor
        [HttpPost]
        [Route("EditCompetitor")]
        public Competitor EditCompetitor([FromBody]Competitor value)
        {
            SendUpdatedCompetitorAsync(value);
            db.Entry(db.Competitors.FirstOrDefault(comp => comp.CompetitorId == value.CompetitorId)).CurrentValues.SetValues(value);
            db.SaveChanges();
            return value;
        }

        public async Task SendNewCompetitorAsync(Competitor competitor)
        {
            await Clients.All.NewCompetitor(competitor);
        }

        public async Task SendDisconnectedCompetitorAsync(Competitor competitor)
        {
            await Clients.All.DisconnectedCompetitor(competitor);
        }

        public async Task SendUpdatedCompetitorAsync(Competitor competitor)
        {
            await Clients.All.UpdatedCompetitor(competitor);
        }
    }
}