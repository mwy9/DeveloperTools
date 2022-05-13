using System;
using System.Diagnostics;

//CSharp监控外部进程运行状态
//ProcessMonitorHelper.MonitorProcess("steam");


namespace LibraryProcess
{
    public class ProcessMonitorHelper
    {
        private static Process[] MyProcesses;

        /// <summary>
        /// 代码可放在定时器中循环检测监控的程序是否启动
        /// 需要外挂一个程序用于监控其他程序运行状态，一旦检测到另一程序关闭就触发事件处理
        /// </summary>
        /// <param name="strProcessName"></param>
        public static void MonitorProcess(string strProcessName)
        {
            MyProcesses = Process.GetProcessesByName(strProcessName);//需要监控的程序名，该方法带出该程序所有用到的进程
            foreach (Process myprocess in MyProcesses)
            {
                if (myprocess.ProcessName.ToLower() == strProcessName)
                {
                    Console.WriteLine("Process running!");
                    myprocess.EnableRaisingEvents = true;//设置进程终止时触发的时间
                    myprocess.Exited += new EventHandler(myprocess_Exited);//发现外部程序关闭即触发方法myprocess_Exited
                }
            }
        }

        private static void myprocess_Exited(object sender, EventArgs e)//被触发的程序
        {
            Console.WriteLine("Process Exit!");
        }
    }
}
