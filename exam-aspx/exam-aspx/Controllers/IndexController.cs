using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using exam_aspx.Entity;
using exam_aspx.Models;


namespace exam_aspx.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            var sid      = Request.Params["sid"];
            var password = Request.Params["password"];
            StudentModel model = new StudentModel();
            int studentID = model.login(sid, password);
            if( studentID != -1)
            {
                Session["student"] = studentID;
                if (Request.Params["rememberme"] != null && Request.Params["rememberme"] != "")
                    Session.Timeout = 60 * 24 * 7;// one week
                else
                    Session.Timeout = 300; // 5 hours
                return Redirect("/Student");
            }
            else
            {
                ViewBag.login_failed = true ;
                //return Redirect("index");
                return View("Index");
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

    }
}
