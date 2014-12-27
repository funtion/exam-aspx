using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using exam_aspx.Entity;
using exam_aspx.Models;

namespace exam_aspx.Controllers
{
    public class BaseController : Controller
    {
        protected int getSid()
        {
            return 0;
        }

        protected TitleEntity getTitle()
        {
            TitleModel model = new TitleModel();
            return model.getTitle();
        }

        protected void assignTitle(){

            var title = getTitle();
            ViewBag.Title = title.title;
            ViewBag.SubTitle = title.subtitle;

        }
    }
}
