using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using exam_aspx.Controllers;
using exam_aspx.Models;
using System.Collections;

namespace exam_aspx.Controllers
{
    public class ExamController : Controller
    {
        private int getSid()
        {
            if (Session["student"] == null)
                return -1;
            return (int)Session["student"];
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            if (getSid() == -1)
            {
                return Redirect("/Index/Index");
            }
            var model = new  ExamModel();
            var exam = model.getExamById(id);
            if(exam == null)
                return Redirect("/Index/Index");

            ViewBag.exam = exam;
            return View();
        }


        [HttpGet]
        public ActionResult Exam(int id)
        {
            if (getSid() == -1)
            {
                return Redirect("/Index/Index");
            }
            var model = new ExamModel();
            var exam = model.getExamById(id);
            if (exam == null)
                return Redirect("/Index/Index");
            var question = model.genExam(id);
            Session["exam"] = question;
            var endTime = System.DateTime.Now.AddMinutes(exam.time);
            Session["end_time"] = endTime;
            return View();
        }

        [HttpPost]
        public ActionResult Submit()
        {
            if (getSid() == -1)
            {
                return Redirect("/Index/Index");
            }
            var endTime = (DateTime)Session["end_time"];

            if (System.DateTime.Now.CompareTo(endTime.AddMinutes(1)) > 0) // 多等一分钟以免网络延迟的影响
            {
                ViewBag.err = "考试时间已过";
            }
            else
            {
                var data = Request.Params["data"];
                var ans = new JavaScriptSerializer().Deserialize< ArrayList>(data);





            }


            return View();
        }




    }
}
