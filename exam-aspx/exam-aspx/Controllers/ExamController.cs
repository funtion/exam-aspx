using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using exam_aspx.Controllers;
using exam_aspx.Models;


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

            return View();
        }

    }
}
