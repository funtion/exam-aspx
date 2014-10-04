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
                Session.Timeout = 300;
                return View();
            }
            else
            {
                ViewBag.login_failed = true ;
                return View("Index");
            }

        }

    }
}
