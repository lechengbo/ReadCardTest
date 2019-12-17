using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTest.Service
{
    public static class Utility
    {
        public static List<string> WhereStartWith(List<string> list, string content)
        {
            List<string> newList = new List<string>();
            foreach (var item in list)
            {
                if (item.StartsWith(content))
                {
                    newList.Add(item);
                }
            }

            return newList;
        }
        public static List<string> WhereContains(List<string> list, string content)
        {
            List<string> newList = new List<string>();
            foreach (var item in list)
            {
                if (item.Contains(content))
                {
                    newList.Add(item);
                }
            }

            return newList;
        }

        public static List<string> AddRangeExtension(List<string> list, List<string> list2)
        {
            foreach (var item in list2)
            {
                list.Add(item);
            }
            return list;
        }

        public static bool IsNullOrWhiteSpace(string target)
        {
            if (target.Equals(null) || target.Equals(string.Empty) || target.Length == 0 || target.Trim(' ').Length == 0)
            {
                return true;
            }
            return false;
        }

        public static string ExecCMD(string command)
        {
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            pro.StartInfo.FileName = "cmd.exe";
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.CreateNoWindow = true;
            //pro.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pro.Start();
            pro.StandardInput.WriteLine(command);
            pro.StandardInput.WriteLine("exit");
            //pro.StandardInput.WriteLine("\n");
            pro.StandardInput.AutoFlush = true;
            //获取cmd窗口的输出信息
            string output = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();//等待程序执行完退出进程
            pro.Close();
            return output;

        }

    }
}
