using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;

namespace exam_aspx.Models
{
    public class TitleModel:BaseModel
    {
        public TitleEntity getTitle()
        {
            var cmd = buildCommand("select * from title order by id desc limit 1");
            var reader = cmd.ExecuteReader();
            reader.Read();
            TitleEntity result = new TitleEntity();
            result.title = reader.GetString(1);
            result.subtitle = reader.GetString(2);
            return result;
        }

        public int setTitle(string title,string subTitle)
        {
            var cmd = buildCommand("insert into title('title','sub_title') values(?,?)");
            cmd.AddVarcharParam("title", title);
            cmd.AddVarcharParam("sub_title", subTitle);
            var result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}