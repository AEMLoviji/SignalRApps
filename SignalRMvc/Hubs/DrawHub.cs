using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRDraw.Models;

namespace SignalRDraw
{
    public class DrawHub : Hub
    {
        public void Send(Data data)
        {
            Clients.AllExcept(Context.ConnectionId).addLine(data);
        }
    }
}