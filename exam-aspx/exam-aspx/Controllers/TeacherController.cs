using System;
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
            
            return View();
        }

        [HttpPost]
        public ActionResult AddExam()
        {
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
                        int examId = examModel.addExam(examEntity.time, examEntity.sNumber, examEntity.mNumber, examEntity.tNumber, examEntity.sScore, examEntity.mScore, examEntity.tScore);
                        int type = -1;
                        int count = 1;
                        foreach (Question q in examEntity.sc)
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
                        foreach (Question q in examEntity.mc)
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
                        foreach (Question q in examEntity.tf)
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

       
    }
}
