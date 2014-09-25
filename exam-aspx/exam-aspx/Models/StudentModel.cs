using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;


namespace exam_aspx.Models
{
    public class StudentModel:BaseModel
    {
        public bool login(string sid,string password)
        {

            OdbcCommand command = new OdbcCommand("select * from student where sid=? and password=?",connection);
            command.Parameters.Add(new OdbcParameter("sid", OdbcType.VarChar)).Value = sid;
            command.Parameters.Add(new OdbcParameter("password", OdbcType.VarChar)).Value = password;
            command.Prepare();
            OdbcDataReader reader =  command.ExecuteReader();
            return reader.HasRows;
        }
    }
}