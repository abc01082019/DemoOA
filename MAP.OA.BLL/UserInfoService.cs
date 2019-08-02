using MAP.OA.DALFactory;
using MAP.OA.EFDAL;
using MAP.OA.IBLL;
using MAP.OA.IDAL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using MAP.OA.NHDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.BLL
{
    // 模块内高内聚，模块间低耦合
    // 变化点: 1.跨数据库。有mysql，sqlserver2，数据库访问层驱动变化
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        #region old method
        // 调用数据访问层中的UserInfoDal的方法

        // 1 菜鸟到一般开发人员
        // 依赖接口编程
        //IUserInfoDal userInfoDal = new NHDAL.UserInfoDal();

        // 2 稍微高级的开发人员不这么做,如果有多种数据实现的方法(EF, NH, ...)这需要手动改 new ***
        //private IUserInfoDal userInfo = StaticDalFactory.GetUserInfoDal();

        // 3 再次进行封装到DbSession中
        //DbSession dbSession = new DbSession();
        //IDbSession dbSession = new DbSession();
        //private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession();

        // 3 更高级: Ioc, DI 依赖注入, Sprint.Net


        //public UserInfo Add(UserInfo userInfo)
        //{
        //    // UnitWork单元工作模式
        //    // 比如: 10000: 每个用户,对数据库进行交互就会变小,吞吐率就提高了.
        //    dbSession.UserInfoDal.Add(userInfo);
        //    if (dbSession.SaveChanges() > 0)
        //    {

        //    }
        //    dbSession.UserInfoDal.Add(new UserInfo());
        //    dbSession.OrderInfoDal.Delete(new OrderInfo());
        //    dbSession.OrderInfoDal.Update(new OrderInfo());
        //    dbSession.SaveChanges();    // 从数据提交的权利从数据访问层提到了业务逻辑层

        //    return null;
        //    //return dbSession.UserInfoDal.Add(userInfo);
        //} 
        #endregion

        #region 子类在构造函数中给父类属性赋值，没约束性
        //public UserInfoService()
        //{
        //    //这么写就没有约束性
        //    CurrentDal = DbSession.UserInfoDal;
        //} 
        #endregion

        #region  override base methode
        //public UserInfoService(IDbSession dbSession)
        //    :base(dbSession)
        //{
        //    this.DbSession = dbSession;
        //}

        //public override void SetCurrentDal()
        //{
        //    CurrentDal = DbSession.UserInfoDal;
        //} 
        #endregion

        public IQueryable<UserInfo> LoadPageData(Model.Param.UserQueryParam userQueryParam)
        {
            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal);

            if (!string.IsNullOrEmpty(userQueryParam.SrchName))
            {
                temp = temp.Where(u => u.UserName.Contains(userQueryParam.SrchName));
            }

            if (!string.IsNullOrEmpty(userQueryParam.SrchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.SrchRemark));
            }

            userQueryParam.Total = temp.Count();

            // Pagination
            return temp.OrderBy(u => u.Id)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();

        }

        public bool SetRole(int id, List<int> roleIds)
        {
            var user = CurrentDal.GetEntities(u => u.Id == id).FirstOrDefault();

            // remove all roles first
            user.RoleInfo.Clear();

            var allSeletedRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));

            foreach (var roleInfo in allSeletedRoles)
            {
                // add new roles
                user.RoleInfo.Add(roleInfo);
            }
            DbSession.SaveChanges();
            return true;
        }
    }
}
