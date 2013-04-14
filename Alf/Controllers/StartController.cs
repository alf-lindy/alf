using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alf.Controllers
{
    public class StartController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Teachers() 
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult EventInfo() 
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Competition()
        {
            return View();
        }
    }
}
