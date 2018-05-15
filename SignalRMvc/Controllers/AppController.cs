using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRMvc.Controllers
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
    }
}