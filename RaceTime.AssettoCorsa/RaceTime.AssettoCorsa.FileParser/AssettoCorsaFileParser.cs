using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RaceTime.Common.Models;
using RaceTime.Common.Common;
using RaceTime.AssettoCorsa.FileParser.Common;
using System.Globalization;
using RaceTime.AssettoCrosa.Common.Helpers;

namespace RaceTime.AssettoCorsa.FileParser
{
    public class AssettoCorsaFileParser
    {
        private FileStream fs;
        private StreamReader sr;
        private int count = 0;
        private Session session;

        public AssettoCorsaFileParser()
        {

        }

        public void StartFileParser()
        {
           // var test = ApiWrapperNet4.Get<object>("values");
             //var test2 = ApiWrapperNet4.Post<object>("test/addlap", new Lap());

            if (session == null) session = new Session();

            var file = Directory.GetFiles(@"E:\Games\Steam\steamapps\common\assettocorsa\server\logs\session").LastOrDefault();

            fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            sr = new StreamReader(fs);

            string line;

            while (true)
            {
                if ((line = sr.ReadLine()) != null)
                {
                    switch (line)
                    {
                        case "TCP packet 63":        //Connection Event
                            AddCompetitor(sr, line);
                            break;
                        case "TCP packet 88":        //Split Completed Event
                            AddSector(sr, line);
                            break;
                        case "TCP packet 73":        //Lap Completed Event
                            LapCompleted(sr, line);
                            break;
                        case "TCP packet 130":       //Collision Event
                            break;
                        case "TCP packet 67":        //Driver Left Event
                            break;
                        case "TCP packet 80":        //Tyre Change event     
                            break;
                        case "NextSession":          //Next Session Event 
                            break;
                        case "REQ":                  //Server Name event    
                            GetServerDetails(sr, line);
                            break;
                        default:                     //Check default values
                            GetSessionData(sr, line);
                            break;
                    }

                    using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
                    using (var b = new StreamWriter(a))
                    {
                        b.WriteLine($"{count}: {line}");
                    }
                }
                else
                {
                    count++;
                    Thread.Sleep(1000);
                }
            }

            //StartFileWatcher();

        }

        private void GetSessionData(StreamReader sr, string line)
        {
            if (!line.Contains("SENDING session name")) return;
            string serverName = session.ServerName;

            session = new Session();
            session.Competitors = new List<Competitor>();
            session.SessionName = line.Remove(0, 23);
            session.SessionID = sr.ReadLine().Remove(0, 24);
            session.ServerName = serverName;
            session.SessionType = (eSessionType)(int.Parse(sr.ReadLine().Remove(0, 23)) - 1);
            session.SessionTime = int.Parse(sr.ReadLine().Remove(0, 23));
            session.SessionLaps = int.Parse(sr.ReadLine().Remove(0, 23));

        }

        private void GetServerDetails(StreamReader sr, string line)
        {
            line = sr.ReadLine();
            line = line.Remove(line.IndexOf('['));
            line = line.Remove(line.LastIndexOf(' '));

            session.Track = line.Substring(line.LastIndexOf(' ') + 1);
            line = line.Remove(line.LastIndexOf(' '));
            line = line.Remove(line.LastIndexOf(' '));
            line = line.Remove(line.LastIndexOf(' '));

            line = line.Remove(0, line.IndexOf(' ') + 1);
            line = line.Remove(0, line.IndexOf(' ') + 1);
            line = line.Remove(0, line.IndexOf(' ') + 1);

            session.ServerName = line;
        }

        private void AddCompetitor(StreamReader sr, string line)
        {
            //Dispatching TCP message to bmw_m3_e92 (1) [MaxChal []]
            line = sr.ReadLine();

            var carName = line.GetCarName();

            var driverName = line.GetDriverName();

            if (session.Competitors.Any(comp => comp.CompetitorName.ToUpper() == driverName.ToUpper() && comp.CarName.ToUpper() == carName.ToUpper()))
                session.Competitors.FirstOrDefault(comp => comp.CompetitorName.ToUpper() == driverName.ToUpper() && comp.CarName.ToUpper() == carName.ToUpper()).ActiveDriver = true;
            else            
                session.Competitors.Add(new Competitor
                {
                    ActiveDriver = true,
                    CarName = carName,
                    CompetitorName = driverName,
                    Laps = new List<Lap>(),
                    LapsCompleted = 0,
                    LapsLead = 0,
                    Position = 0
                });

        }

        private void AddSector(StreamReader sr, string line)
        {
            line = sr.ReadLine();
            var carName = line.GetCarName();
            var driverName = line.GetDriverName();

            line = sr.ReadLine();
            line = line.Replace("Car.onSplitCompleted ","");
            line = line.Remove(0, line.IndexOf(' ') + 1);
            var sector = (eSector)int.Parse(line.Substring(0, 1));

            line = line.Remove(0, line.IndexOf(' ') + 1);

            var time = TimeSpan.FromMilliseconds(int.Parse(line));

            if (sector == eSector.Sector1)
                session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.Add(new Lap
                {
                    Sector1 = time,
                    TyreCompound = session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).CurrentTyre                    
                });
            else if (sector == eSector.Sector2)
                session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().Sector2 = time;
            else if (sector == eSector.Sector3)
                session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().Sector3 = time;
        }

        private void LapCompleted(StreamReader rs, string line)
        {
            line = sr.ReadLine();
            var carName = line.GetCarName();
            var driverName = line.GetDriverName();

            sr.ReadLine();
            var laptime = sr.ReadLine().Replace($"LAP {driverName} ", "");

            var mintues = int.Parse(laptime.Substring(0, laptime.IndexOf(':')));
            laptime = laptime.Remove(0, laptime.IndexOf(':') + 1);
            var seconds = int.Parse(laptime.Substring(0, laptime.IndexOf(':')));
            laptime = laptime.Remove(0, laptime.IndexOf(':') + 1);
            var milis = int.Parse(laptime);

            var lapTimeSpan = new TimeSpan(0, 0, mintues, seconds, milis);

            int splitCount = int.Parse(sr.ReadLine().Replace("SPLIT COUNT: ", ""));

            sr.ReadLine();
            sr.ReadLine();
            line = sr.ReadLine();

            if (line.Contains("Invalid"))
                session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().IsValid = false;
            else
                session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().IsValid = true;


            session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).LapsCompleted++;
            session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().LapTime = lapTimeSpan;
            session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().LapNumber = 
                session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).LapsCompleted;

            switch (splitCount)
            {
                case 2:
                    session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().Sector2 = 
                        lapTimeSpan - session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().Sector1;
                    break;
                case 3:
                    session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().Sector3 =
                        lapTimeSpan - session.Competitors.FirstOrDefault(comp => comp.CompetitorName == driverName && comp.CarName == carName).Laps.LastOrDefault().Sector2;
                    break;
                default:
                    break;
            }
        }



    }
}
