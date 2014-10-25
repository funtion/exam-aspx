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
            Session["exam_info"] = exam;
            var question = model.genExam(id);
            Session["exam"] = question;
            
            if (Session["end_time"] == null) 
            {
                var endTime = System.DateTime.Now.AddMinutes(exam.time);
                Session["end_time"] = endTime;
                Session["strat_time"] = DateTime.Now;
            }

            Session["remain_time"] =  (int) ( ( (DateTime)Session["end_time"] ).Subtract(DateTime.Now).TotalSeconds );
            ViewBag.reamin_time = Session["remain_time"];
            
            ViewBag.questions = question;
            ViewBag.exam = exam;
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
                return Json("{status:error,message:考试时间已过}");
            }
            else
            {
                var sc = new List<Dictionary<string,string>>();
                var mc = new List<Dictionary<string, string>>();
                var tf = new List<Dictionary<string, string>>();
                double scScore = 0, mcScore = 0, tfScore = 0;
                var data = Request.Params["data"];
                var ansList = new JavaScriptSerializer().Deserialize< ArrayList>(data);
                var question = Session["exam"] as List<QuestionEntity>;
                var exam_info = (ExamEntity)Session["exam_info"];

                foreach (Dictionary<String,Object> ans in ansList)
                {
                    int id = (int)ans["id"];
                    var correctAns = question[id].ans;
                    var getAns = ans["choice"] as String;

                    var tmp_dic = new Dictionary<string, string>();
                    tmp_dic["problem_id"] = String.Format("{0}",ans["id"]);
                    tmp_dic["choice"] = (string)ans["choice"];
                    tmp_dic["ans"] = correctAns;
                    tmp_dic["is_correct"] = "false";
                    if (question[id].type == "SC")
                    {
                        String tmp = ( (char)(Int32.Parse(getAns) + 'A')).ToString();
                        if (tmp == correctAns)
                        {
                            scScore += exam_info.sScore;
                            tmp_dic["is_correct"] = "true";
                        }
                        sc.Add(tmp_dic);

                    }
                    else if (question[id].type == "MC")
                    {
                        var l = from c in getAns.Split(';') orderby c select ( (char)(Int32.Parse(c) + 'A')).ToString() ;
                        var tmp = String.Join("", l);
                        if (tmp == correctAns)
                        {
                            mcScore +=exam_info.mScore;
                            tmp_dic["is_correct"] = "true";
                        }
                        mc.Add(tmp_dic);

                    }
                    else
                    {
                        if (correctAns.ToLower() == getAns.ToLower())
                        {
                            tfScore+=exam_info.tScore;
                            tmp_dic["is_correct"] = "true";
                        }
                        tf.Add(tmp_dic);
                    }
                }

                var resultModel = new ResultModel();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                resultModel.insertResult(getSid(), exam_info.id, serializer.Serialize(sc), serializer.Serialize(mc), serializer.Serialize(tf), scScore, mcScore, tfScore);
                return Json(String.Format("{{status:'success',redirct:'/Student/ResultDetail/{0}'}}", resultModel.lastId()) );

            }
           
        }
    }
}
