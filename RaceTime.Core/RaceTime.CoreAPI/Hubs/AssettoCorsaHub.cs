using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceTime.CoreAPI.Hubs
{
    public class AssettoCorsaHub : Hub
    {
        public override Task OnConnected()
        {
            // Set connection id for just connected client only
            return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
        }

        // Server side methods called from client
        public Task Subscribe(int groupID)
        {
            return Groups.Add(Context.ConnectionId, groupID.ToString());
        }

        public Task Unsubscribe(int groupID)
        {
            return Groups.Remove(Context.ConnectionId, groupID.ToString());
        }

        public void Try()
        {
            Console.WriteLine("Trying");
        }
    }
}
