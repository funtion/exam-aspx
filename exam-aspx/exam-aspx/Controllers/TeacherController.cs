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
using System.Diagnostics;

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
        public ActionResult Project()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            var model = new ProjectModel();
            ViewBag.Course = model.getAllCources();
            ViewBag.Years = model.getDistinctYears();
            ViewBag.Homeworks = model.getDistinctHomeWork();
            return View();
        }
        [HttpGet]
        public ActionResult Profile()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            TeacherEntity teacher = (TeacherEntity)Session["teacher"];
            ViewBag.user = teacher.username;
            return View();
        }

        [HttpPost]
        public ActionResult ModifyProfile()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            var response = new Dictionary<string, string>();
            var username = Request.Params["username"];
            var oldpass = Request.Params["oldpass"];
            var newpass = Request.Params["newpass"];
            if (username == null || oldpass == null || newpass == null)
            {
                response.Add("status", "failed");
                response.Add("error", "bad param");
            }
            var model = new TeacherModel();
            TeacherEntity OldTeacher = (TeacherEntity)Session["teacher"];
            var teacher = model.getTeacher(OldTeacher.username, oldpass);
            if (teacher == null)
            {
                response.Add("status", "failed");
                response.Add("error", "wrong oldpass");
                return Json(response);
            }
            int row = model.ChangePass(OldTeacher.username, username, newpass);
            if (row != 1)
            {
                response.Add("status", "failed");
                response.Add("error", "update error");
                return Json(response);
            }
            if (OldTeacher.username != username)
            {
                Logout();
            }
            response.Add("status", "success");
            return Json(response);
        }

        [HttpPost]
        public ActionResult AddProject()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            var response = new Dictionary<string, string>();
            HttpPostedFileBase file = Request.Files.Get("java");
            var baseSavePath = "~/upload/project";
            var savePath = "";
            if (file != null)
            {
                HttpPostedFileBase image = Request.Files.Get("image");
                if (image!=null&&!checkFileExt(image.FileName, "jpg"))
                {//检查图片后缀
                    response.Add("status", "failed");
                    response.Add("error", "only jpg allowed!");
                    return Json(response);
                }
                if (checkFileExt(file.FileName, "java"))
                {
                    var course = Request.Params["course"];
                    var year = Request.Params["year"];
                    var homework = Request.Params["homework"];
                    var student = Request.Params["student"];
                    var description = Request.Params["description"];
                    var visible = Request.Params["visible"];
                    if (course != null && year != null && homework != null&&student!=null)
                    {
                        savePath = string.Format("{0}/{1}/{2}/{3}/{4}", baseSavePath, course, year, homework,student);
                        



                        if (Directory.Exists(Server.MapPath(savePath)))//检查目录是否存在，存在则递归删除目录
                        {
                            var model = new ProjectModel();
                            var project = model.getAllProject(course,year,homework,student);
                            if (project.id > 0)  //数据库中存在则删除数据
                            {
                                model.DelProject(course, year, homework, student);
                            }
                            Directory.Delete(Server.MapPath(savePath), true);
                        }
                        Directory.CreateDirectory(Server.MapPath(savePath));
                        //储存图片文件
                        
                        var img_url = "";
                        if (image != null)
                        { //是否存在图片，存在则保存图片
                            img_url = string.Format("{0}/{1}", savePath, image.FileName);
                            
                            image.SaveAs(Server.MapPath(img_url));
                        }

                        //储存java文件
                        file.SaveAs(Server.MapPath(string.Format("{0}/{1}", savePath, file.FileName)));
                        try
                        {
                                var err = "";
                                //编译java文件
                                //var buildClass = string.Format("javac {0}/*.java", Server.MapPath(savePath));
                                Process process = new Process();
                                process.StartInfo = new ProcessStartInfo();
                               
                                process.StartInfo.RedirectStandardInput = true;    
                                process.StartInfo.RedirectStandardError = true;
                                process.StartInfo.RedirectStandardOutput = true;
                                process.StartInfo.WorkingDirectory = Server.MapPath(savePath);
                                process.StartInfo.FileName = "cmd";
                                process.StartInfo.UseShellExecute = false;

                                process.Start();
                                
                                process.StandardInput.WriteLine("javac *.java");
                                
                                process.StandardInput.WriteLine(string.Format("jar cf {0}.jar *.class",file.FileName));

                                process.StandardInput.WriteLine("exit");
                                
                                try
                                {
                                    if (!process.StandardError.EndOfStream)
                                    {
                                        err += "\nstderr\n" + process.StandardError.ReadToEnd();
                                        if (!process.StandardOutput.EndOfStream)
                                            err += "\nstdout\n" + process.StandardOutput.ReadToEnd();
                                    }
                                      

                                }
                                catch (Exception e)
                                {
                                    //ignore it 
                                }

                                // += Function.Execute(buildClass);
                                //打包class文件
                               // var buildJar = string.Format("jar cf {0}/{1}.jar {0}/*.class", Server.MapPath(savePath), file.FileName, Server.MapPath(savePath));

                               // err += Function.Execute(buildJar);


                                
                                

                                if (err != "")
                                {
                                    Directory.Delete(Server.MapPath(savePath), true);
                                    response.Add("status", "failed");
                                    response.Add("error", "some thing wrong with your code"+err);
                                    return Json(response);
                                }
                                
                                int defaultVisible = 1;
                                if(visible!=null){
                                    defaultVisible = int.Parse(visible);
                                }
                                
                                var model = new ProjectModel();
                                var jar_url = string.Format("{0}/{1}.jar",savePath,file.FileName);
                                var code_url = string.Format("{0}/{1}", savePath, file.FileName);
                                var row = model.AddProject(course, year, homework, student, code_url, jar_url, img_url, description, defaultVisible);
                                if (row != 1)
                                {
                                    response.Add("status", "failed");
                                    response.Add("error", "insert error");
                                }
                                
                                
                        }
                        catch (Exception e)
                        {
                            response.Add("status", "failed");
                            response.Add("error", "some thing wrong with your code:"+e.Message);
                            return Json(response);
                        }


                        response.Add("status", "success");
                        
                    }
                    else
                    {
                        response.Add("status", "failed");
                        response.Add("error", "course,year,homework can't be null");
                    }
                }
                else
                {
                    response.Add("status", "failed");
                    response.Add("error", "only  *.java allowed");
                }
                
            }
            else
            {
                response.Add("status", "failed");
                response.Add("error", "no file");
            }
            return Json(response);
        }
        [HttpPost]
        public ActionResult DelProject()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            var response = new Dictionary<string,string>();
            var course = Request.Params["course"];
            var year = Request.Params["year"];
            var homework = Request.Params["homework"];
            var student = Request.Params["student"];
            if (course == null || year == null || homework == null || student == null)
            {
                response.Add("status", "failed");
                response.Add("error", "bad param");
            }
            var model = new ProjectModel();
            int row = model.DelProject(course, year, homework, student);
            if (row == 0)
            {
                response.Add("status", "failed");
                response.Add("error", "delete error");
            }
            Directory.Delete(Server.MapPath(string.Format("~/upload/project/{0}/{1}/{2}/{3}",course,year,homework,student)), true);
            response.Add("status", "success");

            return Json(response);
        }

        public ActionResult ModifyProject()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            var response = new Dictionary<string, string>();
           
            var course = Request.Params["course"];
            var year = Request.Params["year"];
            var homework = Request.Params["homework"];
            var student = Request.Params["student"];
            var visible = Request.Params["visible"];
            if (course == null || year == null || homework == null || student == null || visible==null)
            {
                response.Add("status", "failed");
                response.Add("error", "bad param");
                return Json(response);
            }
            var model = new ProjectModel();
            int row = model.ModifyProjectStatus(course, year, homework, student, int.Parse(visible));
            if (row == 0)
            {
                response.Add("status", "failed");
                response.Add("error", "update failed");
                return Json(response);
            }
            response.Add("status", "success");
            return Json(response);
        }
        [HttpPost]
        public ActionResult GetProjectDetail()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            var response = new Dictionary<string,string>();
            var model = new ProjectModel();
            string course = Request.Params["course"];
            string year = Request.Params["year"];
            string homework = Request.Params["homework"];
            string student = Request.Params["student"];
            var data = model.getAllProject(course, year, homework, student);
            if (data != null)
            {
                response.Add("name", data.student);
                response.Add("description", data.description);
                response.Add("image", data.imgUrl.Split('~')[1]);
                response.Add("programa", data.classFileUrl.Split('~')[1]);
                response.Add("code", string.Format("upload/project/{0}/{1}/{2}/{3}/{4}",course,year,homework,student,data.code));
                response.Add("visible",string.Format("{0}",data.visible));
                response.Add("status", "success");
            }
            else
            {
                response.Add("status", "failed");
            }
            return Json(response);
              
        }

        [HttpPost]
        public ActionResult GetProject()
        {
            if (loginStatus() == false)
            {
                return Redirect("Login");
            }
            var root = new Dictionary<string, object>();
            var id = Request["id"];
            
            if (id == null)
            {
                return Json(root);
            }
            var dataArray = id.Split('_');
            var model = new ProjectModel();
            var list = new List<Dictionary<string, object>>();
            if (dataArray.Length == 1) //get course
            {
                
                var course = model.getAllCources();
                foreach (var c in course)
                {

                    Dictionary<string, object> node = new Dictionary<string, object>();
                    node.Add("text", c);
                    node.Add("id", string.Format("course_{0}",c));
                    node.Add("children",true);
                    list.Add(node);
                }
                root.Add("text", "PROJECT");
                root.Add("children", list);
                

            }
            else if (dataArray.Length == 2)//get year by course
            {
               var years =  model.getAllYears(dataArray[1]);
               foreach (var y in years)
               {
                   Dictionary<string, object> node = new Dictionary<string, object>();
                    node.Add("text", y);
                    node.Add("id", string.Format("course_{0}_{1}",dataArray[1],y));
                    node.Add("children",true);
                    list.Add(node);
               }
               return Json(list);
            }
            else if (dataArray.Length == 3)//get homework by course and year
            {
                var homeworks = model.getAllHomeWork(dataArray[1], dataArray[2]);
                foreach (var h in homeworks)
                {
                    Dictionary<string, object> node = new Dictionary<string, object>();
                    node.Add("text", h);
                    node.Add("id", string.Format("course_{0}_{1}_{2}", dataArray[1],dataArray[2], h));
                    node.Add("children", true);
                    list.Add(node);
                    
                }
                return Json(list);

            }
            else if(dataArray.Length==4)//get 
            {
                var students = model.getAllProjectStudent(dataArray[1], dataArray[2], dataArray[3]);
                foreach (var s in students)
                {
                    Dictionary<string, object> node = new Dictionary<string, object>();
                    node.Add("text", s);
                    node.Add("id", string.Format("course_{0}_{1}_{2}_{3}", dataArray[1], dataArray[2],dataArray[3], s));
                    //node.Add("children", true);
                    list.Add(node);

                }
                return Json(list);


            }



            return Json(root);
            
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
        public bool checkFileExt(string filename,string valid="docx")
        {
            bool ret = false;
            
            if (filename.LastIndexOf(".") > 0 && filename.LastIndexOf(".") < filename.Length-1)
            {
               var  ext = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - 1 -filename.LastIndexOf("."));
               
               if (ext == valid)
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
