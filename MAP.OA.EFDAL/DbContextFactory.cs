using MAP.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.EFDAL
{
    /// <summary>
    /// 从数据层中获取EF上下文对象来对表进行操作
    /// </summary>
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            // 抽象编程
            // 将返回类型改成抽象的，这时外部调用这个类时，不会在意里面返回的是什么子类
            //      在类里面可以随时改成其他数据库
            // 这时调用这个类的也是以基类来调用的
            // 上下文可以随时切换

            //return new DataModelContainer();

            // 一次请求用一个实例
            // 少创建实例，节省时间
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new DataModelContainer();
                CallContext.SetData("DbContext", db);
            }
            return db;
        }
    }
}
