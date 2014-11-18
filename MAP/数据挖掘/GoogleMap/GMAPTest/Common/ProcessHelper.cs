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

        #region 1. 线程基本操作
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
        }
        /// <summary>
        /// 关闭exe
        /// </summary>
        /// <param name="name"></param>
        public static void Close(string name)
        {
            var process = Process.GetProcessesByName(name).ToList();//根据进程名称获取其进程   去掉后缀名
            if (process.Count > 0)
            {
                process.ForEach(Close);
            }
        }
        /// <summary>
        /// 判断线程是否存在
        /// </summary>
        /// <param name="name"></param>
        public static bool CheckIsAlive(string name)
        {
            var process = Process.GetProcessesByName(name).ToList();//根据进程名称获取其进程   去掉后缀名
            return process.Count > 0;
        } 
        #endregion

        #region 2. 翻墙软件
        /// <summary>
        /// 翻墙软件
        /// </summary>
        private static Process Process_Psiphon;

        public static void OpenP3(string path)
        {
            Process_Psiphon = OpenExe(path);
        }

        public static void CloseP3()
        {
            //psiphon3.exe
            //psiphon3-plonk.exe
            //psiphon3-polipo.exe
            if (Process_Psiphon == null)
            {
                Close("psiphon3");
                Close("psiphon3-plonk");
                Close("psiphon3-polipo");
            }
            else
                Close(Process_Psiphon);
        } 
        #endregion

        #region 3. 360
        /// <summary>
        /// 判断360是否打开，并关闭
        /// </summary>
        /// <returns></returns>
        public static bool CheckAndClose360()
        {
            bool b = CheckIsAlive("360se");
            if (b)
                Close("360se");
            return b;
        } 
        #endregion
    }
}
