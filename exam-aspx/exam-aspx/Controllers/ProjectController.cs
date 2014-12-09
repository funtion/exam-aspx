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
        //
        // GET: /Project/
        [HttpGet]
        public ActionResult Index()
        {

            var model = new ProjectModel();
            if (Request.Params["course"] != "")
            {
                if (Request.Params["year"] != "")
                {
                    if (Request.Params["homework"] != "")
                    {

                    }
                    else
                    {
                        ViewBag.data = model.getAllHomeWork(Request.Params["course"], Request.Params["year"]);
                    }
                }
                else
                {
                    ViewBag.data = model.getAllYears(Request.Params["course"]);
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
            string course = Request.Params["course"];
            string year = Request.Params["year"];
            string homeork = Request.Params["homework"];
            string student = Request.Params["student"];
            ViewBag.data = model.getAllProject(course, year, homeork, student);
            return View();
        }


    }
}
