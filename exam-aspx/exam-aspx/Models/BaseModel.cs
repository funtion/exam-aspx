using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using exam_aspx.Entity;

namespace exam_aspx.Models
{
    public static class ExtendOdbc
    {
        public static void AddParam(this OdbcCommand cmd, string name, OdbcType type, Object value)
        {
            cmd.Parameters.Add(new OdbcParameter(name, type)).Value = value;
        }
    }

    public class BaseModel
    {
        protected OdbcConnection connection { get; set; }

        public BaseModel()
        {
            String connectStr = "DRIVER={MySQL ODBC 5.2 Unicode Driver};Database=exam;Server=localhost;UID=user08;PWD=user08;";
            connection = new OdbcConnection(connectStr);
            connection.Open();
        }
        public OdbcCommand buildCommand(string sql)
        {
            return new OdbcCommand(sql, connection);
        }
    }
}