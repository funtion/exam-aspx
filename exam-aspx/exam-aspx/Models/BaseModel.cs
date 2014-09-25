using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace exam_aspx.Models
{
    public class BaseModel
    {
        protected OdbcConnection connection { get; set; }
        public BaseModel()
        {
            String connectStr = "DRIVER={MySQL ODBC 5.2 Unicode Driver};Database=exam;Server=localhost;UID=user08;PWD=user08;";
            connection = new OdbcConnection(connectStr);
            connection.Open();
        }
    }
}