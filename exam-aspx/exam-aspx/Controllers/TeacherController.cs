﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using exam_aspx.Models;
using exam_aspx.Entity;
using System.Drawing;
using System.IO;

namespace exam_aspx.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        [HttpGet]
        public ActionResult Index()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            ExamModel model = new ExamModel();
            List<ExamEntity> list = model.getAllExam();
            ViewBag.examList = list;
            return View();
        }
        [HttpGet]
        public ActionResult Announcement()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            AnnouncementModel model = new AnnouncementModel();
            List<AnnouncementEntity> list = model.getAllAnnouncements();
            ViewBag.AnnouncementList = list;
            return View();
        }
        [HttpGet]
        public ActionResult File()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
           FileModel model = new FileModel();
           FileEntity[] fArray = model.getFiles(0, model.getTheNumberOfFile());
           ViewBag.fileList = fArray;
            return View();
        }
        [HttpGet]
        public ActionResult Student()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            StudentModel model = new StudentModel();
            ViewBag.studentList =  model.getAllStudents();
            return View();
        }
        [HttpGet]
        public ActionResult Result()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            ViewBag.resultList = new List<ResultEntity>();
            try
            {
                int examid = int.Parse(Request["exam"]);
                ResultModel model = new ResultModel();
                ExamModel examModel = new ExamModel();
                ViewBag.resultList = model.getExamResultByExamId(examid);
                ViewBag.examInfo = examModel.getExamById(examid);
                
            }
            catch
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
           
             return View();
           
            
        }
        [HttpGet]
        public ActionResult Logout()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Session["teacher"] = null;
            return Redirect("Login");
        }

        [HttpGet]
        public ActionResult LimitUser()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            LimitUserModel model = new LimitUserModel();
            ViewBag.limituserList = model.getAllLimitUser();
            return View();
        }

        [HttpPost]
        public ActionResult delResult()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, String> ret = new Dictionary<string, string>();
            try
            {
                int id = int.Parse(Request["id"]);
                ResultModel model = new ResultModel();
                int row = model.delResultByExamId(id);
                if (row > 0)
                {
                    ret.Add("status", "success");
                }
                else
                {
                    ret.Add("status", "failed");
                    ret.Add("error", "delete error!");
                }

            }
            catch
            {
                ret.Add("status", "failed");
                ret.Add("error", "bad param!");
            }
            return Json(ret);
        }
        [HttpPost]
        public ActionResult delLimitUser()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, String> ret = new Dictionary<string, string>();
            try
            {
                int id = int.Parse(Request["id"]);
                LimitUserModel model = new LimitUserModel();
                int row = model.delLimitUser(id);
                if(row==1){
                    ret.Add("status","success");
                }
                else{
                    ret.Add("status","failed");
                    ret.Add("error","delete error!");
                }

            }
            catch{
                ret.Add("status","failed");
                ret.Add("error","bad param!");
            }
            return Json(ret);
        }

        [HttpPost]
        public ActionResult CheckLogin()
        {
           
                var user = Request["user"];
                var pass = Request["pass"];
                TeacherModel model = new TeacherModel();
                TeacherEntity teacher = model.getTeacher(user, pass);
                if (teacher != null)
                {
                    Session["teacher"] = teacher;
                    Session.Timeout = 60 * 5;
                    return Redirect("Index");
                }
                return Redirect("Login");


        }
        [HttpPost]
        public ActionResult AddLimitUser()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, String> ret = new Dictionary<string, string>();
            try
            {
                string sid = Request["sid"];
                LimitUserModel model = new LimitUserModel();
                int row = model.addLimitUser(sid);
                if (row > 0)
                {
                    ret.Add("status", "success");

                }
                else
                {
                    ret.Add("status", "failed");
                    ret.Add("error", "add error");
                }
            }
            catch (Exception e)
            {
                ret.Add("status", "failed");
                ret.Add("error", "bad param!");
            }
            return Json(ret);

        }
        [HttpPost]
        public ActionResult delStudent()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, String> ret = new Dictionary<string, string>();
            try
            {
                int id = int.Parse(Request["id"]);
                StudentModel model = new StudentModel();
                int row = model.delStudentById(id);
                if (row > 0)
                {
                    ret.Add("status", "success");
                }
                else
                {
                    ret.Add("status", "failed");
                    ret.Add("error", "delete error!");
                }
            }
            catch (Exception e)
            {
                ret.Add("status", "failed");
                ret.Add("error", "bad param!");
            }
            return Json(ret);

        }
        [HttpPost]
        public ActionResult delFile()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, String> ret = new Dictionary<string, string>();
            try
            {
                int id = int.Parse(Request["id"]);
                exam_aspx.Models.FileModel model = new exam_aspx.Models.FileModel();
                FileEntity file = model.getFileById(id);
                if (file != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(file.path)))
                    {
                        System.IO.File.Delete(Server.MapPath(file.path));
                    }
                }
                int row = model.deleFile(id);
                if (row > 0)
                {
                    ret.Add("status", "success");
                    
                }
                else
                {
                    ret.Add("status", "failed");
                    ret.Add("error","delete failed!");
                }
                
            }
            catch (Exception e)
            {
                ret.Add("status", "failed");
                ret.Add("error", "bad param");
            }
            return Json(ret);
        }
        [HttpPost]
        public ActionResult addFile()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, String> ret = new Dictionary<string, string>();
            HttpPostedFileBase file = Request.Files.Get("file");
            string savepath = "/upload/file";
            if (file != null)
            {
                if (!Directory.Exists(Server.MapPath(savepath)))
                {
                    Directory.CreateDirectory(Server.MapPath(savepath));
                }
                file.SaveAs(Server.MapPath(string.Format("{0}/{1}", savepath, file.FileName)));
                try
                {
                    exam_aspx.Models.FileModel model = new exam_aspx.Models.FileModel();
                    model.addFile(file.FileName, string.Format("{0}/{1}", savepath, file.FileName), file.ContentLength);
                    ret.Add("status", "success");
                }
                catch (Exception e)
                {
                    ret.Add("status", "failed");
                    ret.Add("error", "insert error!");
                }
                
                
            }
            else
            {
                ret.Add("status", "failed");
                ret.Add("error", "no file!");
            }
            return Json(ret);
        }
        [HttpPost]
        public ActionResult AddAnnouncement()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, string> response = new Dictionary<string, string>();
            AnnouncementModel model = new AnnouncementModel();
            try
            {
                string title = Request["title"];
                string content = Request["content"];
                int i = model.addAnnouncement(title, content);
                if (i > 0)
                {
                    response.Add("status", "success");
                }
                else{
                    response.Add("status","failed");
                    response.Add("error", "insert error");
                }
            }
            catch
            {
                response.Add("status", "failed");
                response.Add("error", "bad param");
            }
            return Json(response);
        }
        [HttpPost]
        public ActionResult DelAnnouncement()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, string> response = new Dictionary<string, string>();
            AnnouncementModel model = new AnnouncementModel();
            try
            {
                int id = int.Parse(Request["id"]);
                int i = model.delAnnouncement(id);
                if (i > 0)
                {
                    response.Add("status", "success");
                }
                else
                {
                    response.Add("status", "failed");
                    response.Add("error", "delete error");
                }
            }
            catch
            {
                response.Add("status", "failed");
                response.Add("error", "bad param");
            }
            return Json(response);
        }
        [HttpPost]
        public ActionResult UpdateAnnouncement()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, string> response = new Dictionary<string, string>();
            AnnouncementModel model = new AnnouncementModel();
            try
            {
                int id = int.Parse(Request["id"]);
                string title = Request["title"];
                string content = Request["content"];
                int i = model.updateAnnouncement(id,title,content);
                if (i > 0)
                {
                    response.Add("status", "success");
                }
                else
                {
                    response.Add("status", "failed");
                    response.Add("error", "update error");
                }
            }
            catch
            {
                response.Add("status", "failed");
                response.Add("error", "bad param");
            }
            return Json(response);
        }
        [HttpPost]
        public ActionResult ModifyExamStatus()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, string> response = new Dictionary<string, string>();
            ExamModel examModel = new ExamModel();
            int id, status;
            try
            {
               id = int.Parse(Request["id"]);
                status = int.Parse(Request["status"]);
               int tmp = examModel.modifyStatus(id, status);
                if (tmp > 0)
                {
                   response.Add("status", "success");
 
               }
                else
                {
                    response.Add("status", "failed");
                    response.Add("error", "something wrong!");
                }
            }
            catch
            {
                response.Add("status", "failed");
                response.Add("error", "bad param!");
            }
            return Json(response);
       }

        [HttpPost]
        public ActionResult setExamName()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string, string> response = new Dictionary<string, string>();
            ExamModel model = new ExamModel();
            try
            {
                //singleNum=" + singleNum + "&mutilNum=" + mutilNum + "&judgeNum=" + judgeNum + "&singleScore=" + singleScore + "&mutilScore=" + mutilScore + "&judgeScore=" + judgeScore + "&time=" + time,
                int id = int.Parse(Request["id"]);
                string name = Request["name"];
                int sNum = int.Parse(Request["singleNum"]);
                int mNum = int.Parse(Request["mutilNum"]);
                int tNum = int.Parse(Request["judgeNum"]);
                double sScore = double.Parse(Request["singleScore"]);
                double mScore = double.Parse(Request["mutilScore"]);
                double tScore = double.Parse(Request["judgeScore"]);
                int time = int.Parse(Request["time"]);

               // int row = model.setExamName(id, name);
                int row = model.setExamName(id, name, sNum, mNum, tNum, sScore, mScore, tScore, time);
                if (row > 0)
                {
                    response.Add("status", "success");

                }
                else
                {
                    response.Add("status", "failed");
                    response.Add("error", "update error!");
                }
            }
            catch
            {
                response.Add("status", "failed");
                response.Add("error", "bad param!");
            }
            return Json(response);
        }

        [HttpPost]
        public ActionResult DeleteExam()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string,string> response = new Dictionary<string,string>();
            ExamModel examModel = new ExamModel();
            QuestionModel questionModel = new QuestionModel();
            int id;
            try
            {
                id = int.Parse(Request["id"]);
                if (Directory.Exists(Server.MapPath(string.Format("~/upload/{0}", id))))
                {
                    Directory.Delete(Server.MapPath(string.Format("~/upload/{0}", id)),true);
                }
                int questionCol = questionModel.deleteQuestionByExamId(id);
                int examCol = examModel.deleteExam(id);
                if (questionCol > 0 && examCol > 0)
                {
                    response.Add("status", "success");
                }
                else
                {
                    response.Add("status", "failed");
                    response.Add("error", "something wrong when delete the record from database!");
                }
            }
            catch 
            {
                response.Add("status", "failed");
                response.Add("error", "bad id!");
            }
            return Json(response);
        }

        [HttpPost]
        public ActionResult AddExam()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            Dictionary<string,String> ret = new Dictionary<string,string>();
            HttpPostedFileBase file =  Request.Files.Get("doc");
            if (file != null)
            {
                if (checkFileExt(file.FileName))
                {
                    ExamModel examModel= new ExamModel();
                    QuestionModel questionModel = new QuestionModel();
                    var docName = "~/upload/tmp.docx";
                    file.SaveAs(Server.MapPath(docName));
                    try
                    {
                        ExamEntity examEntity = examModel.praseFromDoc(Server.MapPath(docName));
                        int examId = examModel.addExam(examEntity.time, examEntity.sNumber, examEntity.mNumber, examEntity.tNumber, examEntity.sScore, examEntity.mScore, examEntity.tScore,0,file.FileName.Substring(0,file.FileName.Length-5));
                        int type = -1;
                        int count = 1;
                        foreach (QuestionEntity q in examEntity.sc)
                        {
                            
                            switch (q.type)
                            {
                                case "SC":
                                    type = 0;
                                    break;
                                case "MC":
                                    type = 1;
                                    break;
                                case "TF":
                                    type = 2;
                                    break;
                            }
                             var imageDir = "";
                             var imageName = "";
                             if (q.image != null)
                             {
                                 imageName = string.Format("/{0}.jpg", count);
                                 imageDir = string.Format("~/upload/{0}/sc", examId, count);
                                 if(!Directory.Exists(Server.MapPath(imageDir)))
                                 {
                                     Directory.CreateDirectory(Server.MapPath(imageDir));
                                 }
                                
                                 q.image.Save(Server.MapPath(imageDir+imageName));
                                 count = count + 1;
                             }
                           
                            var choiceJson = new JavaScriptSerializer().Serialize(q.choices);
                            questionModel.addQuesiton(type, q.ans, choiceJson, imageDir+imageName, q.statement, examId);
                        }
                        count = 1;
                        foreach (QuestionEntity q in examEntity.mc)
                        {

                            switch (q.type)
                            {
                                case "SC":
                                    type = 0;
                                    break;
                                case "MC":
                                    type = 1;
                                    break;
                                case "TF":
                                    type = 2;
                                    break;
                            }
                             var imageDir = "";
                             var imageName = "";
                             if (q.image != null)
                             {
                                 imageName = string.Format("/{0}.jpg",count);
                                 imageDir = string.Format("~/upload/{0}/mc", examId, count);
                                 if(!Directory.Exists(Server.MapPath(imageDir)))
                                 {
                                     Directory.CreateDirectory(Server.MapPath(imageDir));
                                 }
                                
                                 q.image.Save(Server.MapPath(imageDir+imageName));
                                 count = count + 1;
                             }
                            
                            var choiceJson = new JavaScriptSerializer().Serialize(q.choices);
                            questionModel.addQuesiton(type, q.ans, choiceJson, imageDir+imageName, q.statement, examId);
                        }
                        count = 1;
                        foreach (QuestionEntity q in examEntity.tf)
                        {

                            switch (q.type)
                            {
                                case "SC":
                                    type = 0;
                                    break;
                                case "MC":
                                    type = 1;
                                    break;
                                case "TF":
                                    type = 2;
                                    break;
                            }
                             var imageDir = "";
                             var imageName = "";
                             if (q.image != null)
                             {
                                 imageName = string.Format("/{0}.jpg", count);
                                 imageDir = string.Format("~/upload/{0}/tf", examId, count);
                                 if(!Directory.Exists(Server.MapPath(imageDir)))
                                 {
                                     Directory.CreateDirectory(Server.MapPath(imageDir));
                                 }
                                
                                 q.image.Save(Server.MapPath(imageDir+imageName));
                                 count = count + 1;
                             }
                            
                            var choiceJson = new JavaScriptSerializer().Serialize(q.choices);
                            questionModel.addQuesiton(type, q.ans, choiceJson, imageDir+imageName, q.statement, examId);
                        }
                    

                    }
                    catch(Exception e)
                    {
                        System.IO.File.Delete(Server.MapPath(docName));
                        ret.Add("status", "failed");
                        ret.Add("error", "parse doc error");
                        return Json(ret);
                    }
                    System.IO.File.Delete(Server.MapPath(docName));
                    ret.Add("status", "success");
                    ret.Add("filename", file.FileName);
                    return Json(ret);
                }
                else
                {
                    
                    ret.Add("status", "failed");
                    ret.Add("error", "file type not allowed");
                    return Json(ret);
                }
            }
            else
            {
              
                ret.Add("status", "failed");
                ret.Add("error", "no files");
                return Json(ret);
            }
           
        }
        public bool checkFileExt(string filename)
        {
            bool ret = false;
            
            if (filename.LastIndexOf(".") > 0 && filename.LastIndexOf(".") < filename.Length-1)
            {
               var  ext = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - 1 -filename.LastIndexOf("."));
               
               if (ext == "docx")
               {
                   ret = true;
               }
            }

            return ret;
        }

        public bool loginStatus()
        {
            if (Session["teacher"] == null)
                return false;
            else
            {
                Session.Timeout = 60 * 5;
                return true;
            } 
                

        
        }

       
    }
}
