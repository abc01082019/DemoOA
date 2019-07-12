using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserInfoDal userInfoDal = new UserInfoDal();
            userInfoDal.Show();

            //下面用容器来创建UserInfoDal的实例

            // 第一步: 初始化容器
            IApplicationContext ctx = ContextRegistry.GetContext();

            // 创建对象
            IUserInfoDal dal = ctx.GetObject("UserInfoDal") as IUserInfoDal;
            dal.Show();

            Console.ReadKey();
        }
    }
}
