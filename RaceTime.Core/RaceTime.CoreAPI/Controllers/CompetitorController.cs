using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceTime.Common.Models;

namespace RaceTime.CoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Competitor")]
    public class CompetitorController : Controller
    {
        RaceTimeContext db = new RaceTimeContext();

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

            db.SaveChanges();
            return value;
        }

        // POST api/Competitor/disconnectcompetitor
        [HttpPost]
        [Route("DisconnectCompetitor")]
        public Competitor DisconnectCompetitor([FromBody]Competitor value)
        {
            db.Competitors.FirstOrDefault(comp => comp.CompetitorId == value.CompetitorId).IsConnected = false; 

            db.SaveChanges();
            return value;
        }
    }
}