using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using exam_aspx.Models;
using exam_aspx.Entity;

namespace exam_aspx.Controllers
{
    public class ProjectController : Controller
    {
        const string COURSE = "course";
        const string YEAR = "year";
        const string HOMEWORK = "homework";
        const string STUDENT = "student";

        

        [HttpGet]
        public ActionResult Index()
        {
            
            var model = new ProjectModel();
            ViewBag.href = "/Project/Index/?course=";
            if (Request.Params[COURSE] != null)
            {
                ViewBag.href += Request.Params[COURSE] + "&year=";
                if (Request.Params[YEAR] != null)
                {
                    ViewBag.href += Request.Params[YEAR] + "&homework=";
                    if (Request.Params[HOMEWORK] != null)
                    {
                        ViewBag.href = "/Project/Info/?" + COURSE + "=" + Request.Params[COURSE] + "&" + YEAR + "=" + Request.Params[YEAR] + "&" + HOMEWORK + "=" + Request.Params[HOMEWORK]+"&"+STUDENT+"=";
                        ViewBag.data = model.getAllProjectStudent(Request.Params[COURSE], Request.Params[YEAR], Request.Params[HOMEWORK]);
                        
                    }
                    else
                    {
                        
                        ViewBag.data = model.getAllHomeWork(Request.Params[COURSE], Request.Params[YEAR]);
                    }
                }
                else
                {
                    
                    ViewBag.data = model.getAllYears(Request.Params[COURSE]);
                }
            }
            else
            {
                ViewBag.data = model.getAllCources();
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult Info()
        {
            var model = new ProjectModel();
            string course = Request.Params[COURSE];
            string year = Request.Params[YEAR];
            string homeork = Request.Params[HOMEWORK];
            string student = Request.Params[STUDENT];
            ViewBag.data = model.getAllProject(course, year, homeork, student);
            string jar = ViewBag.data.classFileUrl;

            var mainClass = jar.Split('/');
            var actualName = mainClass[mainClass.Length - 1];
            ViewBag.MainClass = actualName.Split('.')[0];

            //ViewBag.MainClass = jar.Substring(0, jar.Length - 3) + "class";
            
            string codepath = ViewBag.data.code;
            codepath = Server.MapPath(codepath);

            ViewBag.data.codetext = System.IO.File.ReadAllText(codepath);
            //ViewBag.MainClass = codepath.Replace(".java","").Replace(@"\", ".");

            return View();
        }
    }
}
