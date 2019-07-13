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

        static LogHelper()
        {
            // 从队列中获取错误信息写到 日志文件中
            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    string str = ExceptionStringQueue.Dequeue();
                    // 把异常写到日志中
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
