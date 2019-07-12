using MAP.OA.DALFactory;
using MAP.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.BLL
{
    /// <summary>
    /// 父类逼迫子类给自己的一个属性赋值
    /// 赋值操作必须要在父类其他方法被调用前被赋值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T: class, new()
    {
        public IBaseDal<T> CurrentDal { get; set; }

        public IDbSession DbSession
        {
            get
            {
                return DbSessionFactory.GetCurrentDbSession();
            }
        }

        public BaseService() // 构造函数调用子类重写的抽象方法
        {
            SetCurrentDal();
        }

        public abstract void SetCurrentDal(); // 抽象方法 要求子类必须实现

        
        //CRUD

        #region Select/Read
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);
        }

        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda, isAsc);
        }
        #endregion

        #region Insert/Create
        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }
        #endregion

        #region Update
        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }
        #endregion

        #region Delete
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
