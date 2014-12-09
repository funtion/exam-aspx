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
        //
        // GET: /Project/
        [HttpGet]
        public ActionResult Index()
        {
            
            var model = new ProjectModel();
            if (Request.Params[COURSE] != "")
            {
                if (Request.Params[YEAR] != "")
                {
                    if (Request.Params[HOMEWORK] != "")
                    {
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
            return View();
        }
    }
}
