using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exam_aspx.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            if (Request.Params["course"] != "")
            {
                if (Request.Params["year"] != "")
                {
                    if (Request.Params["homework"] != "")
                    {

                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
            return View();
        }

    }
}
