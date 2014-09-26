using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exam_aspx.Entity
{
    public class StudentEntity
    {
        public string sid; 
        public string name;
        public string password;
        public StudentEntity(string sid,string name,string password)
        {
            this.name = name;
            this.sid = sid;
            this.password = password;
        }
    }
}