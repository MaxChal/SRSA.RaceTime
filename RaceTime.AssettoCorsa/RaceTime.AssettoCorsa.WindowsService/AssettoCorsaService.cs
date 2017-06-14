using acPlugins4net;
using RaceTime.AssettoCorsa.FileParser;
using RaceTime.AssettoCorsa.ServerPlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceTime.AssettoCorsa.WindowsService
{
    partial class AssettoCorsaService : ServiceBase
    {
        private AssettoCorsaFileParser parser;

        public AssettoCorsaService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            RaceTimeACPlugin raceTimePlugin = new RaceTimeACPlugin();

            AcServerPluginManager pluginManager = new AcServerPluginManager();
            pluginManager.LoadInfoFromServerConfig();
            pluginManager.AddPlugin(raceTimePlugin);
            pluginManager.LoadPluginsFromAppConfig();
            pluginManager.Connect();

            parser = new AssettoCorsaFileParser(raceTimePlugin);
            parser.FileAlive = true;
            Thread thread = new Thread(new ParameterizedThreadStart(StartFileParser));
            thread.Start(parser);

        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            parser.FileAlive = false;
        }

        private static void StartFileParser(object parser)
        {
            var fileParser = (AssettoCorsaFileParser)parser;
            fileParser.StartFileParser();
        }
    }
}
