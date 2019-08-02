using MAP.OA.Model;
using MAP.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.EFDAL
{
    /// <summary>
    /// Responsibility: Encapsulate all DAL's common CRUD
    /// Responsibility of one class must be single 
    /// </summary>
    public class BaseDal<T> where T: class, new ()
    {
        // 这时还是用new，将new去掉。变得低耦合
        //private DataModelContainer dbContent = new DataModelContainer();

        // 依赖抽象编程. 好处: 可以应对变化的时候，改变最小.
        // 最求: 业务/需求变化时代码改动最小
        public DbContext DbContent
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }


        //CRUD

        #region Select/Read
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return DbContent.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc)
        {
            total = DbContent.Set<T>().Where(whereLambda).Count();
            if (isAsc)
                return DbContent.Set<T>().Where(whereLambda)
                    .OrderBy<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
            else
                return DbContent.Set<T>().Where(whereLambda)
                    .OrderByDescending<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();

        }
        #endregion

        #region Insert/Create
        public T Add(T entity)
        {
            DbContent.Set<T>().Add(entity);
            //DbContent.SaveChanges();
            return entity;
        }
        #endregion

        #region Update
        public bool Update(T entity)
        {
            DbContent.Set<T>().Attach(entity);
            DbContent.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            //return DbContent.SaveChanges() > 0;
            return true;
        }
        #endregion

        #region Delete
        public bool Delete(T entity)
        {
            DbContent.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            //return DbContent.SaveChanges() > 0;
            return true;
        }

        public bool Delete(int id)
        {
            var entity = DbContent.Set<T>().Find(id);
            DbContent.Set<T>().Remove(entity);
            return true;
        }

        public bool DeleteLogical(int id)
        {
            var entity = DbContent.Set<T>().Find(id);
            DbContent.Entry<T>(entity).Property("DelFlag").CurrentValue = (short)DelFlagEnum.Deleted;
            DbContent.Entry<T>(entity).Property("DelFlag").IsModified = true;
            return true;
        }
        #endregion
    }
}
