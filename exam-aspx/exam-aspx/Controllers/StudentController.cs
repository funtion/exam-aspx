using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using exam_aspx.Models;
using exam_aspx.Entity;


using System.Web.Script.Serialization;

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
            AnnouncementModel announceModel = new AnnouncementModel();
            ExamModel examModel = new ExamModel();
            FileModel fileModel = new FileModel();

            ViewBag.announcement = announceModel.getAnnouncements(0, 3);
            ViewBag.exam = examModel.getAvailableExam().Take(3);
            ViewBag.file = fileModel.getFiles(0, 3);
            return View();
        }
        [HttpGet]
        public ActionResult Announcement()
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            AnnouncementModel model = new AnnouncementModel();
            ViewBag.data = model.getAnnouncements(0, model.getNumberOfDisplayAnnouncement()); //get all
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
            ExamModel model = new ExamModel();
            ViewBag.data = model.getAvailableExam();

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
            var decoder = new JavaScriptSerializer();
            ViewBag.sQuestion = decoder.Deserialize< List< Dictionary<string,string> > >(data.sQuestion);
            ViewBag.mQuestion = decoder.Deserialize<List<Dictionary<string, string>>>(data.mQuestion);
            ViewBag.tQuestion = decoder.Deserialize<List<Dictionary<string, string>>>(data.tQuestion);
            ViewBag.data = data;
            return View();
        }
        [HttpGet]
        public ActionResult File(int start=0,int size=10)
        {
            int sid = getSid();
            if (sid == -1)
            {
                return Redirect("/Index/Index");
            }
            var model = new FileModel();
            ViewBag.count = model.getTheNumberOfFile();
            ViewBag.data = model.getFiles(0, ViewBag.count);
            ViewBag.size = size;

            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["student"] = null;
            return Redirect("/Index/Index");
        }

        [HttpPost]
        public ActionResult Register()
        {
            StudentModel studentModel = new StudentModel();
            LimitUserModel limituserModel = new LimitUserModel();
            try
            {
                var name = Request.Form["name"];
                var sid = Request.Form["sid"];
                var password = Request.Form["password"];
                if (name == "" || sid == "" || password == "")
                {
                    ViewBag.err = "请完整填写信息";
                    return View("~/Views/Index/Register.cshtml");
                }
                if (!limituserModel.isAllowed(sid))
                {
                    ViewBag.err = "学号被禁止注册";
                    return View("~/Views/Index/Register.cshtml");
                }

                var student = new StudentEntity() {sid=sid,name=name,password=password };
                var res = studentModel.register(student);
                if (res != 1)
                {
                    ViewBag.err = "注册失败，请检查所填信息";
                    return View("~/Views/Index/Register.cshtml");
                }
                else
                {
                    return View("~/Views/Index/Index.cshtml");
                }

            }
            catch (Exception e)
            {
                ViewBag.err = "注册失败，请检查所填信息";
                return View("~/Views/Index/Register.cshtml");
            }
        }
    }
}
