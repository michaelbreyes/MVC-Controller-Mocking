using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllerMock.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserInfo()
        {
            var name = Request.QueryString["Name"];
            if (name == "Mock") return Content("Ok");

            return Content("Not ok");
        }

        public ActionResult ModifySession()
        {
            Session["IsModified"] = true;

            return Content("Ok");
        }

        [HttpPost]
        public ActionResult ReceiveFormData()
        {
            var name = Request.Form["Name"];
            if (name == "Mock") return Content("Ok");

            return Content("Not ok");
        }
    }
}