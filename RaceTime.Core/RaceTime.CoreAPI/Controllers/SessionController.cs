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
    [Route("api/Session")]
    public class SessionController : Controller
    {

        RaceTimeContext db = new RaceTimeContext();
        // POST api/session/addsession
        [HttpPost]
        [Route("AddSession")]
        public Session AddSession([FromBody]Session value)
        {
            db.Sessions.Where(session => session.ServerName == value.ServerName && session.IsActive == true).ToList().ForEach(sesson => sesson.IsActive = false);

            db.Sessions.Add(value);
            db.SaveChanges();
            return value;
        }

        // POST api/session/endsession
        [HttpPost]
        [Route("EndSession")]
        public Session EndSession([FromBody]Session value)
        {
            db.Sessions.FirstOrDefault(session => session.SessionId == value.SessionId).IsActive = false;
            db.SaveChanges();
            return value;
        }

        // POST api/session/editsession
        [HttpPost]
        [Route("EditSession")]
        public Session EditSession([FromBody]Session value)
        {
            if (db.Sessions.Any(s => s.SessionId == value.SessionId))
            {
                db.Sessions.Remove(db.Sessions.FirstOrDefault(session => session.SessionId == value.SessionId));
                db.Sessions.Add(value);
            } 
            db.SaveChanges();
            return value;
        }
    }
}