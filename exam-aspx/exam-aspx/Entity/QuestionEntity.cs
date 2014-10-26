using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Drawing;

namespace exam_aspx.Entity
{
    public class QuestionEntity
    {
        public string statement;
        public string ans;
        public ArrayList choices = new ArrayList();
        public string type;
        public Image image;
        public String imageURL;
        public int id;
    }
}