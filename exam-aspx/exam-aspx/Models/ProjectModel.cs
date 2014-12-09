using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;

namespace exam_aspx.Models
{
    public class ProjectModel :BaseModel
    {
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

        public ProjectEntity[] getAllProject(string course, string year, string homework)
        {
            var sql = buildCommand("select * from project course=? and year=? and homework=?");
            sql.AddVarcharParam("course", course);
            sql.AddVarcharParam("year", year);
            sql.AddVarcharParam("homework", homework);
            var reader = sql.ExecuteReader();
            var res = new List<ProjectEntity>();
            while (reader.Read())
            {
                ProjectEntity tmp = new ProjectEntity { 
                    course = reader.GetString(1),
                    year = reader.GetString(2),
                    homework = reader.GetString(3),
                    imgUrl = reader.GetString(4),
                    description = reader.GetString(5),
                    classFileUrl = reader.GetString(6),
                    code = reader.GetString(7)
                };
                res.Add(tmp);
            }
            return res.ToArray();
        }


    }
}