using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Diagnostics;

namespace exam_aspx.Controllers
{
    public class Function
    {

        public static string Execute(string path, string command)
        {
            Process process = null;
            ProcessStartInfo startInfo = null;
            string output = ""; //输出字符串  
            string err = "";
            if (command != null && !command.Equals(""))
            {
                // Process process = new Process();//创建进程对象  
                startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                //startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                startInfo.RedirectStandardInput = true;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.RedirectStandardError = true;
                startInfo.CreateNoWindow = true;//不创建窗口      

                

                try
                {
                    process = Process.Start(startInfo);

                    var cdPath = string.Format("cd {0}", path);
                    if (path.LastIndexOf(':') != -1)
                    {
                        
                        process.StandardInput.WriteLine(path.Substring(0,path.IndexOf(':')+1));
                    }
                    process.StandardInput.WriteLine(cdPath);
                    process.StandardInput.WriteLine(command);
                    process.StandardInput.WriteLine("exit");
                    err = process.StandardError.ReadToEnd();
                    output = process.StandardOutput.ReadToEnd();//读取进程的输出

                }
                catch (Exception e)
                {
                    if (process != null)
                        process.Close();
                }
                finally
                {
                    if (process != null)
                    {
                        //process.StandardInput.WriteLine("exit");
                        process.Close();
                    }
                }

            }
            return err;
        }
        public static string Execute(string command, int seconds=0)
        {
            Process process = null;
            ProcessStartInfo startInfo = null;
            string output = ""; //输出字符串  
            string err = "";
            if (command != null && !command.Equals(""))
            {
               // Process process = new Process();//创建进程对象  
                startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                //startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                startInfo.RedirectStandardInput = true;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.RedirectStandardError = true;
                startInfo.CreateNoWindow = true;//不创建窗口      
               
        
               
                try{
                        process = Process.Start(startInfo);         
                        process.StandardInput.WriteLine(command);
                        process.StandardInput.WriteLine("exit");
                        err = process.StandardError.ReadToEnd();
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出
                        
                }
                catch(Exception e)
                {
                    if (process != null)
                        process.Close();
                }
                finally
                {
                    if (process != null)
                    {
                        //process.StandardInput.WriteLine("exit");
                        process.Close();
                    }
                }
                
            }
            return err;
        }  

      


    }
}