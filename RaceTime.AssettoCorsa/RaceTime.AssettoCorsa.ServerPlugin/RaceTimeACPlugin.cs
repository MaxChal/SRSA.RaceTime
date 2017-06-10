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
        public List<MsgLapCompletedLeaderboardEnty> Leaderboard { get; set; }
        public List<Competitor> Competitors { get; set; }

        public RaceTimeACPlugin()
        {
            Competitors = new List<Competitor>();            
        }

      
        protected override void OnAcServerAlive()
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnAcServerAlive Called");
            }
        }

        protected override void OnAcServerTimeout()
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnAcServerTimeout Called");
            }
        }

        protected override void OnBulkCarUpdateFinished()
        {
            base.OnBulkCarUpdateFinished();
        }

        protected override void OnCarInfo(MsgCarInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCarInfo Called {msg.CarId}");
            }
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
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnChatMessage Called {msg.CarId}");
            }
        }

        protected override void OnClientLoaded(MsgClientLoaded msg)
        {

            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnClientLoaded Called {msg.CarId}");
            }
        }

        protected override void OnCollision(IncidentInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCollison Called {msg.ImpactSpeed}");
            }
        }

        protected override void OnCollision(MsgClientEvent msg)
        {
            base.OnCollision(msg);
        }

        protected override bool OnCommandEntered(string cmd)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCommandEntered Called {cmd}");
            }
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
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnConnectionClosed Called {msg.CarId}");
            }

            var competitor = Competitors.FirstOrDefault(comp => comp.ConnectionId == msg.CarId && comp.DriverGuid == msg.DriverGuid);
            Competitors.Remove(competitor);
            competitor.IsConnected = false;

            ApiWrapperNet4.Post<Competitor>("competitor/disconnectCompetitor", competitor);
        }

        protected override void OnDisconnected()
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnDisconnected Called");
            }
        }

        protected override void OnInit()
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnInit Called");
            }
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
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnNewConnection Called {msg.CarId}");
            }

            var competitor = new Competitor
            {
                CompetitorId = Guid.NewGuid().ToString(),
                SessionId = CurrentSession.SessionId,
                CarId = msg.CarId,
                ConnectionId = msg.CarId,
                CarModel = msg.CarModel,
                CarSkin = msg.CarSkin,
                DriverName = msg.DriverName,
                DriverGuid = msg.DriverGuid,
                IsConnected = true
            };

            Competitors.Add(ApiWrapperNet4.Post<Competitor>("competitor/addcompetitor", competitor));           
        }

        protected override void OnNewSession(MsgSessionInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnNewSession Called {msg.SessionType}");
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

            Competitors.Where(comp => comp.IsConnected == true && comp.SessionId != CurrentSession.SessionId).ToList().ForEach(comp =>
            {
                comp.SessionId = CurrentSession.SessionId;
                comp.CompetitorId = Guid.NewGuid().ToString();
                ApiWrapperNet4.Post<Competitor>("competitor/addcompetitor", comp);
            });
        }

        protected override void OnProtocolVersion(MsgVersionInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnProtocolVersion Called {msg.Version}");
            }
        }

        protected override void OnServerError(MsgError msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnServerError Called {msg.ErrorMessage}");
            }
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

            if (CurrentSession != null)
            {
                CurrentSession.AmbientTemp = msg.AmbientTemp;
                CurrentSession.ElapsedMs = msg.ElapsedMS;
                CurrentSession.RoadTemp = msg.RoadTemp;
                CurrentSession.ServerName = msg.ServerName;
                CurrentSession.SessionDuration = msg.SessionDuration;
                CurrentSession.SessionLaps = msg.Laps;
                CurrentSession.SessionName = msg.Name;
                CurrentSession.SessionTrack = msg.Track;
                CurrentSession.SessionTrackConfig = msg.TrackConfig;
                CurrentSession.SessionType = msg.SessionType;
                CurrentSession.SessionWaitTime = msg.WaitTime;
                CurrentSession.Version = msg.Version;
                CurrentSession.CurrentSessionIndex = msg.CurrentSessionIndex;
                CurrentSession.SessionCount = msg.SessionCount;
                CurrentSession.SessionIndex = msg.SessionIndex;
                CurrentSession.Weather = msg.Weather;
                CurrentSession = ApiWrapperNet4.Post<Session>("session/edit", CurrentSession);
            }
            else
            {
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
}
