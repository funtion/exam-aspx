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
        
        
        public int login(string sid,string password)
        {

            OdbcCommand command = new OdbcCommand("select * from student where sid = ? and password = ?",connection);
            
            command.AddParam("sid",OdbcType.VarChar,sid);
            command.AddParam("password",OdbcType.VarChar,password);
            OdbcDataReader reader =  command.ExecuteReader();
            if (!reader.HasRows)
                return -1;
            return reader.GetInt32(0);
        }

        public int register(StudentEntity student)
        {
            OdbcCommand command = new OdbcCommand("insert into student(sid,name,password) values(?,?,?)",connection);
            command.Parameters.Add(new OdbcParameter("sid", OdbcType.VarChar)).Value = student.sid ;
            command.Parameters.Add(new OdbcParameter("name", OdbcType.VarChar)).Value = student.name;
            command.Parameters.Add(new OdbcParameter("password", OdbcType.VarChar)).Value = student.password;
            return command.ExecuteNonQuery();
        }

        public bool isRegisted(string sid)
        {
            var cmd = buildCommand("select * from student where sid=?");
            cmd.AddParam("sid", OdbcType.VarChar, sid);
            return cmd.ExecuteReader().HasRows;
        }
        public List<StudentEntity> getAllStudents()
        {
            List<StudentEntity> list = new List<StudentEntity>();
            var cmd = buildCommand("select * from student order by sid");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                StudentEntity student = new StudentEntity();
                student.id = reader.GetInt32(0);
                student.sid = reader.GetString(1);
                student.name = reader.GetString(2);
                student.password = reader.GetString(3);
                list.Add(student);
            }
            return list;
        }
        public int delStudentById(int id)
        {
            var cmd = buildCommand("delete from student where id=?");
            cmd.AddIntParam("id", id);
            return cmd.ExecuteNonQuery();
        }
    }
}