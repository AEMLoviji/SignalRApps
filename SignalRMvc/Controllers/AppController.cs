using Microsoft.AspNet.SignalR;
using SignalRApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRApps.Controllers
{
    public class AppController : Controller
    {
        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Draw()
        {
            return View();
        }

        #region push
        public ActionResult Push()
        {
            var model = new List<Book>
            {
                new Book { Id = 1, Name = "Sun is shining when I am sleeping", Author = "Elvin", Price = 50 },
                new Book { Id = 2, Name = "Start over me", Author = "Elvin", Price = 49 }
            };

            return View(model);
        }

        public void AddBook()
        {
            SendMessage("Added new book");
        }

        private void SendMessage(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
            context.Clients.All.displayMessage(message);
        }

        #endregion
    }
}