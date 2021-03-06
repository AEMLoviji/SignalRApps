﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRApps.Models;

namespace SignalRApps
{
    public class DrawHub : Hub
    {
        public void Send(Data data)
        {
            Clients.AllExcept(Context.ConnectionId).addLine(data);
        }
    }
}