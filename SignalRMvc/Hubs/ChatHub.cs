using Microsoft.AspNet.SignalR;
using SignalRMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SignalRMvc.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();

        /// <summary>
        /// Used to send new message
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        /// <summary>
        /// Used to add new connected user
        /// </summary>
        /// <param name="userName"></param>
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                // Sending message to current user
                Clients.Caller.onConnected(id, userName, Users);

                // Sending message to all users, except this one
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        /// <summary>
        /// Disconnecting user, when browser will be refreshed or closed
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
