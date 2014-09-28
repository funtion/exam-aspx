using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using exam_aspx.Entity;

namespace exam_aspx.Models
{
    
    public class StudentModel:BaseModel
    {
        
        
        public bool login(string sid,string password)
        {

            OdbcCommand command = new OdbcCommand("select * from student where sid = ? and password = ?",connection);
            
            command.AddParam("sid",OdbcType.VarChar,sid);
            command.AddParam("password",OdbcType.VarChar,password);
            OdbcDataReader reader =  command.ExecuteReader();
            return reader.HasRows;
        }

        public void register(StudentEntity student)
        {
            OdbcCommand command = new OdbcCommand("insert into student(sid,name,password) values(?,?,?)",connection);
            command.Parameters.Add(new OdbcParameter("sid", OdbcType.VarChar)).Value = student.sid ;
            command.Parameters.Add(new OdbcParameter("name", OdbcType.VarChar)).Value = student.name;
            command.Parameters.Add(new OdbcParameter("password", OdbcType.VarChar)).Value = student.password;
            command.ExecuteNonQuery();
        }

    }
}