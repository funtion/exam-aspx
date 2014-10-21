using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;

namespace exam_aspx.Models
{
    public class TeacherModel:BaseModel
    {

        public TeacherEntity getTeacher(string username, string password)
        {
            var cmd = buildCommand("select * from teacher where user=? and password=?");
            cmd.AddVarcharParam("user", username);
            cmd.AddVarcharParam("password", password);
            var reader = cmd.ExecuteReader();
            if (reader.Read()) {
                TeacherEntity teacher = new TeacherEntity();
                teacher.id = reader.GetInt32(0);
                teacher.username = reader.GetString(1);
                teacher.password = reader.GetString(2);
                return teacher;
            }
            return null;
        }
    }
}