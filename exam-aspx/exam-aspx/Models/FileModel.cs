using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;
using System.Data.Odbc;


namespace exam_aspx.Models
{
    public class FileModel:BaseModel
    {
        public FileEntity[] getFiles(int start=0,int end=10)
        {
            var cmd = buildCommand("select * from file order by time desc limit ?,?");
            cmd.AddIntParam("strat", start);
            cmd.AddIntParam("end", end);
            var result = new List<FileEntity>();
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                FileEntity tmp = new FileEntity();
                tmp.id = reader.GetInt32(0);
                tmp.name = reader.GetString(1);
                tmp.path = reader.GetString(2);
                tmp.time = reader.GetDateTime(3);
                tmp.size = reader.GetInt32(4);
                result.Add(tmp);
            }
            return result.ToArray();
        }

        public int addFile(string name,string path,int size)
        {
            var cmd = buildCommand("insert into file(name,path,size,time) values(?,?,?,now())");
            cmd.AddVarcharParam("name", name);
            cmd.AddVarcharParam("path", path);
            cmd.AddIntParam("size", size);
            return cmd.ExecuteNonQuery();
        }

        public int deleFile(int id)
        {
            var cmd = buildCommand("delete from file where id=?");
            cmd.AddIntParam("id", id);
            return cmd.ExecuteNonQuery();
        }

        public int getTheNumberOfFile()
        {
            var cmd = buildCommand("select count(*) from file");
            var reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public FileEntity getFileById(int id)
        {
            FileEntity file = null;
            var cmd = buildCommand("select * from file where id=?");
            cmd.AddIntParam("id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                file = new FileEntity();
                file.id = reader.GetInt32(0);
                file.name = reader.GetString(1);
                file.path = reader.GetString(2);
                file.time = reader.GetDateTime(3);
                file.size = reader.GetInt32(4);
            }
           return file;
        }
    }
}