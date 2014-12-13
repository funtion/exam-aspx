using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;

namespace exam_aspx.Models
{
    public class ProjectModel :BaseModel
    {
        const int ID = 0, COURSE = 1, YEAR = 2, HOMEWORK = 3, IMG_URL = 4, DESCRIPTION = 5, CLASS_FILE_URL=6,CODE = 7, STUDENT = 8;
        public string[] getAllCources()
        {
            var sql = buildCommand("select distinct course from project ");
            var reader = sql.ExecuteReader();
            var res = new List<string>();
            while (reader.Read())
            {
                res.Add(reader.GetString(0));
            }
            return res.ToArray();

        }
      
        
        public string[] getAllYears(string course)
        {
            var sql = buildCommand("select distinct year from project where course=?");
            sql.AddVarcharParam("course", course);
            var reader = sql.ExecuteReader();
            var res = new List<string>();
            while (reader.Read())
            {
                res.Add(reader.GetString(0));
            }
            return res.ToArray();
        }
        public string[] getAllHomeWork(string course, string year)
        {
            var sql = buildCommand("select homework from project where course=? and year=?");
            sql.AddVarcharParam("course", course);
            sql.AddVarcharParam("year", year);
            var reader = sql.ExecuteReader();
            var res = new List<string>();
            while (reader.Read())
            {
                res.Add(reader.GetString(0));
            }
            return res.ToArray();
        }

        public string[] getAllProjectStudent(string course, string year, string homework)
        {
            var sql = buildCommand("select * from project where course=? and year=? and homework=?");
            sql.AddVarcharParam("course", course);
            sql.AddVarcharParam("year", year);
            sql.AddVarcharParam("homework", homework);
            var reader = sql.ExecuteReader();
            var res = new List<string>();
            while (reader.Read())
            {
                res.Add(reader.GetString(STUDENT));
            }
            return res.ToArray();
        }


        public ProjectEntity getAllProject(string course, string year, string homework,string student)
        {
            var sql = buildCommand("select * from project where course=? and year=? and homework=? and student=?");

            sql.AddVarcharParam("course", course);
            sql.AddVarcharParam("year", year);
            sql.AddVarcharParam("homework", homework);
            sql.AddVarcharParam("student",student);
            var reader = sql.ExecuteReader();
            ProjectEntity res = new ProjectEntity();
            if (reader.HasRows)
            {
                reader.Read();
                 res = new ProjectEntity
                {

                    id = reader.GetInt32(ID),
                    course = reader.GetString(COURSE),
                    year = reader.GetString(YEAR),
                    homework = reader.GetString(HOMEWORK),
                    imgUrl = reader.GetString(IMG_URL),
                    description = reader.GetString(DESCRIPTION),
                    classFileUrl = reader.GetString(CLASS_FILE_URL),
                    code = reader.GetString(CODE),
                    student = reader.GetString(STUDENT)
                };
            }
            

            return res;

        }


    }
}