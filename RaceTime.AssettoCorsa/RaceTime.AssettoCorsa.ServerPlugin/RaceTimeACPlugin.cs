using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using acPlugins4net;
using acPlugins4net.info;
using acPlugins4net.messages;
using System.IO;

namespace RaceTime.AssettoCorsa.ServerPlugin
{
    public class RaceTimeACPlugin : AcServerPlugin
    {
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
            using (var a = new FileStream("Test.txt", FileMode.Append, FileAccess.Write))
            using (var b = new StreamWriter(a))
            {
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnCarUpdate Called {driverInfo.TotalTime} {driverInfo.Gap}");
            }
            base.OnCarUpdate(driverInfo);
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
                b.WriteLine($"{DateTime.Now.TimeOfDay}: OnLapCompleted Called: {TimeSpan.FromMilliseconds(msg.Laptime).ToString()}");
            }
        }

        protected override void OnLapCompleted(MsgLapCompleted msg)
        {
            base.OnLapCompleted(msg);
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
            base.OnSessionEnded(msg);
        }

        protected override void OnSessionInfo(MsgSessionInfo msg)
        {
            base.OnSessionInfo(msg);
        }
    }
}
