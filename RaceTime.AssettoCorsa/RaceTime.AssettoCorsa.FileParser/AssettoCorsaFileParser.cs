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
using RaceTime.AssettoCorsa.Common.Helpers;
using RaceTime.AssettoCorsa.ServerPlugin;
using RaceTime.Common.Enums;

namespace RaceTime.AssettoCorsa.FileParser
{
    public class AssettoCorsaFileParser
    {
        private FileStream fs;
        private StreamReader sr;
        private int count = 0;
        private Session session;
        private RaceTimeACPlugin raceTimePlugin;

        public AssettoCorsaFileParser(RaceTimeACPlugin raceTimePlugin)
        {
            this.raceTimePlugin = raceTimePlugin;
        }

        public void StartFileParser()
        {
            var fileAlive = true;

           // if (session == null) session = new Session();

            var file = Directory.GetFiles(@"E:\Games\Steam\steamapps\common\assettocorsa\server\logs\session").LastOrDefault();

            fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            sr = new StreamReader(fs);

            string line;
            line = sr.ReadToEnd();

            while (fileAlive)
            {
                if ((line = sr.ReadLine()) != null)
                {
                    switch (line)
                    {
                       
                        case "TCP packet 88":        //Split Completed Event
                            AddSector(sr, line);
                            break;
                        case "TCP packet 80":        //Tyre Change event     
                            UpdateCurrentTyre(sr, line);
                            break;
                        case "TCP packet 73":        //Lap Completed Event                          
                        case "TCP packet 130":       //Collision Event                           
                        case "TCP packet 67":        //Driver Left Event                        
                        case "NextSession":          //Next Session Event                             
                        case "REQ":                  //Server Name event                             
                        case "TCP packet 63":        //Connection Event                          
                        default:                     //Check default values                        
                            break;
                    }                    
                }
                else
                {
                    count++;
                    Thread.Sleep(1000);
                }

                string lastfile = Directory.GetFiles(@"E:\Games\Steam\steamapps\common\assettocorsa\server\logs\session").LastOrDefault();
                if (lastfile != file)
                {
                    sr.Close();
                    fs.Close();
                    file = lastfile;
                    fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    sr = new StreamReader(fs);
                }
            }

            //StartFileWatcher();

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

            var time = int.Parse(line);

            raceTimePlugin.AddSector(driverName, time, sector);
            
        }

        private void UpdateCurrentTyre(StreamReader sr, string line)
        {
            line = sr.ReadLine();
            var carName = line.GetCarName();
            var driverName = line.GetDriverName();

            line = sr.ReadLine();          
            line = line.Remove(0, line.LastIndexOf(' ') + 1);

            var tyre = line;

            raceTimePlugin.UpdateDriverTyreType(driverName, tyre);
         
        }
        
    }
}
