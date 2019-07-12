using MAP.OA.EFDAL;
using MAP.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.DALFactory
{
    /// <summary>
    /// 从数据库中获取数据接口并封装
    /// 返回一个以抽象 IUserInfoDal 包装的 UserInfo
    ///             通过UserInfo类来调用UserInfo 'CRUP'函数
    /// ======================================================
    /// 简单工厂            vs           抽象工厂
    /// 还是需要改代码                 不需要改代码了，该配置就行
    /// </summary>
    public class StaticDalFactory
    {
        #region 简单工厂
        /// <summary>
        /// 简单工厂
        /// </summary>
        /// <returns></returns>
        public static IUserInfoDal _GetUserInfoDal()
        {
            // 新问题: 还是需要 new 方法
            // 解决方案: 直接在Config中设置
            return new UserInfoDal();
        }

        public static IOrderInfoDal _GetOrderInfoDal()
        {
            return new OrderInfoDal();
        }
        #endregion

        public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];

        #region 抽象工厂
        /// <summary>
        /// 完全去掉new
        /// </summary>
        /// <returns></returns>
        public static IUserInfoDal GetUserInfoDal()
        {
            //return new UserInfoDal();
            //return new NHUserInfoDal();

            // 把上面的new去掉，希望改一个配置那么实例就发生变化, 就不需要该代码
            // 使用的是一个反射,这时注意要在Portal中添加程序集，不然我们就需要给定一个绝对路径
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal") as IUserInfoDal;
        }

        public static IOrderInfoDal GetOrderInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal") as IOrderInfoDal;
        } 
        #endregion

    }
}
