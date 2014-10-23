using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using exam_aspx.Controllers;
using exam_aspx.Models;
using System.Collections;
using exam_aspx.Entity;


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
            if(exam == null || exam.ready == 0)
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
            if (exam == null || exam.ready == 0)
                return Redirect("/Index/Index");
            var question = model.genExam(id);
            Session["exam"] = question;
            var endTime = System.DateTime.Now.AddMinutes(exam.time);
            Session["end_time"] = endTime;
            ViewBag.questions = question;
            return View();
        }

        [HttpPost]
        public ActionResult Submit()
        {
            int score = 0;
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
                var question = Session["exam"] as List<QuestionEntity>;
                
                foreach (Dictionary<String,Object> a in ans)
                {
                    int id = (int)a["id"];
                    var correctAns = question[id].ans;
                    var getAns = a["choice"] as String;
                    if (question[id].type == "SC")
                    {
                        String tmp = ( (char)(Int32.Parse(getAns) + 'A')).ToString();
                        if (tmp == correctAns)
                        {
                            score++;
                        }
                    }
                    else if (question[id].type == "MC")
                    {
                        var l = from c in getAns.Split(';') orderby c select ( (char)(Int32.Parse(c) + 'A')).ToString() ;
                        var tmp = String.Join("", l);
                        if (tmp == correctAns)
                        {
                            score++;
                        }

                    }
                    else
                    {
                        if (correctAns == getAns)
                        {
                            score++;
                        }
                    }
                }
                
            }
            return Json(score);
        }




    }
}
