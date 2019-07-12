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
            IUserInfoDal userInfoDal = new UserInfoDal("x");
            userInfoDal.Show();

            Console.WriteLine("---------1---------");

            //下面用容器来创建UserInfoDal的实例

            // 第一步: 初始化容器
            IApplicationContext ctx = ContextRegistry.GetContext();

            // 创建对象
            IUserInfoDal efdal = ctx.GetObject("EFUserInfoDal") as IUserInfoDal;
            efdal.Show();

            Console.WriteLine("--------2----------");

            // xml配置文件中配置的文件
            IUserInfoDal dal = ctx.GetObject("UserInfoDal") as IUserInfoDal;
            dal.Show();

            Console.WriteLine("---------3---------");

            // 
            UserInfoService userInfoServive = ctx.GetObject("UserInfoService") as UserInfoService;
            userInfoServive.Show();

            Console.WriteLine("---------4---------");

            Console.ReadKey();
        }
    }
}
