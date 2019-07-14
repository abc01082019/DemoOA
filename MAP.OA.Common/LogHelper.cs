using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAP.OA.Common
{
    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        public static List<ILogWriter> LogWriterList = new List<ILogWriter>();

        static LogHelper()
        {
            LogWriterList.Add(new Log4NetWriter());


            // 从队列中获取错误信息写到 日志文件中
            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    if (ExceptionStringQueue.Count > 0)
                    {
                        string str = ExceptionStringQueue.Dequeue();
                        // 把异常写到日志中

                        foreach(var logWriter in LogWriterList)
                        {
                            logWriter.WriteLogInfo(str);
                        }
                    }
                }
            });
        }

        public static void WriteLog(string exceptionText)
        {
            lock(ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(exceptionText);
            }
        }
    }
}
