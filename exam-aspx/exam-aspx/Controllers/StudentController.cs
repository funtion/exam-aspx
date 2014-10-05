using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using exam_aspx.Models;
using exam_aspx.Entity;


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
        public ActionResult Index()//公告
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            AnnouncementModel model = new AnnouncementModel();
            var data = model.getAnnouncements();
            ViewData["announcement"]=data;
            return View();
        }

        [HttpGet]
        public ActionResult Announcement(int id)
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            AnnouncementModel model = new AnnouncementModel();
            var data = model.getAnnouncementByID(id);
            if (data == null)
                return Redirect("/Student/Index");
            ViewBag.data = data;
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
            var model = new ResultModel();
            var data = model.getResultByStudentId(sid);
            ViewBag.data = data;
            return View();
        }
        [HttpGet]
        public ActionResult ResultDetail(int id)
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            var model = new ResultModel();
            var data = model.getResultById(id);
            ViewBag.data = data;
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
            var model = new FileModel();
            ViewBag.data = model.getFiles();
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
