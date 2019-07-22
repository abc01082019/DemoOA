
using MAP.OA.EFDAL;
using MAP.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.DALFactory
{
    public partial class DbSession: IDbSession
    {
	
		public IOrderInfoDal OrderInfoDal
        {
            get { return StaticDalFactory.GetOrderInfoDal(); }
        } 
	
		public IRoleInfoDal RoleInfoDal
        {
            get { return StaticDalFactory.GetRoleInfoDal(); }
        } 
	
		public IUserInfoDal UserInfoDal
        {
            get { return StaticDalFactory.GetUserInfoDal(); }
        } 
	
		public int SaveChanges()
		{
			return DbContextFactory.GetCurrentDbContext().SaveChanges();
		}
    }
}