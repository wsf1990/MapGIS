using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.Common
{
    /// <summary>
    /// 用于控制exe软件或者其他进程等
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 翻墙软件
        /// </summary>
        private static Process Process_Psiphon;
        /// <summary>
        /// 打开exe文件
        /// </summary>
        /// <param name="path"></param>
        public static Process OpenExe(string path)
        {
            return Process.Start(path);
        }

        /// <summary>
        /// 关闭exe
        /// </summary>
        /// <param name="process"></param>
        public static void Close(Process process)
        {
            process.CloseMainWindow();
            process.Close();
            process.Dispose();
            //Process.
        }

        public static void OpenP3(string path)
        {
            Process_Psiphon = OpenExe(path);
        }

        public static void CloseP3()
        {
            //psiphon3.exe
            //psiphon3-plonk.exe
            //psiphon3-polipo.exe
            Close(Process_Psiphon);
        }
        /// <summary>
        /// 判断360是否打开，并关闭
        /// </summary>
        /// <returns></returns>
        public static bool CheckAndClose360()
        {
            var process = Process.GetProcessesByName("360se").ToList();//根据进程名称获取其进程   去掉后缀名
            if (process.Count > 0)
            {
                process.ForEach(Close);
                return true;
            }
            return false;
        }
    }
}
