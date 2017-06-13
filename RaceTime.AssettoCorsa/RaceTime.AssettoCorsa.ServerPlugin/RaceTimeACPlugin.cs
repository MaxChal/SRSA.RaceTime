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
using RaceTime.Common.Enums;
using RaceTime.AssettoCorsa.ServerPlugin.Models;

namespace RaceTime.AssettoCorsa.ServerPlugin
{
    public class RaceTimeACPlugin : AcServerPlugin
    {

        public Session CurrentSession { get; set; }
        public List<MsgLapCompletedLeaderboardEnty> Leaderboard { get; set; }
        public List<Competitor> Competitors { get; set; }
        public List<Lap> CurrentLaps { get; set; }

        public RaceTimeACPlugin()
        {
            Competitors = new List<Competitor>();
            CurrentLaps = new List<Lap>();
        }


        protected override void OnAcServerAlive()
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnAcServerAlive Called");
            //}
        }

        protected override void OnAcServerTimeout()
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnAcServerTimeout Called");
            //}
        }

        protected override void OnBulkCarUpdateFinished()
        {
            base.OnBulkCarUpdateFinished();
        }

        protected override void OnCarInfo(MsgCarInfo msg)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCarInfo Called {msg.CarId}");
            //}

            if (Competitors.Any(driver => driver.DriverGuid == msg.DriverGuid && driver.IsConnected == true) || string.IsNullOrEmpty(msg.DriverGuid)) return;


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

        protected override void OnCarUpdate(DriverInfo driverInfo)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCarUpdate Called {driverInfo.TotalTime} {driverInfo.Gap}");
            //}



            var currentCompetitor = Competitors.FirstOrDefault(driver => driver.DriverGuid == driverInfo.DriverGuid);
            if (currentCompetitor == null) return;

            Competitor matchCompetitor = new Competitor()
            {
                CompetitorId = currentCompetitor.CompetitorId,
                CurrentAcceleration = currentCompetitor.CurrentAcceleration,
                CurrentSpeed = currentCompetitor.CurrentSpeed,
                CurrentTyreCompound = currentCompetitor.CurrentTyreCompound,
                Distance = currentCompetitor.Distance,
                DriverId = currentCompetitor.DriverId,
                EndSplinePos = currentCompetitor.EndSplinePos,
                IsConnected = currentCompetitor.IsConnected,
                LapsLead = currentCompetitor.LapsLead,
                SessionId = currentCompetitor.SessionId,
                StartSplinePos = currentCompetitor.StartSplinePos,
                DriverGuid = currentCompetitor.DriverGuid,
                DriverName = currentCompetitor.DriverName,
                DriverTeam = currentCompetitor.DriverTeam,
                CarId = currentCompetitor.CarId,
                CarModel = currentCompetitor.CarModel,
                CarSkin = currentCompetitor.CarSkin,
                BallastKg = currentCompetitor.BallastKg,
                BestLap = currentCompetitor.BestLap,
                TotalTime = currentCompetitor.TotalTime,
                LapCount = currentCompetitor.LapCount,
                StartPosition = currentCompetitor.StartPosition,
                Position = currentCompetitor.Position,
                Gap = currentCompetitor.Gap,
                Incidents = currentCompetitor.Incidents,
                CurrentLapStart = currentCompetitor.CurrentLapStart,
                TopSpeed = currentCompetitor.TopSpeed,
                IsAdmin = currentCompetitor.IsAdmin,
                IsOnOutLap = currentCompetitor.IsOnOutLap,
                ConnectionId = currentCompetitor.ConnectionId,
                ConnectedTimestamp = currentCompetitor.ConnectedTimestamp,
                DisconnectedTimestamp = currentCompetitor.DisconnectedTimestamp,
            };

            //currentCompetitor.LapsLead = driverInfo.Lap
            //currentCompetitor.CurrentTyreCompound = driverInfo

            currentCompetitor.DriverGuid = driverInfo.DriverGuid;
            currentCompetitor.DriverName = driverInfo.DriverName;
            currentCompetitor.DriverTeam = driverInfo.DriverTeam;
            currentCompetitor.CarId = driverInfo.CarId;
            currentCompetitor.CarModel = driverInfo.CarModel;
            currentCompetitor.CarSkin = driverInfo.CarSkin;
            currentCompetitor.BallastKg = driverInfo.BallastKG;
            currentCompetitor.BestLap = (int)driverInfo.BestLap == 0 ? currentCompetitor.BestLap : (int)driverInfo.BestLap;
            currentCompetitor.TotalTime = (int)driverInfo.TotalTime == 0 ? currentCompetitor.TotalTime : (int)driverInfo.TotalTime;
            currentCompetitor.LapCount = driverInfo.LapCount;
            currentCompetitor.StartPosition = driverInfo.StartPosition;
            currentCompetitor.Position = driverInfo.Position;
            currentCompetitor.Gap = driverInfo.Gap;
            currentCompetitor.Incidents = driverInfo.Incidents;
            currentCompetitor.CurrentLapStart = driverInfo.CurrentLapStart;
            currentCompetitor.TopSpeed = driverInfo.TopSpeed;
            currentCompetitor.IsAdmin = driverInfo.IsAdmin;
            currentCompetitor.IsOnOutLap = driverInfo.IsOnOutlap;
            currentCompetitor.ConnectionId = driverInfo.ConnectionId;
            currentCompetitor.ConnectedTimestamp = (int)driverInfo.ConnectedTimestamp;
            currentCompetitor.DisconnectedTimestamp = (int)driverInfo.DisconnectedTimestamp;

            if (currentCompetitor.TopSpeed != matchCompetitor.TopSpeed ||
                currentCompetitor.BestLap != matchCompetitor.BestLap ||
                currentCompetitor.LapCount != matchCompetitor.LapCount ||
                currentCompetitor.Position != matchCompetitor.Position ||
                currentCompetitor.Gap != matchCompetitor.Gap ||
                currentCompetitor.Incidents != matchCompetitor.Incidents ||
                currentCompetitor.CurrentLapStart != matchCompetitor.CurrentLapStart
                )
            {
                //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
                //using (var b = new StreamWriter(a))
                //{
                //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCarUpdate Called {driverInfo.TotalTime} {driverInfo.Gap}");
                //}
                var result = ApiWrapperNet4.Post<Competitor>("competitor/editcompetitor", currentCompetitor);
            }
        }

        protected override void OnCarUpdate(MsgCarUpdate msg)
        {
            base.OnCarUpdate(msg);
        }

        protected override void OnChatMessage(MsgChat msg)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnChatMessage Called {msg.CarId}");
            //}
        }

        protected override void OnClientLoaded(MsgClientLoaded msg)
        {

            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnClientLoaded Called {msg.CarId}");
            //}
        }

        protected override void OnCollision(IncidentInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCollison Called {msg.ImpactSpeed}");
            }

            var collision = new Collision
            {
                CollisionId = Guid.NewGuid().ToString(),
                Competitor1 = Competitors.FirstOrDefault(driver => driver.ConnectionId == msg.ConnectionId1)?.CompetitorId,
                Competitor2 = Competitors.FirstOrDefault(driver => driver.ConnectionId == msg.ConnectionId2)?.CompetitorId,
                ImpactSpeed = msg.ImpactSpeed,
                RelPosition = msg.RelPosition.ToString(),
                SessionId = CurrentSession.SessionId,
                Timestamp = (int)DateTime.Now.TimeOfDay.TotalMilliseconds,
                Type = msg.Type,
                WorldPostion = msg.WorldPosition.ToString()
            };

            ApiWrapperNet4.Post<Competitor>("collision/addcollision", collision);
        }

        protected override void OnCollision(MsgClientEvent msg)
        {
            base.OnCollision(msg);
        }

        protected override bool OnCommandEntered(string cmd)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCommandEntered Called {cmd}");
            //}
            return base.OnCommandEntered(cmd);
        }

        protected override void OnConnected()
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnConnectied Called");
            //}
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
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnInit Called");
            //}
        }

        protected override void OnLapCompleted(LapInfo msg)
        {
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnLapCompleted Called:{TimeSpan.FromMilliseconds(msg.Laptime).ToString()}");
            }

            var currentCompetitor = Competitors.FirstOrDefault(driver => msg.ConnectionId == driver.ConnectionId);
            var currentLap = CurrentLaps.FirstOrDefault(l => l.CompetitorId == currentCompetitor.CompetitorId);
            CurrentLaps.Remove(currentLap);

            if (currentLap != null)
            {
                currentLap.Cuts = msg.Cuts;
                currentLap.GripLevel = msg.GripLevel;
                currentLap.LapTime = (int)msg.Laptime;
                currentLap.LapNo = msg.LapNo;
                currentLap.Timestamp = (int)msg.Timestamp;
                currentLap.Position = msg.Position;
                currentLap.IsValid = msg.Cuts == 0;
                currentLap.LapLength = msg.LapLength;

                if (currentLap.Sector1 == default(int?)) currentLap.Sector1 = currentLap.LapTime;
                else if (currentLap.Sector2 == default(int?)) currentLap.Sector2 = currentLap.LapTime - currentLap.Sector1;
                else if (currentLap.Sector3 == default(int?)) currentLap.Sector3 = currentLap.LapTime - currentLap.Sector1 - currentLap.Sector2;

                var result = ApiWrapperNet4.Post<Lap>("lap/editlap", currentLap);
            }
            else
            {
                Lap lap = new Lap
                {
                    LapId = Guid.NewGuid().ToString(),
                    CompetitorId = currentCompetitor.CompetitorId,
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
        }

        protected override void OnLapCompleted(MsgLapCompleted msg)
        {
            Leaderboard = msg.Leaderboard;
        }

        protected override void OnNewConnection(MsgNewConnection msg)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnNewConnection Called {msg.CarId}");
            //}

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
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnNewSession Called {msg.SessionType}");
            //}
            PluginManager.EnableRealtimeReport((ushort)(PluginManager.RealtimeUpdateInterval * 5));

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
                Weather = msg.Weather,
                SessionGame = "Assetto Corsa"
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
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnProtocolVersion Called {msg.Version}");
            //}
        }

        protected override void OnServerError(MsgError msg)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnServerError Called {msg.ErrorMessage}");
            //}
        }

        protected override void OnSessionEnded(MsgSessionEnded msg)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnSessionEnded Called");
            //}

            CurrentSession.IsActive = false;

            var endSession = ApiWrapperNet4.Post<Session>("session/endSession", CurrentSession);
        }

        protected override void OnSessionInfo(MsgSessionInfo msg)
        {
            //using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            //using (var b = new StreamWriter(a))
            //{
            //    b.WriteLine($"{DateTime.Now.TimeOfDay}: OnSessionInfo Called {JsonConvert.SerializeObject(msg)}");
            //}
            PluginManager.EnableRealtimeReport((ushort)(PluginManager.RealtimeUpdateInterval * 5));

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
                    Weather = msg.Weather,
                    SessionGame = "Assetto Corsa"
                };

                CurrentSession = ApiWrapperNet4.Post<Session>("session/addsession", CurrentSession);
            }
        }

        public void AddSector(string driverName, int sectorTime, eSector sector)
        {
            try
            {
                switch (sector)
                {
                    case eSector.Sector1:
                        var driver = Competitors.FirstOrDefault(comp => comp.DriverName == driverName);
                        var currentLap = new Lap
                        {
                            LapId = Guid.NewGuid().ToString(),
                            CompetitorId = driver.CompetitorId,
                            ConnectionId = driver.ConnectionId,
                            Sector1 = sectorTime
                        };
                        while (CurrentLaps.Any(laps => laps.CompetitorId == driver.CompetitorId))
                        {
                            var delLap = CurrentLaps.FirstOrDefault(laps => laps.CompetitorId == driver.CompetitorId);
                            CurrentLaps.Remove(delLap);
                        }

                        CurrentLaps.Add(currentLap);
                        var addLap = ApiWrapperNet4.Post<Lap>("lap/addlap", currentLap);
                        break;
                    case eSector.Sector2:
                        var driver2 = Competitors.FirstOrDefault(comp => comp.DriverName == driverName);
                        CurrentLaps.FirstOrDefault(lap => lap.CompetitorId == driver2.CompetitorId).Sector2 = sectorTime;
                        var addLap2 = ApiWrapperNet4.Post<Lap>("lap/editlap", CurrentLaps.FirstOrDefault(lap => lap.CompetitorId == driver2.CompetitorId));
                        break;
                    case eSector.Sector3:
                        var driver3 = Competitors.FirstOrDefault(comp => comp.DriverName == driverName);
                        CurrentLaps.FirstOrDefault(lap => lap.CompetitorId == driver3.CompetitorId).Sector3 = sectorTime;
                        var addLap3 = ApiWrapperNet4.Post<Lap>("lap/editlap", CurrentLaps.FirstOrDefault(lap => lap.CompetitorId == driver3.CompetitorId));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
