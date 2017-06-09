using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using acPlugins4net;
using acPlugins4net.info;
using acPlugins4net.messages;
using System.IO;
using Newtonsoft.Json;
using RaceTime.Common.Helpers;
using RaceTime.AssettoCorsa.Common;
using RaceTime.AssettoCorsa.Common.Helpers;
using RaceTime.Common.Models;

namespace RaceTime.AssettoCorsa.ServerPlugin
{
    public class RaceTimeACPlugin : AcServerPlugin
    {

        public Session CurrentSession { get; set; }
        public MsgLapCompletedLeaderboardEnty Leaderboard { get; set; }

        public RaceTimeACPlugin()
        {
            
        }

      
        protected override void OnAcServerAlive()
        {
            base.OnAcServerAlive();
        }

        protected override void OnAcServerTimeout()
        {
            base.OnAcServerTimeout();
        }

        protected override void OnBulkCarUpdateFinished()
        {
            base.OnBulkCarUpdateFinished();
        }

        protected override void OnCarInfo(MsgCarInfo msg)
        {
            base.OnCarInfo(msg);
        }

        protected override void OnCarUpdate(DriverInfo driverInfo)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCarUpdate Called {driverInfo.TotalTime} {driverInfo.Gap}");
            //}
            //base.OnCarUpdate(driverInfo);
        }

        protected override void OnCarUpdate(MsgCarUpdate msg)
        {
            base.OnCarUpdate(msg);
        }

        protected override void OnChatMessage(MsgChat msg)
        {
            base.OnChatMessage(msg);
        }

        protected override void OnClientLoaded(MsgClientLoaded msg)
        {

            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnClientLoaded Called");
            }
        }

        protected override void OnCollision(IncidentInfo msg)
        {
            base.OnCollision(msg);
        }

        protected override void OnCollision(MsgClientEvent msg)
        {
            base.OnCollision(msg);
        }

        protected override bool OnCommandEntered(string cmd)
        {
            return base.OnCommandEntered(cmd);
        }

        protected override void OnConnected()
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnConnectied Called");
            }
        }

        protected override void OnConnectionClosed(MsgConnectionClosed msg)
        {
            base.OnConnectionClosed(msg);
        }

        protected override void OnDisconnected()
        {
            base.OnDisconnected();
        }

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnLapCompleted(LapInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnLapCompleted Called:{TimeSpan.FromMilliseconds(msg.Laptime).ToString()}");
            }

            Lap lap = new Lap
            {
                LapId = Guid.NewGuid().ToString(),
                CompetitorId = "",
                ConnectionId = msg.ConnectionId,
                Cuts = msg.Cuts,
                GripLevel = msg.GripLevel,
                LapTime = (int)msg.Laptime,
                LapNo = msg.LapNo,
                Timestamp = (int)msg.Timestamp,
                Position = msg.Position,
                IsValid = msg.Cuts == 0,
                LapLength = msg.LapLength
            };

            var addLap = ApiWrapperNet4.Post<Lap>("lap/addlap", lap);
        }

        protected override void OnLapCompleted(MsgLapCompleted msg)
        {
            Leaderboard = msg.Leaderboard;
        }

        protected override void OnNewConnection(MsgNewConnection msg)
        {
            base.OnNewConnection(msg);
        }

        protected override void OnNewSession(MsgSessionInfo msg)
        {
            base.OnNewSession(msg);
        }

        protected override void OnProtocolVersion(MsgVersionInfo msg)
        {
            base.OnProtocolVersion(msg);
        }

        protected override void OnServerError(MsgError msg)
        {
            base.OnServerError(msg);
        }

        protected override void OnSessionEnded(MsgSessionEnded msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnSessionEnded Called");
            }

            CurrentSession.IsActive = false;

            var endSession = ApiWrapperNet4.Post<Session>("session/endSession", CurrentSession);
        }

        protected override void OnSessionInfo(MsgSessionInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnSessionInfo Called {JsonConvert.SerializeObject(msg)}");
            }

            CurrentSession = new Session
            {
                SessionId = Guid.NewGuid().ToString(),
                EventId = "",
                AmbientTemp = msg.AmbientTemp,
                ElapsedMs = msg.ElapsedMS,
                IsActive = true,
                RoadTemp = msg.RoadTemp,
                ServerName = msg.ServerName,
                SessionDuration = msg.SessionDuration,
                SessionLaps = msg.Laps,
                SessionName = msg.Name,
                SessionTrack = msg.Track,
                SessionTrackConfig = msg.TrackConfig,
                SessionType = msg.SessionType,
                SessionWaitTime = msg.WaitTime,
                Timestamp = DateTime.Now.TimeOfDay.Milliseconds,
                Version = msg.Version,
                CurrentSessionIndex = msg.CurrentSessionIndex,
                SessionCount = msg.SessionCount,
                SessionIndex = msg.SessionIndex,
                Weather = msg.Weather
            };

            CurrentSession = ApiWrapperNet4.Post<Session>("session/addsession", CurrentSession);
        }
    }
}
