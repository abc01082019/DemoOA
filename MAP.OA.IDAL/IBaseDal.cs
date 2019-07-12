using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.IDAL
{
    public interface IBaseDal<T> where T: class, new ()
    {
        //CRUD

        #region Select/Read
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc);
        #endregion

        #region Insert/Create
        T Add(T entity);
        #endregion

        #region Update
        bool Update(T entity);
        #endregion

        #region Delete
        bool Delete(T entity);
        #endregion
    }
}
