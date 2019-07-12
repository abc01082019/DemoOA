using MAP.OA.EFDAL;
using MAP.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.DALFactory
{
    /// <summary>
    /// 把DAL层的所有类接口封装到一个类中
    /// </summary>
    public class DbSession: IDbSession
    {
        #region 简单工厂抽象工厂部分
        public IUserInfoDal UserInfoDal
        {
            get { return StaticDalFactory.GetUserInfoDal(); }
        }
        public IOrderInfoDal OrderInfoDal
        {
            get { return StaticDalFactory.GetOrderInfoDal(); }
        } 
        #endregion

        /// <summary>
        /// 拿到当前EF的上下文，然后进行 把修改实体进行一个整体提交.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }

    }
}
