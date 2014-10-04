using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exam_aspx.Controllers
{
    public class StudentController : Controller
    {
        private int getSid()
        {
            if (Session["student"] == null)
                return -1;
            return (int)Session["student"];
        }

        [HttpGet]
        public ActionResult Index()
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Exam()
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Result()
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult File()
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["student"] = null;
            return Redirect("/Index/Index");
        }
    }
}
